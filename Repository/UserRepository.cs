using System.Collections.Generic;
using Blog.Interface;
using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(SqlConnection connection) : base(connection) { }

        public int Create(User user)
        {
            user.Id = 0;

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                long id = connection.Insert<User>(user);

                return (int)id;
            }
        }

        public bool Delete(int id)
        {
            var user = Get(id);

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Delete<User>(user);
            }
        }

        public User Get(int id)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Get<User>(id);
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.GetAll<User>();
            }
        }

        public bool Update(User user)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Update<User>(user);
            }
        }
    }
}