using Blog.Models;
using Blog.Repository;

namespace Blog.Controller
{
    public class UserController
    {
        public int Create(User user)
        {
            var repository = new BaseRepository<User>(null);
            return repository.Create(user);
        }

    }
}