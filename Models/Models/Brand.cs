using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Brand Name")]
        [MaxLength(30)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }   
    }
}