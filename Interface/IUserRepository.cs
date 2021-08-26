using System.Collections.Generic;
using Blog.Models;

namespace Blog.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        int Create(User user);
        bool Update(User user);
        bool Delete(int id);
    }
}