using CodeHollow.FeedReader;
using CodeHollow.FeedReader.Feeds;
using ThirdWay.Data.Model;

namespace ThirdWay.Feed
{
    public class Reader
    {
        private string _feedUrl;
        private CodeHollow.FeedReader.Feed _feed;

        public Reader(string feedUrl)
        {
            _feedUrl = feedUrl;
        }

        protected async Task GetFeed()
        {
            if(_feed?.Link == null)
                _feed = await FeedReader.ReadAsync(_feedUrl);
        }

        public async Task<Data.Model.Feed> GetFeedMetadataAsync()
        {
            await GetFeed();
            var twFeed = new Data.Model.Feed();
            twFeed.LastUpdated = _feed.LastUpdatedDate ?? DateTime.UtcNow;
            twFeed.Description = _feed.Description ?? "";
            twFeed.Uri = _feedUrl;
            twFeed.Url = _feed.Link;
            twFeed.ImageUrl = _feed.ImageUrl;
            twFeed.Title = _feed.Title;
            twFeed.Author = "";
            if (_feed.Type == FeedType.Atom)
            {
                twFeed.Author = ((AtomFeed)_feed.SpecificFeed).Author.Name ?? "";
            }

            return twFeed;
        }

        public async Task<IList<Post>> GetPostsAsync()
        {
            var posts = new List<Post>();
            await GetFeed();

            foreach (var item in _feed.Items)
            {
                var post = new Post();
                post.Uri = item.Link;
                post.UriHash = Utilities.GetHashFromString(item.Link);
                post.Title = item.Title;
                post.Author = item.Author;
                post.PublishDateTime = item.PublishingDate ?? DateTime.UtcNow;
                post.LastUpdated = item.PublishingDate ?? DateTime.UtcNow;
                
                if (_feed.Type == FeedType.Atom)
                {
                    var atomPost = (AtomFeedItem)item.SpecificItem;
                    post.Author = atomPost?.Author.Name ?? ((AtomFeed)_feed.SpecificFeed).Author.Name ?? _feed.Title;
                    post.LastUpdated = atomPost?.UpdatedDate ?? DateTime.UtcNow;
                }

                post.Description = item.Description;

                var parser = new ItemParser(_feed);
                post.Body = parser.ParseBody(item);
                post.ImageUrl = parser.GetHeroImage(item);

                posts.Add(post);
            }

            return posts;
        }
    }
}
