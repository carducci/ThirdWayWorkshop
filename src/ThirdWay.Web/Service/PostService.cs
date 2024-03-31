using Microsoft.EntityFrameworkCore;
using ThirdWay.Data;
using ThirdWay.Data.Model;

namespace ThirdWay.Web.Service
{
    public interface IPostService
    {
        Task<Post> GetPostAsync(int id);
        Task<Post> GetPostAsync(string hash);
        Task<List<Post>> GetAllAsync(int take = 5, int offset=0);
        Task<List<Post>> GetUnreadAsync(int take = 5, int offset = 0);
        Task<List<Post>> GetFavoriteAsync(int take = 5, int offset = 0);
        Task MarkReadAsync(int id);
        Task MarkUnreadAsync(int id);
        Task ToggleFavoriteAsync(int id);
        void Dispose();
        Task<List<Post>> SearchPosts(string search);
    }

    public class PostService(ReaderContext context) : IDisposable, IPostService
    {
        private readonly ReaderContext _context = context;

        public async Task<Post> GetPostAsync(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id)!;

            return post;
        }

        public async Task<Post> GetPostAsync(string hash)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.UriHash == hash)!;

            return post;
        }

        public async Task<List<Post>> GetAllAsync(int take = 5, int offset = 0) 
            => await _context.Posts.OrderByDescending(p => p.PublishDateTime).Skip(offset).Take(take).ToListAsync();

        public async Task<List<Post>> GetUnreadAsync(int take = 5, int offset = 0) 
            => await _context.Posts.Where(p => !p.IsRead).OrderByDescending(p => p.PublishDateTime).Skip(offset).Take(take).ToListAsync();

        public async Task<List<Post>> GetFavoriteAsync(int take = 5, int offset = 0) 
            => await _context.Posts.Where(p => p.IsFavorite).OrderByDescending(p => p.PublishDateTime).Skip(offset).Take(take).ToListAsync();

        public async Task MarkReadAsync(int id)
        {
            var post = await GetPostAsync(id);
            post.IsRead = true;
            await _context.SaveChangesAsync();
        }

        public async Task MarkUnreadAsync(int id)
        {
            var post = await GetPostAsync(id);
            post.IsRead = false;
            await _context.SaveChangesAsync();
        }

        public async Task ToggleFavoriteAsync(int id)
        {
            var post = await GetPostAsync(id);
            post.IsFavorite = !post.IsFavorite;
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<List<Post>> SearchPosts(string search)
        {
            //ultra simplistic search
            var terms = search.ToLower().Split(" ");
            return await _context.Posts.Where(p => terms.Any(term => p.Title.ToLower().Contains(term))).ToListAsync();
        }
    }
}
