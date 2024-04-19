using Models;

namespace Web.ViewModel
{
    public class ShopVM
    {
        public IEnumerable<Product> Products { get; set; }
        public int? CategoryId { get; set; }
        public int? MaximumPrice { get; set; } 
        public int? MinimubPrice { get; set; } 
        public int? PageNo { get; set; }
        public string? search {get; set;}
        public int? SortBy { get; set; }
        public Pager Pager { get; set; }
    }
}