﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BontoBuy.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Status { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ActivationCode { get; set; }
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
        public DbSet<CustomerViewModel> Customers { get; set; }
        public DbSet<ProductViewModel> Products { get; set; }
        public DbSet<ItemViewModel> Items { get; set; }
        public DbSet<ModelViewModel> Models { get; set; }
        public DbSet<SpecificationViewModel> Specifications { get; set; }
        public DbSet<SpecialCategoryViewModel> SpecialCategories { get; set; }
        public DbSet<ModelSpecViewModel> ModelSpecs { get; set; }
        public DbSet<BrandViewModel> Brands { get; set; }
        public DbSet<AdminViewModel> Admins { get; set; }
        public DbSet<SupplierViewModel> Suppliers { get; set; }
        public DbSet<PhotoViewModel> Photos { get; set; }
        public DbSet<ProductSpecViewModel> ProductSpecs { get; set; }
        public DbSet<PhotoModelViewModel> PhotoModels { get; set; }
        public DbSet<StatusViewModel> Statuses { get; set; }
        public DbSet<OrderViewModel> Orders { get; set; }
        public DbSet<OrderStatusViewModel> OrderStatuses { get; set; }
        public DbSet<DeliveryViewModel> Deliveries { get; set; }
        public DbSet<ReturnViewModel> Returns { get; set; }
        public DbSet<DeliveryAddressViewModel> DeliveryAddresses { get; set; }
        public DbSet<PaymentViewModel> Payments { get; set; }
        public DbSet<CommissionViewModel> Commissions { get; set; }
        public DbSet<RatingViewModel> Ratings { get; set; }
        public DbSet<WishlistViewModel> Wishlists { get; set; }
        public DbSet<DeliveryAddressStatusViewModel> DeliveryAddressStatuses { get; set; }
        public DbSet<ReturnStatusViewModel> ReturnStatuses { get; set; }
        public DbSet<PaymentStatusViewModel> PaymentStatuses { get; set; }
        public DbSet<ModelCommissionViewModel> ModelCommissions { get; set; }
        public DbSet<WishlistModelViewModel> WishlistModels { get; set; }
        public DbSet<RatingModelViewModel> RatingModels { get; set; }
        public DbSet<ReviewViewModel> Reviews { get; set; }
    }
}