using System;
using System.Linq;
using Blog.Controller;
using Blog.Enums;
using Blog.Models;
using Blog.Repository;

namespace Blog
{
    class Program
    {
        #region Desafios do Balta

        // Cadastrar um perfil (NAO ENTENDI)
        // Vincular um usuário a um perfil (NAO ENTENDI)

        #endregion

        static void Main(string[] args)
        {
            int option = 0;

            do
            {
                do
                {
                    WriteMenuOptions();
                    int.TryParse(Console.ReadLine(), out option);
                } while (option < 1 || option > 12);

                SelectMenuMethods(option);

            } while (true);
        }

        static void WriteMenuOptions()
        {
            Console.Clear();
            Console.WriteLine("---- MENU ----");
            Console.WriteLine("1 - Cadastrar um usuário");
            Console.WriteLine("2 - Cadastrar uma categoria");
            Console.WriteLine("3 - Cadastrar uma tag");
            Console.WriteLine("4 - Cadastrar um post");
            Console.WriteLine("5 - Vincular um post a uma tag");
            Console.WriteLine("6 - Listar os usuários");
            Console.WriteLine("7 - Listar categorias com quantidade de posts");
            Console.WriteLine("8 - Listar tags com quantidade de posts");
            Console.WriteLine("9 - Listar os posts de uma categoria");
            Console.WriteLine("10 - Listar todos os posts com sua categoria e tags");
            Console.WriteLine("11 - Sair");
            Console.Write("Option: ");
        }

        static void SelectMenuMethods(int option)
        {
            var menuEnum = MenuEnum.TransformNumberInEnumMenu(option);

            switch (menuEnum)
            {
                case EMenu.CreateUser:
                    CreateUser();
                    break;
                case EMenu.CreateCategory:
                    CreateCategory();
                    break;
                case EMenu.CreateTag:
                    CreateTag();
                    break;
                case EMenu.CreatePost:
                    CreatePost();
                    break;
                case EMenu.LinkPostTag:
                    LinkPostTag();
                    break;
                case EMenu.ListUsers:
                    ListUsers();
                    break;
                case EMenu.ListCategoryQtyPosts:
                    ListCategoryQtyPosts();
                    break;
                case EMenu.ListTagsQtyPosts:
                    ListTagsQtyPosts();
                    break;
                case EMenu.ListPostsCategory:
                    ListPostsCategory();
                    break;
                case EMenu.ListAllPostsInfo:
                    ListAllPostsInfo();
                    break;
                default: Exit(); break;
            }
        }

        private static void CreateUser()
        {
            var userRepository = new UserRepository(null);
            var user = new User();

            Console.Clear();
            Console.WriteLine("--- Criação de usuário ---");
            Console.Write("Nome: ");
            user.Name = Console.ReadLine();
            Console.Write("Email: ");
            user.Email = Console.ReadLine();
            Console.Write("Senha: ");
            user.PasswordHash = Console.ReadLine();
            Console.Write("Bio: ");
            user.Bio = Console.ReadLine();
            Console.Write("Imagem: ");
            user.Image = Console.ReadLine();
            Console.Write("Slug: ");
            user.Slug = Console.ReadLine();

            int id = userRepository.Create(user);
            Console.WriteLine($"Usuário {id} foi criado com sucesso.");
            Console.WriteLine("");
            BackToMenuMessage();
        }

        private static void CreateCategory()
        {
            var categoryRepository = new categoryRepository(null);
            var category = new Category();

            Console.Clear();
            Console.WriteLine("--- Criação de categoria ---");
            Console.Write("Nome: ");
            category.Name = Console.ReadLine();
            Console.Write("Slug: ");
            category.Slug = Console.ReadLine();

            int id = categoryRepository.Create(category);
            Console.WriteLine($"Categoria {id} foi criada com sucesso.");
            Console.WriteLine("");
            BackToMenuMessage();
        }

        private static void CreateTag()
        {
            var tagRepository = new TagRepository(null);
            var tag = new Tag();

            Console.Clear();
            Console.WriteLine("--- Criação de tag ---");
            Console.Write("Nome: ");
            tag.Name = Console.ReadLine();
            Console.Write("Slug: ");
            tag.Slug = Console.ReadLine();
            int id = tagRepository.Create(tag);
            Console.WriteLine($"Tag {id} foi criada com sucesso.");
            Console.WriteLine("");
            BackToMenuMessage();
        }

