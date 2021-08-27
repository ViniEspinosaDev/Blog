using Blog.Models;
using Blog.Repository;

namespace Blog.Controller
{
    public class TagController
    {
        public int Create(Tag tag)
        {
            var repository = new BaseRepository<Tag>(null);
            return repository.Create(tag);
        }

        public int LinkPostInTag(int postId, int tagId)
        {
            if (postId <= 0 || tagId <= 0)
                return 0;

            var repository = new TagRepository(null);
            return repository.LinkPostInTag(postId, tagId);
        }
    }
}