using System.Collections.Generic;
using Blog.Models;

namespace Blog.Interface
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role Get(int id);
        int Create(Role role);
        bool Update(Role role);
        bool Delete(int id);
    }
}