        private static void CreatePost()
        {
            var post = new Post();
            var postRepository = new PostRepository(null);

            Console.Clear();
            Console.WriteLine("--- Criação de post ---");

            Console.Write("Titulo: ");
            post.Title = Console.ReadLine();
            Console.Write("Sumário: ");
            post.Summary = Console.ReadLine();
            Console.Write("Corpo: ");
            post.Body = Console.ReadLine();
            Console.Write("Slug: ");
            post.Slug = Console.ReadLine();
            Console.Write("Autor Id: ");
            int.TryParse(Console.ReadLine(), out int authId);
            post.AuthorId = authId;
            Console.Write("Categoria Id: ");
            int.TryParse(Console.ReadLine(), out int catId);
            post.CategoryId = catId;
            post.CreateDate = DateTime.Now;
            post.LastUpdateDate = DateTime.Now;

            int id = postRepository.Create(post);
            Console.WriteLine($"Post Id {id} foi criado com sucesso.\n");
            BackToMenuMessage();
        }

        private static void LinkPostTag()
        {
            var postRepository = new PostRepository(null);
            var tagRepository = new TagRepository(null);

            var posts = postRepository.GetAll();
            var tags = tagRepository.GetAll();

            Console.Clear();
            Console.WriteLine("--- Vincular um post a uma tag ---");
            Console.Write("Posts: ");
            foreach (var item in posts)
                Console.WriteLine($"{item.Id},");
            Console.WriteLine("");
            foreach (var item in tags)
                Console.WriteLine($"{item.Id}");
            Console.WriteLine("");
            Console.Write("Escolha Post: ");
            int.TryParse(Console.ReadLine(), out int postId);
            Console.Write("Escolha Tag: ");
            int.TryParse(Console.ReadLine(), out int tagId);

            tagRepository.LinkPostInTag(postId, tagId);
            System.Console.WriteLine("Vinculo realizado com sucesso.\n");
            BackToMenuMessage();
        }

        private static void ListUsers()
        {
            var userRepository = new UserRepository(null);
            var users = userRepository.GetWithRoles();

            Console.Clear();
            Console.WriteLine("--- Listando usuários ---");
            foreach (var user in users)
                Console.WriteLine(user.ToString());
            BackToMenuMessage();
        }

        private static void ListCategoryQtyPosts()
        {
            var categoryRepository = new categoryRepository(null);
            var categories = categoryRepository.GetWithPosts();

            Console.Clear();
            Console.WriteLine("--- Listando categorias ---");
            foreach (var category in categories)
                Console.WriteLine(category.ToString());
            BackToMenuMessage();
        }

        private static void ListTagsQtyPosts()
        {
            var tagRepository = new TagRepository(null);
            var tags = tagRepository.GetWithPosts();

            Console.Clear();
            Console.WriteLine("--- Listando tags ---");
            foreach (var tag in tags)
                Console.WriteLine(tag.ToString());
            BackToMenuMessage();
        }

        private static void ListPostsCategory()
        {
            var categoryController = new CategoryController();
            var categoryRepository = new categoryRepository(null);
            var categories = categoryRepository.GetWithPosts();

            Console.Clear();
            Console.WriteLine("--- Listar posts de uma categoria ---");
            if (categories.Count() == 0)
            {
                Console.WriteLine("* Nenhuma categoria inserida. *");
                BackToMenuMessage();
            }

            foreach (var item in categories)
                Console.WriteLine(item.ToString());
            Console.Write("Informe o id da categoria: ");
            int.TryParse(Console.ReadLine(), out int categoryId);

            var category = categories.FirstOrDefault(x => x.Id == categoryId);

            Console.Clear();
            Console.WriteLine($"--- Listando posts da categoria {category.Name}");
            string posts = categoryController.ListPosts(category);
            Console.WriteLine(posts);
            BackToMenuMessage();
        }

        private static void ListAllPostsInfo()
        {
            var postRepository = new PostRepository(null);
            var posts = postRepository.GetWithTags();

            Console.Clear();
            Console.WriteLine("--- Listando Posts com as tags ---");
            foreach (var post in posts)
                Console.WriteLine(post.ToString());
            BackToMenuMessage();
        }

        private static void Exit()
        {
            Console.Clear();
            Console.WriteLine("Saindo... Até breve!");
            Environment.Exit(0);
        }

        private static void BackToMenuMessage()
        {
            Console.WriteLine("... Toque para voltar ao menu");
            Console.ReadLine();
        }
    }
}
