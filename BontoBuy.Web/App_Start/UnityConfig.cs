using System;
using BontoBuy.Web.Controllers;
using BontoBuy.Web.Models;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace BontoBuy.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion Unity Container

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<RoleController>(new InjectionConstructor());
            container.RegisterType<RoleController>(new InjectionConstructor());
            container.RegisterType<IBrandRepo, BrandRepo>();
            container.RegisterType<ICategoryRepo, CategoryRepo>();
            container.RegisterType<IItemRepo, ItemRepo>();
            container.RegisterType<IModelRepo, ModelRepo>();
            container.RegisterType<IProductRepo, ProductRepo>();
            container.RegisterType<IProductCreationRepo, ProductCreationRepo>();
            container.RegisterType<ISpecificationRepo, SpecificationRepo>();
            container.RegisterType<ITagRepo, TagRepo>();
            container.RegisterType<IAdminItemRepo, AdminItemRepo>();
            container.RegisterType<IAdminRoleRepo, AdminRoleRepo>();
            container.RegisterType<IAdminCommissionRepo, AdminCommissionRepo>();
        }
    }
}