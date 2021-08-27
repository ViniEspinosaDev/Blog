using System.Collections.Generic;
using System.Linq;
using Blog.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repository
{
    public class PostRepository : BaseRepository<Post>
    {
        public PostRepository(SqlConnection connection) : base(connection) { }

        public IEnumerable<Post> GetWithTags()
        {
            var posts = new List<Post>();
            string query = $@"
                                SELECT
                                    [Post].*,
                                    [Tag].*
                                FROM
                                    [Post]
                                    LEFT JOIN [PostTag] ON [Post].[Id] = [PostTag].[PostId]
                                    LEFT JOIN [Tag] ON [PostTag].[TagId] = [Tag].[Id]
                            ";

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                var post = connection.Query<Post, Tag, Post>
                (
                    query,
                    (post, tag) =>
                    {
                        var pst = posts.FirstOrDefault(x => x.Id == post.Id);

                        if (pst == null)
                        {
                            pst = post;
                            if (tag != null)
                                pst.Tags.Add(tag);
                            posts.Add(pst);
                        }
                        else
                        if (tag != null)
                            pst.Tags.Add(tag);

                        return post;
                    }, splitOn: "Id"
                );
            }

            return posts;
        }
    }
}