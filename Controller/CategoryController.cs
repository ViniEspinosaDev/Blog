using Blog.Models;
using Blog.Repository;

namespace Blog.Controller
{
    public class CategoryController
    {
        public string ListPosts(Category category)
        {
            string listPosts = string.Empty;

            if (category.Posts.Count == 0)
            {
                listPosts = $"* Categoria {category.Name} está Sem posts *";
                return listPosts;
            }

            foreach (var post in category.Posts)
                listPosts += $" - Título: {post.Title} Sumário: {post.Summary}\n";

            return listPosts;
        }

        public int Create(Category category)
        {
            var repository = new BaseRepository<Category>(null);
            return repository.Create(category);
        }
    }
}