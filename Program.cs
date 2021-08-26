using Blog.Models;
using Blog.Repository;
using Blog.Service;

namespace Blog
{
    class Program
    {
        static void Main(string[] args)
        {
            //UserCalls();
            RoleCalls();
        }

        static void UserCalls()
        {
            var userService = new UserService(new UserRepository(null));

            //var user = userController.ReadUser(1);
            //System.Console.WriteLine($"{user.Id} - {user.Name}");

            #region Create User

            var newUser = new User()
            {
                Id = 3,
                Name = "André Baltieri",
                Email = "andre.baltieri@balta.io",
                PasswordHash = "andre123",
                Bio = "8x Microsoft MVP",
                Image = "https://",
                Slug = "andre-balteri"
            };

            #endregion

            //int idNewUser = userController.CreateUser(newUser);
            //System.Console.WriteLine($"Novo usuário: {idNewUser}");

            // bool ok = userController.UpdateUser(newUser);
            bool ok = userService.Delete(2);

            System.Console.WriteLine($"Deu bom: {ok}");

            var users = userService.GetAll();

            foreach (var item in users)
                System.Console.WriteLine($"{item.Id} - {item.Name}");
        }

        static void RoleCalls()
        {
            var roleService = new RoleService(new RoleRepository(null));

            #region Create Role

            var newRole = new Role()
            {
                Name = "Author",
                Slug = "author"
            };

            #endregion

            int id = roleService.Create(newRole);
            System.Console.WriteLine($"Id nova Role: {id}");

            var roles = roleService.GetAll();

            foreach (var item in roles)
                System.Console.WriteLine($"{item.Id} - {item.Name}");
        }
    }
}
