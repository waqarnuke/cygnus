using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Identity;

namespace DataAccess.Data
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> option) :base(option)
        {
        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        //public DbSet<AppUser> AspNetUsers { get; set; }
        public DbSet<SiteConfig> SiteConfig { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubCategory>().HasData(
                new SubCategory { Id = 1, Name = "Vape" },
                new SubCategory { Id = 2, Name = "Pipe" },
                new SubCategory { Id = 3, Name = "Burners" }
            );

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Vape" },
                new Brand { Id = 2, Name = "Pipe" },
                new Brand { Id = 3, Name = "Burners" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "New", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sale", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Disposables", DisplayOrder = 3 },
                new Category { Id = 4, Name = "E-Liquids", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Vape kits", DisplayOrder = 5 },
                new Category { Id = 6, Name = "Accessories", DisplayOrder = 6 },
                new Category { Id = 7, Name = "Multifarious", DisplayOrder = 6 }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "TechSolution", StreeAddress = "12 street block 5", City = "Humble", State = "Tx", ZipCode = "77338", PhoneNumber = "03467778889" },
                new Company { Id = 2, Name = "SoftCygnus", StreeAddress = "13 street block 6", City = "Spring", State = "Tx", ZipCode = "77339", PhoneNumber = "0346123456" },
                new Company { Id = 3, Name = "Microsoft", StreeAddress = "14 street block 7", City = "Kingwood", State = "Tx", ZipCode = "77310", PhoneNumber = "0346456789" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { 
                    Id = 1, 
                    Title = "Vape Time", 
                    Author="Spark", 
                    Description= "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="SWD9999001",
                    ListPrice=99,
                    Price=90,
                    Price50=85,
                    Price100=80,
                    CategoryId = 1,
                    ImageUrl="\\images\\product\\1.jpg",
                    Barcode="345345566456",
                    BrandId = 1,
                    SubCategoryId =1,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                },
                new Product
                {
                    Id = 2,
                    Title = "Vape Dark",
                    Author = "Nancy",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl="\\images\\product\\2.jpg",
                    Barcode="345345566456",
                    BrandId = 1,
                    SubCategoryId =1,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                    
                },
                new Product
                {
                    Id = 3,
                    Title = "Vape Sunset",
                    Author = "Julian",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 2,
                    ImageUrl="\\images\\product\\3.jpg",
                    Barcode="345345566456",
                    BrandId = 2,
                    SubCategoryId = 2,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                    
                },
                new Product
                {
                    Id = 4,
                    Title = "Vape Candy",
                    Author = "Abby",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 2,
                    ImageUrl="\\images\\product\\4.jpg",
                    Barcode="345345566456",
                    BrandId = 2,
                    SubCategoryId = 2,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                    
                },
                new Product
                {
                    Id = 5,
                    Title = "Vape Rock",
                    Author = "Ron",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 3,
                    ImageUrl="\\images\\product\\5.jpg",
                    Barcode="345345566456",
                    BrandId = 3,
                    SubCategoryId = 3,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                    
                },
                new Product
                {
                    Id = 6,
                    Title = "Vape Leaves",
                    Author = "Laura",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId = 3,
                    ImageUrl="\\images\\product\\6.jpg",
                    Barcode="345345566456",
                    BrandId = 3,
                    SubCategoryId = 3,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                    
                },
                new Product { 
                    Id = 7, 
                    Title = "Vape Time", 
                    Author="Spark", 
                    Description= "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN="SWD9999001",
                    ListPrice=99,
                    Price=90,
                    Price50=85,
                    Price100=80,
                    CategoryId = 1,
                    ImageUrl="\\images\\product\\7.jpg",
                    Barcode="345345566456",
                    BrandId = 1,
                    SubCategoryId =1,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                },
                new Product
                {
                    Id = 8,
                    Title = "Vape Dark",
                    Author = "Nancy",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl="\\images\\product\\8.jpg",
                    Barcode="345345566456",
                    BrandId = 1,
                    SubCategoryId =1,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                    
                },
                new Product
                {
                    Id = 9,
                    Title = "Vape Sunset",
                    Author = "Julian",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 2,
                    ImageUrl="\\images\\product\\9.jpg",
                    Barcode="345345566456",
                    BrandId = 2,
                    SubCategoryId = 2,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                    
                },
                new Product
                {
                    Id = 10,
                    Title = "Vape Candy",
                    Author = "Abby",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 2,
                    ImageUrl="\\images\\product\\10.jpg",
                    Barcode="345345566456",
                    BrandId = 2,
                    SubCategoryId = 2,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                    
                },
                new Product
                {
                    Id = 11,
                    Title = "Vape Rock",
                    Author = "Ron",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 3,
                    ImageUrl="\\images\\product\\11.jpg",
                    Barcode="345345566456",
                    BrandId = 3,
                    SubCategoryId = 3,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                    
                },
                new Product
                {
                    Id = 12,
                    Title = "Vape Leaves",
                    Author = "Laura",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId = 3,
                    ImageUrl="\\images\\product\\12.jpg",
                    Barcode="345345566456",
                    BrandId = 3,
                    SubCategoryId = 3,
                    Featured = false,
                    Sale = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Quantity = 15
                    
                }  
            );
        }
    }
}