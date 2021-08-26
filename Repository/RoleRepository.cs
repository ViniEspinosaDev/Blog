using System.Collections.Generic;
using Blog.Interface;
using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repository
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(SqlConnection connection) : base(connection) { }

        public int Create(Role role)
        {
            role.Id = 0;

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                long id = connection.Insert<Role>(role);

                return (int)id;
            }
        }

        public bool Delete(int id)
        {
            var role = Get(id);

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Delete<Role>(role);
            }
        }

        public Role Get(int id)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Get<Role>(id);
            }
        }

        public IEnumerable<Role> GetAll()
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.GetAll<Role>();
            }
        }

        public bool Update(Role role)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Update<Role>(role);
            }
        }
    }
}