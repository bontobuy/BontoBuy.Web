namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BontoBuy.Web.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BontoBuy.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BontoBuy.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //Instructions:
            //Just do the update-database and the db will be seeded
            //Do one table at a time!!!
            //Codes are placed in region for convenience

            #region Category

            context.Categories.AddOrUpdate(
                p => p.CategoryId,
                new CategoryViewModel { Description = "Mobiles & Tablets", Status = "Active" },
                new CategoryViewModel { Description = "Computers", Status = "Active" },
                new CategoryViewModel { Description = "Gaming", Status = "Active" },
                new CategoryViewModel { Description = "Electronics", Status = "Active" },
                new CategoryViewModel { Description = "Women's Fashion", Status = "Active" },
                new CategoryViewModel { Description = "Men's Fashion", Status = "Active" },
                new CategoryViewModel { Description = "Kids' Fashion", Status = "Active" },
                new CategoryViewModel { Description = "Home & Kitchen", Status = "Active" },
                new CategoryViewModel { Description = "Sport, Fitness & Outdoor", Status = "Active" }
                );

            #endregion Category

            #region Product

            context.Products.AddOrUpdate(
                p => p.ProductId,
                new ProductViewModel() { CategoryId = 1, Description = "Mobile Phones", Status = "Active" },
                new ProductViewModel() { CategoryId = 1, Description = "Mobile Accessories", Status = "Active" },
                new ProductViewModel() { CategoryId = 1, Description = "Mobile Case & Covers", Status = "Active" },
                new ProductViewModel() { CategoryId = 1, Description = "Power Banks", Status = "Active" },
                new ProductViewModel() { CategoryId = 1, Description = "Mobile Memory Cards", Status = "Active" },
                new ProductViewModel() { CategoryId = 1, Description = "Tablets", Status = "Active" },
                new ProductViewModel() { CategoryId = 1, Description = "Tablet Accessories", Status = "Active" },
                new ProductViewModel() { CategoryId = 2, Description = "Laptops", Status = "Active" },
                new ProductViewModel() { CategoryId = 2, Description = "Desktops", Status = "Active" },
                new ProductViewModel() { CategoryId = 2, Description = "Computer Components", Status = "Active" },
                new ProductViewModel() { CategoryId = 2, Description = "Printers & Inks", Status = "Active" },
                new ProductViewModel() { CategoryId = 2, Description = "Storage", Status = "Active" },
                new ProductViewModel() { CategoryId = 2, Description = "Routers & Data Cards", Status = "Active" },
                new ProductViewModel() { CategoryId = 2, Description = "Monitors", Status = "Active" },
                new ProductViewModel() { CategoryId = 2, Description = "Software", Status = "Active" },
                new ProductViewModel() { CategoryId = 2, Description = "Computer Accessories", Status = "Active" },
                new ProductViewModel() { CategoryId = 2, Description = "Office Equipment", Status = "Active" },
                new ProductViewModel() { CategoryId = 3, Description = "Gaming Equipment", Status = "Active" },
                new ProductViewModel() { CategoryId = 4, Description = "Televisions", Status = "Active" },
                new ProductViewModel() { CategoryId = 4, Description = "Audio & Video", Status = "Active" },
                new ProductViewModel() { CategoryId = 4, Description = "Large Appliances", Status = "Active" },
                new ProductViewModel() { CategoryId = 4, Description = "Home Appliances", Status = "Active" },
                new ProductViewModel() { CategoryId = 4, Description = "Grooming Appliances", Status = "Active" },
                new ProductViewModel() { CategoryId = 4, Description = "Cameras", Status = "Active" },
                new ProductViewModel() { CategoryId = 4, Description = "Kitchen Appliances", Status = "Active" },
                new ProductViewModel() { CategoryId = 5, Description = "Western Wear", Status = "Active" },
                new ProductViewModel() { CategoryId = 5, Description = "Footwear", Status = "Active" },
                new ProductViewModel() { CategoryId = 5, Description = "Handbags", Status = "Active" },
                new ProductViewModel() { CategoryId = 5, Description = "Eyewear", Status = "Active" },
                new ProductViewModel() { CategoryId = 5, Description = "Watches", Status = "Active" },
                new ProductViewModel() { CategoryId = 5, Description = "Precious Jewellery", Status = "Active" },
                new ProductViewModel() { CategoryId = 5, Description = "Fashion Jewellery", Status = "Active" },
                new ProductViewModel() { CategoryId = 5, Description = "Perfumes", Status = "Active" },
                new ProductViewModel() { CategoryId = 5, Description = "Accessories", Status = "Active" },
                new ProductViewModel() { CategoryId = 6, Description = "Footwear", Status = "Active" },
                new ProductViewModel() { CategoryId = 6, Description = "Bags & Luggages", Status = "Active" },
                new ProductViewModel() { CategoryId = 6, Description = "Clothing", Status = "Active" },
                new ProductViewModel() { CategoryId = 6, Description = "Watches", Status = "Active" },
                new ProductViewModel() { CategoryId = 6, Description = "Accessories", Status = "Active" },
                new ProductViewModel() { CategoryId = 6, Description = "Men's Grooming", Status = "Active" },
                new ProductViewModel() { CategoryId = 6, Description = "Men's Jewellery", Status = "Active" },
                new ProductViewModel() { CategoryId = 6, Description = "Eyewear", Status = "Active" },
                new ProductViewModel() { CategoryId = 7, Description = "Girls Clothing (2-8 Yrs.)", Status = "Active" },
                new ProductViewModel() { CategoryId = 7, Description = "Girls Clothing (8-14 Yrs.)", Status = "Active" },
                new ProductViewModel() { CategoryId = 7, Description = "Boys Clothing (2-8 Yrs.)", Status = "Active" },
                new ProductViewModel() { CategoryId = 7, Description = "Boyss Clothing (8-14 Yrs.)", Status = "Active" },
                new ProductViewModel() { CategoryId = 7, Description = "Girls Clothing (8-14 Yrs.)", Status = "Active" },
                new ProductViewModel() { CategoryId = 7, Description = "Baby Clothing", Status = "Active" },
                new ProductViewModel() { CategoryId = 7, Description = "Kids Footwear", Status = "Active" },
                new ProductViewModel() { CategoryId = 7, Description = "Watches & Accessories", Status = "Active" },
                new ProductViewModel() { CategoryId = 8, Description = "Kitchen Appliances", Status = "Active" },
                new ProductViewModel() { CategoryId = 8, Description = "Kitchenware", Status = "Active" },
                new ProductViewModel() { CategoryId = 8, Description = "Home Furnishing", Status = "Active" },
                new ProductViewModel() { CategoryId = 8, Description = "Home Décor", Status = "Active" },
                new ProductViewModel() { CategoryId = 8, Description = "Home Improvement", Status = "Active" },
                new ProductViewModel() { CategoryId = 8, Description = "Tools & Hardware", Status = "Active" },
                new ProductViewModel() { CategoryId = 8, Description = "Pet Supplies", Status = "Active" },
                new ProductViewModel() { CategoryId = 9, Description = "Sports", Status = "Active" },
                new ProductViewModel() { CategoryId = 9, Description = "Fitness Equipment", Status = "Active" },
                new ProductViewModel() { CategoryId = 9, Description = "Fitness Accessories", Status = "Active" },
                new ProductViewModel() { CategoryId = 9, Description = "Fitness Gadgets", Status = "Active" },
                new ProductViewModel() { CategoryId = 9, Description = "Nutrition & Supplements", Status = "Active" },
                new ProductViewModel() { CategoryId = 9, Description = "Bags & Luggages", Status = "Active" }
                );

            #endregion Product

            ////Do not use!! Not Complete
            //context.Items.AddOrUpdate(
            //    p => p.ItemId,
            //    new ItemViewModel() { ProductId = 1, Description = "Smartphones", Status = "Active" },
            //    new ItemViewModel() { ProductId = 1, Description = "Feature Phones", Status = "Active" },
            //    new ItemViewModel() { ProductId = 1, Description = "4G Phones", Status = "Active" },
            //    new ItemViewModel() { ProductId = 1, Description = "Smartphones", Status = "Active" },
            //    new ItemViewModel() { ProductId = 2, Description = "Screen Guards", Status = "Active" },
            //    new ItemViewModel() { ProductId = 2, Description = "Cables & Chargers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 2, Description = "Insurance & Warranty", Status = "Active" },
            //    new ItemViewModel() { ProductId = 2, Description = "Selfie Sticks and Stands", Status = "Active" },
            //    new ItemViewModel() { ProductId = 2, Description = "Smartphones", Status = "Active" },
            //    new ItemViewModel() { ProductId = 3, Description = "Smartphones", Status = "Active" },
            //    new ItemViewModel() { ProductId = 3, Description = "Printed Back Covers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 3, Description = "Flip Covers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 3, Description = "Plain Back Covers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 4, Description = "Above 9000mAh", Status = "Active" },
            //    new ItemViewModel() { ProductId = 4, Description = "5000 - 9000mAh", Status = "Active" },
            //    new ItemViewModel() { ProductId = 4, Description = "2000 - 5000mAh", Status = "Active" },
            //    new ItemViewModel() { ProductId = 4, Description = "Above 9000mAh", Status = "Active" },
            //    new ItemViewModel() { ProductId = 5, Description = "Up to 16 GB", Status = "Active" },
            //    new ItemViewModel() { ProductId = 5, Description = "32 GB & Above", Status = "Active" },
            //    new ItemViewModel() { ProductId = 6, Description = "Wifi Tablets", Status = "Active" },
            //    new ItemViewModel() { ProductId = 6, Description = "2G Calling", Status = "Active" },
            //    new ItemViewModel() { ProductId = 6, Description = "3G Calling", Status = "Active" },
            //    new ItemViewModel() { ProductId = 6, Description = "4G", Status = "Active" },
            //    new ItemViewModel() { ProductId = 7, Description = "Case & Covers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 7, Description = "Keyboards", Status = "Active" },
            //    new ItemViewModel() { ProductId = 7, Description = "Screen Guards", Status = "Active" },
            //    new ItemViewModel() { ProductId = 8, Description = "2 in 1 Laptops", Status = "Active" },
            //    new ItemViewModel() { ProductId = 8, Description = "MacBooks", Status = "Active" },
            //    new ItemViewModel() { ProductId = 8, Description = "Notebooks", Status = "Active" },
            //    new ItemViewModel() { ProductId = 9, Description = "All in One", Status = "Active" },
            //    new ItemViewModel() { ProductId = 9, Description = "Tower Desktops", Status = "Active" },
            //    new ItemViewModel() { ProductId = 9, Description = "Mini Desktops", Status = "Active" },
            //    new ItemViewModel() { ProductId = 10, Description = "CPU", Status = "Active" },
            //    new ItemViewModel() { ProductId = 10, Description = "Motherboards", Status = "Active" },
            //    new ItemViewModel() { ProductId = 10, Description = "RAM", Status = "Active" },
            //    new ItemViewModel() { ProductId = 10, Description = "Graphic Card", Status = "Active" },
            //    new ItemViewModel() { ProductId = 10, Description = "Internal Hard Drive", Status = "Active" },
            //    new ItemViewModel() { ProductId = 10, Description = "CPU Coolers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 10, Description = "Cabinet", Status = "Active" },
            //    new ItemViewModel() { ProductId = 11, Description = "Printers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 11, Description = "Scanners", Status = "Active" }

            //    );
        }
    }
}