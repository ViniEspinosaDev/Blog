using Microsoft.Data.SqlClient;

namespace Blog.Repository
{
    public class BaseRepository
    {
        public const string CONNECTION_STRING = "Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$";

        public readonly SqlConnection _connection;

        public BaseRepository(SqlConnection connection)
        {
            _connection = connection;
        }
    }
}