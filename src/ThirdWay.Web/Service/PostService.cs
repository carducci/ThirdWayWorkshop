﻿using ThirdWay.Data;
using ThirdWay.Data.Model;

namespace ThirdWay.Web.Service
{
    public interface IPostService
    {
        Post GetPost(int id);
        List<Post> GetAll(int take = 5, int offset=0);
        List<Post> GetUnread(int take = 5, int offset = 0);
        List<Post> GetFavorite(int take = 5, int offset = 0);
        void MarkRead(int id);
        void MarkUnread(int id);
        void MarkFavorite(int id);
        void MarkUnFavorite(int id);
        void Dispose();
    }

    public class PostService : IDisposable, IPostService
    {
        private readonly ILogger<PostService> _logger;
        private readonly ReaderContext _context;

        public PostService(ILogger<PostService> logger, ReaderContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Post GetPost(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id)!;
        }

        public List<Post> GetAll(int take = 5, int offset=0)
        {
            return _context.Posts.OrderByDescending(p => p.PublishDateTime).Skip(offset).Take(take).ToList();
        }

        public List<Post> GetUnread(int take = 5, int offset = 0)
        {
            return _context.Posts.Where(p=>!p.IsRead).OrderByDescending(p => p.PublishDateTime).Skip(offset).Take(take).ToList();
        }

        public List<Post> GetFavorite(int take = 5, int offset = 0)
        {
            return _context.Posts.Where(p => p.IsFavorite).OrderByDescending(p => p.PublishDateTime).Skip(offset).Take(take).ToList();
        }

        public void MarkRead(int id)
        {
            var post = GetPost(id);
            post.IsRead = true;
            _context.SaveChanges();
        }

        public void MarkUnread(int id)
        {
            var post = GetPost(id);
            post.IsRead = false;
            _context.SaveChanges();
        }

        public void MarkFavorite(int id)
        {
            var post = GetPost(id);
            post.IsFavorite = true;
            _context.SaveChanges();
        }

        public void MarkUnFavorite(int id)
        {
            var post = GetPost(id);
            post.IsFavorite = false;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
