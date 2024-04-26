using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class SiteConfig
    {
        [Key]
        public string key { get; set; }
        public string Value { get; set; }
    }
}