using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; } 
        public Address Address { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        [NotMapped]
        public Company Compnay { get; set; }

    }
}