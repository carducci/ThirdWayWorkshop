using CodeHollow.FeedReader;
using CodeHollow.FeedReader.Feeds;
using ThirdWay.Data.Model;

namespace ThirdWay.Feed
{
    public class Reader(string feedUrl)
    {
        private readonly string _feedUrl = feedUrl;
        private CodeHollow.FeedReader.Feed? _feed;

        protected async Task GetFeed()
        {
            _feed ??= await FeedReader.ReadAsync(_feedUrl);
        }

        public async Task<Data.Model.Feed> GetFeedMetadataAsync()
        {
            await GetFeed();
            var twFeed = new Data.Model.Feed
            {
                LastUpdated = _feed!.LastUpdatedDate ?? DateTime.UtcNow,
                Description = _feed.Description ?? "",
                Uri = _feedUrl,
                Url = _feed.Link,
                ImageUrl = _feed.ImageUrl ?? "",
                Title = _feed.Title,
                Author = ""
            };
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

            foreach (var item in _feed!.Items)
            {
                var post = new Post
                {
                    Uri = item.Link,
                    UriHash = Utilities.GetHashFromString(item.Link),
                    Title = item.Title ?? " - ",
                    Author = item.Author ?? " - ",
                    PublishDateTime = item.PublishingDate ?? DateTime.UtcNow,
                    LastUpdated = item.PublishingDate ?? DateTime.UtcNow
                };

                if (_feed.Type == FeedType.Atom)
                {
                    var atomPost = (AtomFeedItem)item.SpecificItem;
                    post.Author = atomPost?.Author.Name ?? ((AtomFeed)_feed.SpecificFeed).Author.Name ?? _feed.Title;
                    post.LastUpdated = atomPost?.UpdatedDate ?? DateTime.UtcNow;
                }

                post.Description = item.Description;

                var parser = new ItemParser(_feed);
                post.Body = parser.ParseBody(item) ?? item.Description;
                post.ImageUrl = parser.GetHeroImage(item);

                posts.Add(post);
            }

            return posts;
        }
    }
}
