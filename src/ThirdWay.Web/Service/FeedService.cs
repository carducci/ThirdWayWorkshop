using Microsoft.EntityFrameworkCore;
using ThirdWay.Data;

namespace ThirdWay.Web.Service
{
    public interface IFeedService
    {
        Task<Data.Model.Feed> GetFeedAsync(int id);
        Task<List<Data.Model.Feed>> GetAllAsync(int take = 5, int offset = 0);
        Task UpsertFeedAsync(string feedUrl);
        void Dispose();
    }

    public class FeedService(ReaderContext context) : IDisposable, IFeedService
    {
        private readonly ReaderContext _context = context;

        public async Task<Data.Model.Feed> GetFeedAsync(int id)
        {
            return await _context.Feeds.FirstOrDefaultAsync(p => p.Id == id)!;
        }

        public async Task<List<Data.Model.Feed>> GetAllAsync(int take = 5, int offset = 0) => await _context.Feeds.OrderByDescending(p => p.Id).Skip(offset).Take(take).ToListAsync();

        public async Task UpsertFeedAsync(string feedUrl)
        {
            var Reader = new Feed.Reader(feedUrl);
            var feed = await Reader.GetFeedMetadataAsync();
            var posts = await Reader.GetPostsAsync();

            if (_context.Feeds.Any(f => f.Url == feedUrl))
            {
                var oldFeed = _context.Feeds.First(f => f.Url == feedUrl);
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
    }
}
