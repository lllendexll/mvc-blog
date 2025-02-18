using System;
using System.ComponentModel.DataAnnotations;

namespace MvcBlog.Models 
{
    public class Article
    {
        public int ArticleId { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Body { get; set; }

        //public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime CreateDate { get; set; } = new DateTime(2024, 2, 16, 12, 0, 0);

        
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public required string ContributorUsername { get; set; }
    }
}
