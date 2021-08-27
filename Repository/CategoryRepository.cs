using System.Collections.Generic;
using System.Linq;
using Blog.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repository
{
    public class categoryRepository : BaseRepository<Category>
    {
        public categoryRepository(SqlConnection connection) : base(connection) { }

        public IEnumerable<Category> GetWithPosts()
        {
            var categories = new List<Category>();
            string query = @"
                            SELECT 
                                [Category].*,
                                [Post].*
                            FROM
                                [Category]
                                LEFT JOIN [Post] ON [Category].[Id] = [Post].[CategoryId]
                            ";

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                var category = connection.Query<Category, Post, Category>
                (
                    query,
                    (category, post) =>
                    {
                        var cat = categories.FirstOrDefault(x => x.Id == category.Id);

                        if (cat == null)
                        {
                            cat = category;
                            if (post != null)
                                cat.Posts.Add(post);
                            categories.Add(cat);
                        }
                        else
                        if (post != null)
                            cat.Posts.Add(post);

                        return category;
                    },
                    splitOn: "Id"
                );
            }

            return categories;
        }
    }
}