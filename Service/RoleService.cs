using System.Collections.Generic;
using Blog.Interface;
using Blog.Models;

namespace Blog.Service
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<Role> GetAll() =>
            _roleRepository.GetAll();

        public Role Get(int id) =>
            _roleRepository.Get(id);

        public int Create(Role role) =>
            _roleRepository.Create(role);

        public bool Update(Role role) =>
            _roleRepository.Update(role);

        public bool Delete(int id) =>
            _roleRepository.Delete(id);
    }
}