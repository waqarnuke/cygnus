
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name = "List Price")]
        [Range(1,1000)]
        public double ListPrice { get; set; }
        
        [Required]
        [Display(Name = "Price for 1-50+")]
        [Range(1,1000)]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1,1000)]
        public double Price50 { get; set; }
        
        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1,1000)]
        public double Price100 { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        
        [ValidateNever]
        public string ImageUrl { get; set; }
        public string Barcode { get; set; }

        [Display(Name = "Brand")]
        public int? BrandId { get; set; }
        [ValidateNever]
        public Brand Brands { get; set; }

        [Display(Name = "Sub Category")]
        public int? SubCategoryId { get; set; }
        [ValidateNever]
        public SubCategory SubCategory { get; set; }
        public bool? Featured { get; set; } = false;
        public bool? Sale { get; set; } = false;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        
    }
}