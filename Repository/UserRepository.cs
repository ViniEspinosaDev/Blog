using System.Collections.Generic;
using System.Linq;
using Blog.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(SqlConnection connection) : base(connection) { }

        public IEnumerable<User> GetWithRoles()
        {
            string query = @" 
                                SELECT 
                                    [User].*,
                                    [Role].*
                                FROM
                                    [User]
                                    LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                                    LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id] 
                                ";

            var users = new List<User>();

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                var items = connection.Query<User, Role, User>
                (
                    query,
                    (user, role) =>
                    {
                        var usr = users.FirstOrDefault(x => x.Id == user.Id);

                        if (usr == null)
                        {
                            usr = user;
                            if (role != null)
                                usr.Roles.Add(role);
                            users.Add(usr);
                        }
                        else
                        if (role != null)
                            usr.Roles.Add(role);

                        return user;
                    }, splitOn: "Id");
            }

            return users;
        }
    }
}