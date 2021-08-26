using System.Collections.Generic;
using Blog.Interface;
using Blog.Models;

namespace Blog.Service
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAll() =>
            _userRepository.GetAll();

        public User Get(int id) =>
            _userRepository.Get(id);

        public int Create(User user) =>
            _userRepository.Create(user);

        public bool Update(User user) =>
            _userRepository.Update(user);

        public bool Delete(int id) =>
            _userRepository.Delete(id);
    }
}