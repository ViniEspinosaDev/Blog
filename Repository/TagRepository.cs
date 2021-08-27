using System.Collections.Generic;
using System.Linq;
using Blog.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repository
{
    public class TagRepository : BaseRepository<Tag>
    {
        public TagRepository(SqlConnection connection) : base(connection) { }

        public IEnumerable<Tag> GetWithPosts()
        {
            var tags = new List<Tag>();
            string query = $@"
                                SELECT
                                    [Tag].*,
                                    [Post].*
                                FROM
                                    [Tag]
                                    LEFT JOIN [PostTag] ON [Tag].[Id] = [PostTag].[TagId]
                                    LEFT JOIN [Post] ON [PostTag].[PostId] = [Post].[Id]
                            ";

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                var tag = connection.Query<Tag, Post, Tag>
                (
                    query,
                    (tag, post) =>
                    {
                        var tg = tags.FirstOrDefault(x => x.Id == tag.Id);

                        if (tg == null)
                        {
                            tg = tag;
                            if (post != null)
                                tg.Posts.Add(post);
                            tags.Add(tg);
                        }
                        else
                        if (post != null)
                            tg.Posts.Add(post);

                        return tag;
                    }, splitOn: "Id"
                );
            }

            return tags;
        }

        public int LinkPostInTag(int postId, int tagId)
        {
            string query = " INSERT INTO [PostTag] VALUES (@PostId, @TagId) ";

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Execute(query, new
                {
                    PostId = postId,
                    TagId = tagId
                });
            }
        }
    }
}