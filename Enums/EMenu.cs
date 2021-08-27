namespace Blog.Enums
{
    public enum EMenu
    {
        CreateUser = 1,
        CreateCategory = 2,
        CreateTag = 3,
        CreatePost = 4,
        LinkPostTag = 5,
        ListUsers = 6,
        ListCategoryQtyPosts = 7,
        ListTagsQtyPosts = 8,
        ListPostsCategory = 9,
        ListAllPostsInfo = 10,
        Exit = 11
    }

    public static class MenuEnum
    {
        public static EMenu TransformNumberInEnumMenu(int number)
        {
            switch (number)
            {
                case 1: return EMenu.CreateUser;
                case 2: return EMenu.CreateCategory;
                case 3: return EMenu.CreateTag;
                case 4: return EMenu.CreatePost;
                case 5: return EMenu.LinkPostTag;
                case 6: return EMenu.ListUsers;
                case 7: return EMenu.ListCategoryQtyPosts;
                case 8: return EMenu.ListTagsQtyPosts;
                case 9: return EMenu.ListPostsCategory;
                case 10: return EMenu.ListAllPostsInfo;
                default:
                    return EMenu.Exit;
            }
        }
    }
}