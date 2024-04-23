using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; } // id which will be primary key
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; } // Category Name

        [DisplayName("Display Order")]
        [Range(1,100)]
        public int DisplayOrder { get; set; }   // which category should be displayd firt on the page 
        public string ImageUrl { get; set; }
    }
}