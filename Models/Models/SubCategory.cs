using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("CategoryType Name")]
        [MaxLength(30)]
        public string Name { get; set; } 
    }
}