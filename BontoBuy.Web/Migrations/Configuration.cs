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
            //    new ProductViewModel() { CategoryId = 6, Description = "Eyewear", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 7, Description = "Girls Clothing (2-8 Yrs.)", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 7, Description = "Girls Clothing (8-14 Yrs.)", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 7, Description = "Boys Clothing (2-8 Yrs.)", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 7, Description = "Boys Clothing (8-14 Yrs.)", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 7, Description = "Girls Clothing (8-14 Yrs.)", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 7, Description = "Baby Clothing", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 7, Description = "Kids Footwear", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 7, Description = "Watches & Accessories", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 8, Description = "Kitchen Appliances", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 8, Description = "Kitchenware", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 8, Description = "Home Furnishing", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 8, Description = "Home Décor", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 8, Description = "Home Improvement", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 8, Description = "Tools & Hardware", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 8, Description = "Pet Supplies", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 9, Description = "Sports", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 9, Description = "Fitness Equipment", Status = "Active" },

            //    new ProductViewModel() { CategoryId = 9, Description = "Fitness Accessories", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 9, Description = "Fitness Gadgets", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 9, Description = "Nutrition & Supplements", Status = "Active" },
            //    new ProductViewModel() { CategoryId = 9, Description = "Bags & Luggages", Status = "Active" }
            //    );

            //#endregion Product

            //context.Items.AddOrUpdate(
            //    p => p.ItemId,
            //    new ItemViewModel() { ProductId = 1, Description = "Smartphones", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 1, Description = "Feature Phones", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 1, Description = "4G Phones", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 2, Description = "Screen Guards", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 2, Description = "Cables & Chargers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 2, Description = "Insurance & Warranty", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 2, Description = "Selfie Sticks and Stands", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 3, Description = "Printed Back Covers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 3, Description = "Flip Covers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 3, Description = "Plain Back Covers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 4, Description = "Above 9000mAh", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 4, Description = "5000 - 9000mAh", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 4, Description = "2000 - 5000mAh", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 4, Description = "Above 9000mAh", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 5, Description = "Up to 16 GB", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 5, Description = "32 GB & Above", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 6, Description = "Wifi Tablets", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 6, Description = "2G Calling", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 6, Description = "3G Calling", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 6, Description = "4G", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 7, Description = "Case & Covers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 7, Description = "Keyboards", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 7, Description = "Screen Guards", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 8, Description = "2 in 1 Laptops", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 8, Description = "MacBooks", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 8, Description = "Notebooks", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 9, Description = "All in One", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 9, Description = "Tower Desktops", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 9, Description = "Mini Desktops", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 10, Description = "CPU", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 10, Description = "Motherboards", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 10, Description = "RAM", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 10, Description = "Graphic Card", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 10, Description = "Internal Hard Drive", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 10, Description = "CPU Coolers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 10, Description = "Cabinet", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 11, Description = "Printers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 11, Description = "Scanners", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 12, Description = "LED Monitors", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 13, Description = "Security", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 13, Description = "Office", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 13, Description = "Operating System", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 14, Description = "Keyboards", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 14, Description = "Mouse", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 14, Description = "Webcams", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 14, Description = "Laptop Skins", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 14, Description = "Cooling Pads", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 14, Description = "Converters", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 14, Description = "Converters", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 14, Description = "Surge Protectors", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 14, Description = "Warranty Insurance", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 15, Description = "Office Security", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 15, Description = "Note Counters", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 15, Description = "Point Of Sale Equipment", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 15, Description = "Vending Machines", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 16, Description = "Gaming Consoles", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 16, Description = "Digital Games", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 16, Description = "Gaming Accessories", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 16, Description = "Gaming Titles", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 17, Description = "Full HD TV's", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 17, Description = "Smart TV's", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 18, Description = "Home Theaters", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 18, Description = "Speakers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 18, Description = "Headphones & Earphones", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 18, Description = "Audio & Video Accessories", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 18, Description = "Video Players", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 18, Description = "Projectors", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 19, Description = "Air Conditioners", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 19, Description = "Air Coolers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 19, Description = "Washing Machines & Dryers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 19, Description = "Refrigerators", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 19, Description = "Microwaves", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 19, Description = "Fans", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 19, Description = "Dishwashers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 20, Description = "Vacuum Cleaners", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 20, Description = "Irons", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 20, Description = " Security", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 20, Description = "Sewing Machines", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 21, Description = "Trimmers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 21, Description = "Shavers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 21, Description = "Clippers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 21, Description = "Grooming Kits", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 21, Description = "Body Groomers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 21, Description = "Hair Dryers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 21, Description = "Hair Straigteners", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 21, Description = "Epilators", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 21, Description = "Hair Stylers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 21, Description = "Facial Care", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 22, Description = "Digital Cameras", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 22, Description = "Digital Cameras", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 22, Description = "Carmcorders", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 22, Description = "Camera Lenses", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 22, Description = "Camera Accessories", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 22, Description = "Selfie Sticks", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 22, Description = "Binoculars and Telescope", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 22, Description = "Digital Photo Frames", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 24, Description = "Tops & Tunics", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 24, Description = "Tees & Polos", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 24, Description = "Shirts", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 24, Description = "Dresses", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 24, Description = "Jeans", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 24, Description = "Jeggings & Palazzos", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 24, Description = "Innerwear & Nightwear", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 24, Description = "Winterwear", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 25, Description = "Sport shoes", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 25, Description = "Heels", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 25, Description = "Wedges", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 25, Description = "Stilettos", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 25, Description = "Casual Shoes", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 25, Description = "Sandals", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 25, Description = "Balerinas", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 25, Description = "Boots", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 26, Description = "Handbags", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 26, Description = "Clutches", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 26, Description = "Wallets", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 27, Description = "Sunglasses", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 27, Description = "Spectacle Frames", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 29, Description = "Rings", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 29, Description = "Earrings", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 29, Description = "Gold Coins", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 29, Description = "Silver Jewellery", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 30, Description = "Necklaces", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 30, Description = "Earrings", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 30, Description = "Bracelets", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 30, Description = "Pendants", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 31, Description = "Perfume", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 31, Description = "Deodorants", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 32, Description = "Belts", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 32, Description = "Socks & Stokings", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 32, Description = "Scarves", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 33, Description = "Sport Shoes", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 33, Description = "Running Shoes", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 33, Description = "Casual Shoes", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 33, Description = "Boots", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 33, Description = "Sandals", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 33, Description = "Loafers", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 33, Description = "Socks", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 34, Description = "Backpacks", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 34, Description = "Laptop Bags", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 34, Description = "Hiking Bags", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 34, Description = "Office Bags", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 35, Description = "Shirts", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 35, Description = "T-Shirts", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 35, Description = "Jeans", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 36, Description = "Men's Watches", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 36, Description = "Smart Watches", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 37, Description = "Belts", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 37, Description = "Wallets", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 38, Description = "Razor", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 38, Description = "Shaving Cream", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 39, Description = "Necklaces", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now },
            //    new ItemViewModel() { ProductId = 40, Description = "Sunglasses", AdminStatus = "Active", Action = "Catalog", Controller = "Home", DtCreated = DateTime.Now, DtUpdated = DateTime.Now }
            //    );

            //context.SpecialCategories.AddOrUpdate(
            //    p => p.SpecialCatId,
            //    new SpecialCategoryViewModel() { Description = "Box Content", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "General", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "Display", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "Software", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "Camera", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "Connectivity", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "Processor", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "Memory & Storage", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "Hardware", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "Hardware Connectivity", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "Battery & Power", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "Dimensions", Status = "Active" },
            //    new SpecialCategoryViewModel() { Description = "Warranty", Status = "Active" }

            //    );

            //context.Specifications.AddOrUpdate(
            //    p => p.SpecificationId,
            //    new SpecificationViewModel() { SpecialCatId = 1, Description = "Box Contents", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 2, Description = "Brand", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 2, Description = "Model", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 2, Description = "Form", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 2, Description = "SIMs", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 2, Description = "SIM Size", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 2, Description = "Color", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 2, Description = "Other Features", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 2, Description = "Call Features", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 3, Description = "Screen Size", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 3, Description = "Display Type", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 3, Description = "Screen Protection", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 3, Description = "Pixel Density", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 3, Description = "MultiTouch", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 3, Description = "Other Screen Features", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 4, Description = "OS Version", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 4, Description = "Pre Installed Apps", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 4, Description = "Multi Language Supported", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 5, Description = "Rear Camera", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 5, Description = "Auto Focus", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 5, Description = "Flash", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 5, Description = "Front Camera", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 5, Description = "Other Camera Features", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 6, Description = "GSM", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 6, Description = "CDMA", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 6, Description = "3G/WCDMA", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 6, Description = "4G/LTE", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 7, Description = "Processor Speed", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 7, Description = "Processor Cores", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 7, Description = "Processor Brand", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 8, Description = "RAM", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 8, Description = "Internal Memory", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 8, Description = "User Memory", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 8, Description = "Expandable Memory", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 8, Description = "Memory Card Slot", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 9, Description = "Accelerometer", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 9, Description = "Compass", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 9, Description = "Proximity", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 9, Description = "Gyro-Sensor", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 10, Description = "Bluetooth A2DP", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 10, Description = "Audio Jack", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 10, Description = "SAR Value", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 10, Description = "FM Radio", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 11, Description = "Battery Capacity", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 11, Description = "Battery Type", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 11, Description = "Replaceable Battery", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 11, Description = "Talk Time", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 12, Description = "HeightxWidthxHeight", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 12, Description = "Weight", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 13, Description = "Warranty Type", Status = "Active" },
            //    new SpecificationViewModel() { SpecialCatId = 13, Description = "Warranty Duration", Status = "Active" }
            //    );

            //context.Brands.AddOrUpdate(
            //    p => p.BrandId,
            //    new BrandViewModel() { Name = "Samsung", Status = "Active" },
            //    new BrandViewModel() { Name = "Apple", Status = "Active" },
            //    new BrandViewModel() { Name = "Lenovo", Status = "Active" },
            //    new BrandViewModel() { Name = "Acer", Status = "Active" },
            //    new BrandViewModel() { Name = "Nokia", Status = "Active" },
            //    new BrandViewModel() { Name = "Dell", Status = "Active" },
            //    new BrandViewModel() { Name = "BlackBerry", Status = "Active" },
            //    new BrandViewModel() { Name = "HP", Status = "Active" },
            //    new BrandViewModel() { Name = "Yezz", Status = "Active" },
            //    new BrandViewModel() { Name = "Blue", Status = "Active" },
            //    new BrandViewModel() { Name = "Xiaomi", Status = "Active" },
            //    new BrandViewModel() { Name = "Htc", Status = "Active" },
            //    new BrandViewModel() { Name = "Sony", Status = "Active" }
            //    );

            //context.OrderStatuses.AddOrUpdate(
            //    p => p.OrderStatusId,
            //    new OrderStatusViewModel() { Name = "Processing" },
            //    new OrderStatusViewModel() { Name = "In transit" },
            //    new OrderStatusViewModel() { Name = "Delivered" }
            //    );

            //context.Ratings.AddOrUpdate(
            //    p => p.RatingId,
            //    new RatingViewModel() { RatingNumber = 2 },
            //    new RatingViewModel() { RatingNumber = 3 },
            //    new RatingViewModel() { RatingNumber = 4 },
            //    new RatingViewModel() { RatingNumber = 5 }
            //    );

            //context.DeliveryAddressStatuses.AddOrUpdate(
            //    p => p.DeliveryAddressStatusId,
            //    new DeliveryAddressStatusViewModel() { Status = "Default" },
            //    new DeliveryAddressStatusViewModel() { Status = "Other" }
            //    );

            //context.PaymentStatuses.AddOrUpdate(
            //    p => p.PaymentStatusId,
            //    new PaymentStatusViewModel() { Status = "Processing" },
            //    new PaymentStatusViewModel() { Status = "Paid" }
            //    );

            //context.ReturnStatuses.AddOrUpdate(
            //    p => p.ReturnStatusId,
            //    new ReturnStatusViewModel() { Status = "Processing" },
            //    new ReturnStatusViewModel() { Status = "Validated" }
            //    );

            //context.ProductSpecs.AddOrUpdate(
            //    p => p.SpecificationId,
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 1 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 2 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 3 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 4 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 5 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 6 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 7 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 8 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 9 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 10 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 11 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 12 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 13 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 14 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 15 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 16 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 17 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 18 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 19 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 20 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 21 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 22 },
            //    new ProductSpecViewModel() { ProductId = 1, SpecificationId = 23 }

            //new ProductSpecViewModel() { ProductId = 2, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 3, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 4, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 5, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 6, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 7, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 8, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 9, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 10, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 11, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 12, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 13, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 14, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 15, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 16, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 17, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 18, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 19, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 20, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 21, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 22, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 23, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 24, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 25, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 26, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 27, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 28, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 29, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 30, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 31, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 32, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 33, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 34, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 35, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 36, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 37, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 38, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 39, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 40, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 41, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 42, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 43, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 44, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 45, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 46, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 47, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 48, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 49, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 50, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 51, SpecificationId = 2 },
            //new ProductSpecViewModel() { ProductId = 52, SpecificationId = 2 }
            //);

            //context.Models.AddOrUpdate(
            //    p => p.ModelId,
            //    new ModelViewModel()
            //    {
            //        ItemId = 27,
            //        ModelNumber = "Acer E15",
            //        BrandId = 4,
            //        Status = "Active",
            //        UserId = "dfb8e1ac-9755-4b4f-8656-e456d920752a",
            //        SupplierId = 7,
            //        Price = 29500,
            //        DtCreated = DateTime.Now.AddDays(-11),
            //        DeliveryInDays = 5,
            //        NumberDaysToAdvert = 12
            //    },
            //     new ModelViewModel()
            //    {
            //        ItemId = 27,
            //        ModelNumber = "Acer Aspire 17",
            //        BrandId = 4,
            //        Status = "Active",
            //        UserId = "dfb8e1ac-9755-4b4f-8656-e456d920752a",
            //        SupplierId = 7,
            //        Price = 39600,
            //        DtCreated = DateTime.Now.AddDays(-11),
            //        DeliveryInDays = 5,
            //        NumberDaysToAdvert = 10
            //    },
            //     new ModelViewModel()
            //    {
            //        ItemId = 27,
            //        ModelNumber = "Macbook Pro",
            //        BrandId = 2,
            //        Status = "Active",
            //        UserId = "dfb8e1ac-9755-4b4f-8656-e456d920752a",
            //        SupplierId = 7,
            //        Price = 45200,
            //        DtCreated = DateTime.Now.AddDays(-11),
            //        DeliveryInDays = 5,
            //        NumberDaysToAdvert = 10
            //    },
            //       new ModelViewModel()
            //    {
            //        ItemId = 27,
            //        ModelNumber = "HP Pavilion",
            //        BrandId = 8,
            //        Status = "Active",
            //        UserId = "4f8edcec-3dac-4f08-b154-97dfd848caeb",
            //        SupplierId = 5,
            //        Price = 44200,
            //        DtCreated = DateTime.Now.AddDays(-11),
            //        DeliveryInDays = 5,
            //        NumberDaysToAdvert = 10
            //    },
            //          new ModelViewModel()
            //    {
            //        ItemId = 27,
            //        ModelNumber = "Ideapad G50",
            //        BrandId = 3,
            //        Status = "Active",
            //        UserId = "4f8edcec-3dac-4f08-b154-97dfd848caeb",
            //        SupplierId = 5,
            //        Price = 33600,
            //        DtCreated = DateTime.Now.AddDays(-11),
            //        DeliveryInDays = 5,
            //        NumberDaysToAdvert = 10
            //    },
            //          new ModelViewModel()
            //    {
            //        ItemId = 27,
            //        ModelNumber = "XPS 13",
            //        BrandId = 6,
            //        Status = "Active",
            //        UserId = "4f8edcec-3dac-4f08-b154-97dfd848caeb",
            //        SupplierId = 7,
            //        Price = 26400,
            //        DtCreated = DateTime.Now.AddDays(-11),
            //        DeliveryInDays = 5,
            //        NumberDaysToAdvert = 10
            //    },
            //          new ModelViewModel()
            //    {
            //        ItemId = 27,
            //        ModelNumber = "XPS 15",
            //        BrandId = 6,
            //        Status = "Active",
            //        UserId = "4f8edcec-3dac-4f08-b154-97dfd848caeb",
            //        SupplierId = 7,
            //        Price = 33600,
            //        DtCreated = DateTime.Now.AddDays(-11),
            //        DeliveryInDays = 5,
            //        NumberDaysToAdvert = 10
            //    }

            //);

            context.Orders.AddOrUpdate(
                p => p.OrderId,
                new OrderViewModel()
                {
                    DtCreated = DateTime.Now.AddDays(-15),
                    ExpectedDeliveryDate = DateTime.Now.AddDays(-10),
                    RealDeliveryDate = DateTime.Now.AddDays(-10),
                    Status = "Delivered",
                    Total = 35600,
                    GrandTotal = 35600,
                    SupplierId = 7,
                    CustomerId = 8,
                    ModelId = 12301,
                    CustomerUserId = "353ce547-9b5d-4536-8a37-8c2cff5339a2",
                    SupplierUserId = "3b819da6-11ca-4c33-8919-378ab5c87474",
                    Quantity = 1,
                    UnitPrice = 29500,
                    HasReturn = true,
                    Notification = "Yes"
                },

                new OrderViewModel()
                {
                    DtCreated = DateTime.Now.AddDays(-15),
                    ExpectedDeliveryDate = DateTime.Now.AddDays(-10),
                    RealDeliveryDate = DateTime.Now.AddDays(-10),
                    Status = "Delivered",
                    Total = 29500,
                    GrandTotal = 29500,
                    SupplierId = 1,
                    CustomerId = 8,
                    ModelId = 12301,
                    CustomerUserId = "353ce547-9b5d-4536-8a37-8c2cff5339a2",
                    SupplierUserId = "3b819da6-11ca-4c33-8919-378ab5c87474",
                    Quantity = 1,
                    UnitPrice = 29500,
                    HasReturn = true,
                    Notification = "Yes"
                },

                new OrderViewModel()
                {
                    DtCreated = DateTime.Now.AddDays(-15),
                    ExpectedDeliveryDate = DateTime.Now.AddDays(-10),
                    RealDeliveryDate = DateTime.Now.AddDays(-10),
                    Status = "Delivered",
                    Total = 29500,
                    GrandTotal = 29500,
                    SupplierId = 1,
                    CustomerId = 8,
                    ModelId = 12301,
                    CustomerUserId = "353ce547-9b5d-4536-8a37-8c2cff5339a2",
                    SupplierUserId = "3b819da6-11ca-4c33-8919-378ab5c87474",
                    Quantity = 1,
                    UnitPrice = 29500,
                    HasReturn = true,
                    Notification = "Yes"
                },
                new OrderViewModel()
                {
                    DtCreated = DateTime.Now.AddDays(-15),
                    ExpectedDeliveryDate = DateTime.Now.AddDays(-10),
                    RealDeliveryDate = DateTime.Now.AddDays(-10),
                    Status = "Delivered",
                    Total = 29500,
                    GrandTotal = 29500,
                    SupplierId = 1,
                    CustomerId = 8,
                    ModelId = 12301,
                    CustomerUserId = "353ce547-9b5d-4536-8a37-8c2cff5339a2",
                    SupplierUserId = "3b819da6-11ca-4c33-8919-378ab5c87474",
                    Quantity = 1,
                    UnitPrice = 29500,
                    HasReturn = true,
                    Notification = "Yes"
                },
                new OrderViewModel()
                {
                    DtCreated = DateTime.Now.AddDays(-15),
                    ExpectedDeliveryDate = DateTime.Now.AddDays(-10),
                    RealDeliveryDate = DateTime.Now.AddDays(-10),
                    Status = "Delivered",
                    Total = 29500,
                    GrandTotal = 29500,
                    SupplierId = 1,
                    CustomerId = 8,
                    ModelId = 12301,
                    CustomerUserId = "353ce547-9b5d-4536-8a37-8c2cff5339a2",
                    SupplierUserId = "3b819da6-11ca-4c33-8919-378ab5c87474",
                    Quantity = 1,
                    UnitPrice = 29500,
                    HasReturn = true,
                    Notification = "Yes"
                }

                );

            //context.Commissions.AddOrUpdate(
            //    p => p.CommissionId,
            //    new CommissionViewModel() { Percentage = 5, DtCreated = DateTime.UtcNow, DtUpdated = DateTime.UtcNow }
            //    );

            //context.ModelSpecs.AddOrUpdate(
            //    p => p.ModelSpecId,
            //        new ModelSpecViewModel() { },
            //    );
        }
    }
}