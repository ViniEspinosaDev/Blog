using System.Linq;
using Blog.Models;
using Blog.Repository;

namespace Blog.Controller
{
    public class PostController
    {
        public string ListTags(Post post)
        {
            string listTags = string.Empty;

            if (post.Tags.Count == 0)
            {
                listTags = $"* Post {post.Id} está Sem tags *";
                return listTags;
            }

            foreach (var tag in post.Tags)
                listTags += $" - Nome: {tag.Name}\n";

            return listTags;
        }

        private Post CompletePostInformations(Post post)
        {
            if (post.CategoryId > 0)
            {
                var repository = new BaseRepository<Category>(null);
                post.Category = repository.Get(post.CategoryId);
            }

            if (post.AuthorId > 0)
            {
                var repository = new BaseRepository<User>(null);
                post.Author = repository.Get(post.AuthorId);
            }

            return post;
        }

        public string ListAllPostsWithTags()
        {
            var postRepository = new PostRepository(null);

            string allPostsWithTags = string.Empty;

            var posts = postRepository.GetWithTags();

            bool noPost = posts.Count() == 0;

            if (noPost)
                return "* Nenhum post foi encontrado. *";

            allPostsWithTags += "--- Listando Posts ---\n";
            foreach (var post in posts)
            {
                CompletePostInformations(post);

                if (post.Category.Id > 0)
                    allPostsWithTags += $"Id: {post.Id} - Nome: {post.Title} - Categoria: {post.Category.Name}\n";
                else
                    allPostsWithTags += $"Id: {post.Id} - Nome: {post.Title} - Categoria: *Sem categoria*\n";

                if (post.Tags.Count > 0)
                {
                    foreach (var tag in post.Tags)
                    {
                        allPostsWithTags += $"  - Tag id: {tag.Id} - Nome: {tag.Name}\n";
                    }
                }
                else
                    allPostsWithTags += $"* Nenhuma tag para post {post.Id}.*\n";
            }

            return allPostsWithTags;
        }

        public int Create(Post post)
        {
            var repository = new BaseRepository<Post>(null);
            return repository.Create(post);
        }
    }
}