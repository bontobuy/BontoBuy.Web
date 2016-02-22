namespace BontoBuy.Web.Migrations
{
    using BontoBuy.Web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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

            //#region Category

            //context.Categories.AddOrUpdate(
            //    p => p.CategoryId,
            //    new CategoryViewModel { Description = "Mobiles & Tablets", Status = "Active" },
            //    new CategoryViewModel { Description = "Computers", Status = "Active" },
            //    new CategoryViewModel { Description = "Gaming", Status = "Active" },
            //    new CategoryViewModel { Description = "Electronics", Status = "Active" },
            //    new CategoryViewModel { Description = "Women's Fashion", Status = "Active" },
            //    new CategoryViewModel { Description = "Men's Fashion", Status = "Active" },
            //    new CategoryViewModel { Description = "Kids' Fashion", Status = "Active" },
            //    new CategoryViewModel { Description = "Home & Kitchen", Status = "Active" },
            //    new CategoryViewModel { Description = "Sport, Fitness & Outdoor", Status = "Active" }
            //    );

            //#endregion Category

            //#region Product

            //context.Products.AddOrUpdate(
            //    p => p.ProductId,
            //    new ProductViewModel() { CategoryId = 1, Description = "Mobile Phones", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 1, Description = "Mobile Accessories", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 1, Description = "Mobile Case & Covers", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 1, Description = "Power Banks", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 1, Description = "Mobile Memory Cards", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 1, Description = "Tablets", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 1, Description = "Tablet Accessories", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 2, Description = "Laptops", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 2, Description = "Desktops", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 2, Description = "Computer Components", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 2, Description = "Printers & Inks", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 2, Description = "Storage", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 2, Description = "Routers & Data Cards", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 2, Description = "Monitors", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 2, Description = "Software", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 2, Description = "Computer Accessories", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 2, Description = "Office Equipment", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 3, Description = "Gaming Equipment", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 4, Description = "Televisions", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 4, Description = "Audio & Video", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 4, Description = "Large Appliances", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 4, Description = "Home Appliances", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 4, Description = "Grooming Appliances", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 4, Description = "Cameras", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 4, Description = "Kitchen Appliances", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 5, Description = "Western Wear", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 5, Description = "Footwear", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 5, Description = "Handbags", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 5, Description = "Eyewear", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 5, Description = "Watches", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 5, Description = "Precious Jewellery", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 5, Description = "Fashion Jewellery", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 5, Description = "Perfumes", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 5, Description = "Accessories", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 6, Description = "Footwear", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 6, Description = "Bags & Luggages", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 6, Description = "Clothing", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 6, Description = "Watches", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 6, Description = "Accessories", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 6, Description = "Men's Grooming", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 6, Description = "Men's Jewellery", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 6, Description = "Eyewear", Status = "Active" }

            //    //new ProductViewModel() { CategoryId = 7, Description = "Girls Clothing (2-8 Yrs.)", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 7, Description = "Girls Clothing (8-14 Yrs.)", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 7, Description = "Boys Clothing (2-8 Yrs.)", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 7, Description = "Boyss Clothing (8-14 Yrs.)", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 7, Description = "Girls Clothing (8-14 Yrs.)", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 7, Description = "Baby Clothing", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 7, Description = "Kids Footwear", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 7, Description = "Watches & Accessories", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 8, Description = "Kitchen Appliances", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 8, Description = "Kitchenware", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 8, Description = "Home Furnishing", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 8, Description = "Home D�cor", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 8, Description = "Home Improvement", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 8, Description = "Tools & Hardware", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 8, Description = "Pet Supplies", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 9, Description = "Sports", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 9, Description = "Fitness Equipment", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 9, Description = "Fitness Accessories", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 9, Description = "Fitness Gadgets", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 9, Description = "Nutrition & Supplements", Status = "Active" },
            //    //new ProductViewModel() { CategoryId = 9, Description = "Bags & Luggages", Status = "Active" }
            //    );

            //#endregion Product

            //Do not use!! Not Complete
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
            //    new ItemViewModel() { ProductId = 11, Description = "Scanners", Status = "Active" },
            //    new ItemViewModel() { ProductId = 12, Description = "LED Monitors", Status = "Active" },
            //    new ItemViewModel() { ProductId = 13, Description = "Security", Status = "Active" },
            //    new ItemViewModel() { ProductId = 13, Description = "Office", Status = "Active" },
            //    new ItemViewModel() { ProductId = 13, Description = "Operating System", Status = "Active" },
            //    new ItemViewModel() { ProductId = 14, Description = "Keyboards", Status = "Active" },
            //    new ItemViewModel() { ProductId = 14, Description = "Mouse", Status = "Active" },
            //    new ItemViewModel() { ProductId = 14, Description = "Webcams", Status = "Active" },
            //    new ItemViewModel() { ProductId = 14, Description = "Laptop Skins", Status = "Active" },
            //    new ItemViewModel() { ProductId = 14, Description = "Cooling Pads", Status = "Active" },
            //    new ItemViewModel() { ProductId = 14, Description = "Converters", Status = "Active" },
            //    new ItemViewModel() { ProductId = 14, Description = "Converters", Status = "Active" },
            //    new ItemViewModel() { ProductId = 14, Description = "Surge Protectors", Status = "Active" },
            //    new ItemViewModel() { ProductId = 14, Description = "Warranty Insurance", Status = "Active" },
            //    new ItemViewModel() { ProductId = 15, Description = "Office Security", Status = "Active" },
            //    new ItemViewModel() { ProductId = 15, Description = "Note Counters", Status = "Active" },
            //    new ItemViewModel() { ProductId = 15, Description = "Point Of Sale Equipment", Status = "Active" },
            //    new ItemViewModel() { ProductId = 15, Description = "Vending Machines", Status = "Active" },
            //    new ItemViewModel() { ProductId = 16, Description = "Gaming Consoles", Status = "Active" },
            //    new ItemViewModel() { ProductId = 16, Description = "Digital Games", Status = "Active" },
            //    new ItemViewModel() { ProductId = 16, Description = "Gaming Accessories", Status = "Active" },
            //    new ItemViewModel() { ProductId = 16, Description = "Gaming Titles", Status = "Active" },
            //    new ItemViewModel() { ProductId = 17, Description = "Full HD TV's", Status = "Active" },
            //    new ItemViewModel() { ProductId = 17, Description = "Smart TV's", Status = "Active" },
            //    new ItemViewModel() { ProductId = 18, Description = "Home Theaters", Status = "Active" },
            //    new ItemViewModel() { ProductId = 18, Description = "Speakers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 18, Description = "Headphones & Earphones", Status = "Active" },
            //    new ItemViewModel() { ProductId = 18, Description = "Audio & Video Accessories", Status = "Active" },
            //    new ItemViewModel() { ProductId = 18, Description = "Video Players", Status = "Active" },
            //    new ItemViewModel() { ProductId = 18, Description = "Projectors", Status = "Active" },
            //    new ItemViewModel() { ProductId = 19, Description = "Air Conditioners", Status = "Active" },
            //    new ItemViewModel() { ProductId = 19, Description = "Air Coolers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 19, Description = "Washing Machines & Dryers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 19, Description = "Refrigerators", Status = "Active" },
            //    new ItemViewModel() { ProductId = 19, Description = "Microwaves", Status = "Active" },
            //    new ItemViewModel() { ProductId = 19, Description = "Fans", Status = "Active" },
            //    new ItemViewModel() { ProductId = 19, Description = "Dishwashers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 20, Description = "Vacuum Cleaners", Status = "Active" },
            //    new ItemViewModel() { ProductId = 20, Description = "Irons", Status = "Active" },
            //    new ItemViewModel() { ProductId = 20, Description = " Security", Status = "Active" },
            //    new ItemViewModel() { ProductId = 20, Description = "Sewing Machines", Status = "Active" },
            //    new ItemViewModel() { ProductId = 21, Description = "Trimmers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 21, Description = "Shavers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 21, Description = "Clippers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 21, Description = "Grooming Kits", Status = "Active" },
            //    new ItemViewModel() { ProductId = 21, Description = "Body Groomers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 21, Description = "Hair Dryers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 21, Description = "Hair Straigteners", Status = "Active" },
            //    new ItemViewModel() { ProductId = 21, Description = "Epilators", Status = "Active" },
            //    new ItemViewModel() { ProductId = 21, Description = "Hair Stylers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 21, Description = "Facial Care", Status = "Active" },
            //    new ItemViewModel() { ProductId = 22, Description = "Digital Cameras", Status = "Active" },
            //    new ItemViewModel() { ProductId = 22, Description = "Digital Cameras", Status = "Active" },
            //    new ItemViewModel() { ProductId = 22, Description = "Carmcorders", Status = "Active" },
            //    new ItemViewModel() { ProductId = 22, Description = "Camera Lenses", Status = "Active" },
            //    new ItemViewModel() { ProductId = 22, Description = "Camera Accessories", Status = "Active" },
            //    new ItemViewModel() { ProductId = 22, Description = "Selfie Sticks", Status = "Active" },
            //    new ItemViewModel() { ProductId = 22, Description = "Binoculars and Telescope", Status = "Active" },
            //    new ItemViewModel() { ProductId = 22, Description = "Digital Photo Frames", Status = "Active" },
            //    new ItemViewModel() { ProductId = 24, Description = "Tops & Tunics", Status = "Active" },
            //    new ItemViewModel() { ProductId = 24, Description = "Tees & Polos", Status = "Active" },
            //    new ItemViewModel() { ProductId = 24, Description = "Shirts", Status = "Active" },
            //    new ItemViewModel() { ProductId = 24, Description = "Dresses", Status = "Active" },
            //    new ItemViewModel() { ProductId = 24, Description = "Jeans", Status = "Active" },
            //    new ItemViewModel() { ProductId = 24, Description = "Jeggings & Palazzos", Status = "Active" },
            //    new ItemViewModel() { ProductId = 24, Description = "Innerwear & Nightwear", Status = "Active" },
            //    new ItemViewModel() { ProductId = 24, Description = "Winterwear", Status = "Active" },
            //    new ItemViewModel() { ProductId = 25, Description = "Sport shoes", Status = "Active" },
            //    new ItemViewModel() { ProductId = 25, Description = "Heels", Status = "Active" },
            //    new ItemViewModel() { ProductId = 25, Description = "Wedges", Status = "Active" },
            //    new ItemViewModel() { ProductId = 25, Description = "Stilettos", Status = "Active" },
            //    new ItemViewModel() { ProductId = 25, Description = "Casual Shoes", Status = "Active" },
            //    new ItemViewModel() { ProductId = 25, Description = "Sandals", Status = "Active" },
            //    new ItemViewModel() { ProductId = 25, Description = "Balerinas", Status = "Active" },
            //    new ItemViewModel() { ProductId = 25, Description = "Boots", Status = "Active" },
            //    new ItemViewModel() { ProductId = 26, Description = "Handbags", Status = "Active" },
            //    new ItemViewModel() { ProductId = 26, Description = "Clutches", Status = "Active" },
            //    new ItemViewModel() { ProductId = 26, Description = "Wallets", Status = "Active" },
            //    new ItemViewModel() { ProductId = 27, Description = "Sunglasses", Status = "Active" },
            //    new ItemViewModel() { ProductId = 27, Description = "Spectacle Frames", Status = "Active" },
            //    new ItemViewModel() { ProductId = 29, Description = "Rings", Status = "Active" },
            //    new ItemViewModel() { ProductId = 29, Description = "Earrings", Status = "Active" },
            //    new ItemViewModel() { ProductId = 29, Description = "Gold Coins", Status = "Active" },
            //    new ItemViewModel() { ProductId = 29, Description = "Silver Jewellery", Status = "Active" },
            //    new ItemViewModel() { ProductId = 30, Description = "Necklaces", Status = "Active" },
            //    new ItemViewModel() { ProductId = 30, Description = "Earrings", Status = "Active" },
            //    new ItemViewModel() { ProductId = 30, Description = "Bracelets", Status = "Active" },
            //    new ItemViewModel() { ProductId = 30, Description = "Pendants", Status = "Active" },
            //    new ItemViewModel() { ProductId = 31, Description = "Perfume", Status = "Active" },
            //    new ItemViewModel() { ProductId = 31, Description = "Deodorants", Status = "Active" },
            //    new ItemViewModel() { ProductId = 32, Description = "Belts", Status = "Active" },
            //    new ItemViewModel() { ProductId = 32, Description = "Socks & Stokings", Status = "Active" },
            //    new ItemViewModel() { ProductId = 32, Description = "Scarves", Status = "Active" },
            //    new ItemViewModel() { ProductId = 33, Description = "Sport Shoes", Status = "Active" },
            //    new ItemViewModel() { ProductId = 33, Description = "Running Shoes", Status = "Active" },
            //    new ItemViewModel() { ProductId = 33, Description = "Casual Shoes", Status = "Active" },
            //    new ItemViewModel() { ProductId = 33, Description = "Boots", Status = "Active" },
            //    new ItemViewModel() { ProductId = 33, Description = "Sandals", Status = "Active" },
            //    new ItemViewModel() { ProductId = 33, Description = "Loafers", Status = "Active" },
            //    new ItemViewModel() { ProductId = 33, Description = "Socks", Status = "Active" },
            //    new ItemViewModel() { ProductId = 34, Description = "Backpacks", Status = "Active" },
            //    new ItemViewModel() { ProductId = 34, Description = "Laptop Bags", Status = "Active" },
            //    new ItemViewModel() { ProductId = 34, Description = "Hiking Bags", Status = "Active" },
            //    new ItemViewModel() { ProductId = 34, Description = "Office Bags", Status = "Active" },
            //    new ItemViewModel() { ProductId = 35, Description = "Shirts", Status = "Active" },
            //    new ItemViewModel() { ProductId = 35, Description = "T-Shirts", Status = "Active" },
            //    new ItemViewModel() { ProductId = 35, Description = "T-Shirts", Status = "Active" },
            //    new ItemViewModel() { ProductId = 35, Description = "Jeans", Status = "Active" },
            //    new ItemViewModel() { ProductId = 36, Description = "Men's Watches", Status = "Active" },
            //    new ItemViewModel() { ProductId = 36, Description = "Smart Watches", Status = "Active" },
            //    new ItemViewModel() { ProductId = 37, Description = "Belts", Status = "Active" },
            //    new ItemViewModel() { ProductId = 37, Description = "Wallets", Status = "Active" },
            //    new ItemViewModel() { ProductId = 38, Description = "Razor", Status = "Active" },
            //    new ItemViewModel() { ProductId = 38, Description = "Shaving Cream", Status = "Active" },
            //    new ItemViewModel() { ProductId = 39, Description = "Necklaces", Status = "Active" },
            //    new ItemViewModel() { ProductId = 40, Description = "Sunglasses", Status = "Active" }
            //    );

            context.SpecialCategories.AddOrUpdate(
                p => p.SpecialCatId,
                new SpecialCategoryViewModel() { Description = "Box Content", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "General", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "Display", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "Software", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "Camera", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "Connectivity", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "Processor", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "Memory & Storage", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "Hardware", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "Guarantee", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "Hardware Connectivity", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "Battery & Power", Status = "Active" },
                new SpecialCategoryViewModel() { Description = "Warranty", Status = "Active" }

                );
        }
    }
}