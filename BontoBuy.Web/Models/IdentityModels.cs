using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BontoBuy.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "DateTime2")]
        public Nullable<DateTime> DtCreated { get; set; }

        [Column(TypeName = "DateTime2")]
        public Nullable<DateTime> DtUpdated { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<CategoryViewModel> Categories { get; set; }
        public DbSet<ProductViewModel> Products { get; set; }
        public DbSet<ItemViewModel> Items { get; set; }
        public DbSet<ModelViewModel> Models { get; set; }
        public DbSet<SpecificationViewModel> Specifications { get; set; }
        public DbSet<TagViewModel> Tags { get; set; }
        public DbSet<ModelSpecViewModel> ModelSpecs { get; set; }
        public DbSet<BrandViewModel> Brands { get; set; }
        public DbSet<AdminViewModel> Admins { get; set; }
        public DbSet<PhotoViewModel> Photos { get; set; }
    }
}