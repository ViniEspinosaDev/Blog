using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Blog.Models
{
    [Table("[Post]")]
    public class Post
    {
        public Post()
        {
            Tags = new List<Tag>();
            Category = new Category();
            Author = new User();
            CategoryId = 0;
            AuthorId = 0;
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        [Write(false)]
        public Category Category { get; set; }
        [Write(false)]
        public User Author { get; set; }

        [Write(false)]
        public List<Tag> Tags { get; set; }

        public override string ToString()
        {
            return $@"
                    ------- || -------
                    POST: {Id}
                    - Nome: {Title}
                    - Sumário: {Summary}
                    - Body: {Body}
                    - Qtd Tags: {Tags.Count}
                    - Categoria: {Category.Name}
                    - Autor: {Author.Name}
                    ------- || -------
                    ";
        }
    }
}