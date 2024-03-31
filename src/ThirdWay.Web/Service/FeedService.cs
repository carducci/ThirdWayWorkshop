using Microsoft.EntityFrameworkCore;
using ThirdWay.Data;

namespace ThirdWay.Web.Service
{
    public interface IFeedService
    {
        Task<Data.Model.Feed?> GetFeedAsync(int id);
        Task<List<Data.Model.Feed>> GetAllAsync();
        Task UpsertFeedAsync(string feedUrl);
        void Dispose();
        Task RefreshAllAsync();
        Task DeleteFeed(int id);
    }

    public class FeedService(ReaderContext context) : IDisposable, IFeedService
    {
        private readonly ReaderContext _context = context;

        public async Task<Data.Model.Feed?> GetFeedAsync(int id)
        {
            return await _context.Feeds.FirstOrDefaultAsync(p => p.Id == id)!;
        }

        public async Task<List<Data.Model.Feed>> GetAllAsync() => await _context.Feeds.OrderByDescending(p => p.Id).ToListAsync();

        public async Task UpsertFeedAsync(string feedUrl)
        {
            var Reader = new Feed.Reader(feedUrl);
            var feed = await Reader.GetFeedMetadataAsync();
            var posts = await Reader.GetPostsAsync();

            if (_context.Feeds.Any(f => f.Uri == feedUrl))
            {
                var oldFeed = _context.Feeds.First(f => f.Uri == feedUrl);
                oldFeed.LastUpdated = feed.LastUpdated;
                oldFeed.Description = feed.Description ?? "";
                oldFeed.ImageUrl = feed.ImageUrl;
                oldFeed.Title = feed.Title;
                feed.Id = oldFeed.Id;
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Feeds.Add(feed);
                await _context.SaveChangesAsync();
            }
            

            foreach (var post in posts)
            {
                if (_context.Posts.Any(p => p.Uri == post.Uri)) continue;
                post.FeedId = feed.Id;
                _context.Posts.Add(post);
            }

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task RefreshAllAsync()
        {
            var feeds = await GetAllAsync();
            var tasks = feeds.Select(feed => UpsertFeedAsync(feed.Url)).ToList();

            await Task.WhenAll(tasks);
        }

        public async Task DeleteFeed(int id)
        {
            await _context.Posts.Where(p => p.FeedId == id).ExecuteDeleteAsync();
            await _context.Feeds.Where(f => f.Id == id).ExecuteDeleteAsync();
        }
    }
}
