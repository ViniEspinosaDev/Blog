using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repository
{
    // OBS: Repo genérico. Só irá aceitar quando T for classe
    public class BaseRepository<T> where T : class
    {
        public const string CONNECTION_STRING = "Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$";

        private readonly SqlConnection _connection;

        public BaseRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public int Create(T model)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                long id = connection.Insert<T>(model);

                return (int)id;
            }
        }

        public bool Delete(int id)
        {
            var model = Get(id);

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Delete<T>(model);
            }
        }

        public T Get(int id)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Get<T>(id);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.GetAll<T>();
            }
        }

        public bool Update(T model)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                return connection.Update<T>(model);
            }
        }
    }
}