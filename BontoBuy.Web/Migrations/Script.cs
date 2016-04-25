namespace BontoBuy.Web.Migrations
{
    using BontoBuy.Web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Script : DbMigrationsConfiguration<BontoBuy.Web.Models.ApplicationDbContext>
    {
        public Script()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BontoBuy.Web.Models.ApplicationDbContext context)
        {
            //This method will be called after migrating to the latest version.

            //You can use the DbSet<T>.AddOrUpdate() helper extension method
            //to avoid creating duplicate seed data. E.g.

            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );

            //Instructions:
            //Just do the update-database and the db will be seeded
            //Do one table at a time!!!
            //Codes are placed in region for convenience

            context.SpecialCategories.AddOrUpdate(
                p => p.SpecialCatId,
                new SpecialCategoryViewModel() { Description = "Box Content", Status = "Active", Position = "Top" },
                new SpecialCategoryViewModel() { Description = "General", Status = "Active", Position = "Top" },
                new SpecialCategoryViewModel() { Description = "Display", Status = "Active", Position = "Middle" },
                new SpecialCategoryViewModel() { Description = "Software", Status = "Active", Position = "Middle" },
                new SpecialCategoryViewModel() { Description = "Camera", Status = "Active", Position = "Middle" },
                new SpecialCategoryViewModel() { Description = "Connectivity", Status = "Active", Position = "Middle" },
                new SpecialCategoryViewModel() { Description = "Processor", Status = "Active", Position = "Middle" },
                new SpecialCategoryViewModel() { Description = "Memory & Storage", Status = "Active", Position = "Middle" },
                new SpecialCategoryViewModel() { Description = "Hardware", Status = "Active", Position = "Middle" },
                new SpecialCategoryViewModel() { Description = "Hardware Connectivity", Status = "Active", Position = "Middle" },
                new SpecialCategoryViewModel() { Description = "Battery & Power", Status = "Active", Position = "Middle" },
                new SpecialCategoryViewModel() { Description = "Dimensions", Status = "Active", Position = "Middle" },
                new SpecialCategoryViewModel() { Description = "Warranty", Status = "Active", Position = "Bottom" },

                //Special Category For Computers
                 new SpecialCategoryViewModel() { Description = "Storage", Status = "Active", Position = "Middle" },
                 new SpecialCategoryViewModel() { Description = "Optical Disk Drive", Status = "Active", Position = "Middle" },
                 new SpecialCategoryViewModel() { Description = "Platform", Status = "Active", Position = "Middle" },
                 new SpecialCategoryViewModel() { Description = "Graphics", Status = "Active", Position = "Middle" },
                 new SpecialCategoryViewModel() { Description = "Keyboard/Input", Status = "Active", Position = "Middle" },
                 new SpecialCategoryViewModel() { Description = "Audio", Status = "Active", Position = "Middle" },
                 new SpecialCategoryViewModel() { Description = "Communication", Status = "Active", Position = "Middle" },
                 new SpecialCategoryViewModel() { Description = "Ports & Slots", Status = "Active", Position = "Middle" },
                 new SpecialCategoryViewModel() { Description = "Security", Status = "Active", Position = "Middle" },
                 new SpecialCategoryViewModel() { Description = "Size & Weight", Status = "Active", Position = "Middle" }

                );

            context.Specifications.AddOrUpdate(
                p => p.SpecificationId,
                new SpecificationViewModel() { SpecialCatId = 1, Description = "Box Contents", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 2, Description = "Brand", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 2, Description = "Model", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 2, Description = "Form", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 2, Description = "SIMs", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 2, Description = "SIM Size", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 2, Description = "Color", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 2, Description = "Other Features", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 2, Description = "Call Features", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 3, Description = "Screen Size", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 3, Description = "Display Type", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 3, Description = "Screen Protection", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 3, Description = "Pixel Density", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 3, Description = "MultiTouch", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 3, Description = "Other Screen Features", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 4, Description = "OS Version", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 4, Description = "Pre Installed Apps", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 4, Description = "Multi Language Supported", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 5, Description = "Rear Camera", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 5, Description = "Auto Focus", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 5, Description = "Flash", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 5, Description = "Front Camera", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 5, Description = "Other Camera Features", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 6, Description = "GSM", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 6, Description = "CDMA", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 6, Description = "3G/WCDMA", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 6, Description = "4G/LTE", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 7, Description = "Processor Speed", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 7, Description = "Processor Cores", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 7, Description = "Processor Brand", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 8, Description = "RAM", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 8, Description = "Internal Memory", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 8, Description = "User Memory", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 8, Description = "Expandable Memory", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 8, Description = "Memory Card Slot", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 9, Description = "Accelerometer", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 9, Description = "Compass", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 9, Description = "Proximity", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 9, Description = "Gyro-Sensor", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 10, Description = "Bluetooth A2DP", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 10, Description = "Audio Jack", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 10, Description = "SAR Value", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 10, Description = "FM Radio", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 11, Description = "Battery Capacity", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 11, Description = "Battery Type", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 11, Description = "Replaceable Battery", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 11, Description = "Talk Time", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 12, Description = "HeightxWidthxHeight", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 12, Description = "Weight", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 13, Description = "Warranty Type", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 13, Description = "Warranty Duration", Status = "Active" },

                //Specifications For Laptops
                new SpecificationViewModel() { SpecialCatId = 14, Description = "Hard Disk Capacity", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 14, Description = "Hard Disk Capacity", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 14, Description = "RPM", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 14, Description = "Hardware Interface", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 15, Description = "Read/Write Speed", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 15, Description = "Optical Drive", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 16, Description = "Operating System", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 3, Description = "Resolution", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 3, Description = "Screen Type", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 17, Description = "Dedicated Graphics Memory Type", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 17, Description = "Type", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 17, Description = "Dedicated Graphics Memory Capacity", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 17, Description = "Graphic Processor", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 18, Description = "Integrated Camera", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 18, Description = "Pointer Device", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 18, Description = "Keyboard", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 19, Description = "Internal Mic", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 19, Description = "Speakers", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 19, Description = "Sound Effect", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 20, Description = "Wireless LAN", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 20, Description = "Bluetooth", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 11, Description = "Standard Battery", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 21, Description = "USP Ports", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 21, Description = "Mic In", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 21, Description = "Multi Card Slot", Status = "Active" },
                new SpecificationViewModel() { SpecialCatId = 22, Description = "Lock Port", Status = "Active" }
                );

            context.ProductSpecs.AddOrUpdate(

                //ProductSpec For Mobiles
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 2 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 3 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 4 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 5 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 6 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 7 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 8 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 9 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 10 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 11 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 12 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 13 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 14 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 15 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 16 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 17 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 18 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 19 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 20 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 21 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 22 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 23 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 24 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 25 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 26 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 27 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 28 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 29 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 30 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 31 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 32 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 33 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 34 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 35 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 36 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 37 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 38 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 39 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 40 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 41 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 42 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 43 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 44 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 45 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 46 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 47 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 48 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 49 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 50 },
                new ProductSpecViewModel() { ProductId = 1, SpecificationId = 51 },

                //ProductSpec For Laptops
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 52 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 53 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 54 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 55 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 56 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 57 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 58 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 59 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 60 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 61 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 62 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 63 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 64 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 65 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 66 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 67 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 68 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 69 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 70 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 71 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 72 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 73 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 74 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 75 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 76 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 52 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 52 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 52 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 52 },
                new ProductSpecViewModel() { ProductId = 2, SpecificationId = 52 }

                );
        }
    }
}