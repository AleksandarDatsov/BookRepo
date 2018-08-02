using System;
using System.Web.Mvc;
using BookStore.Domain.Entities;
using BookStore.Domain.Infrastructure.Repository.Interfaces;
using BookStore.Domain.Infrastructure.UnitOfWorkPattern;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Persistence.Repositories;
using BookStore.Infrastructure.Repository.UnitOfWork;
using BookStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Lifetime;

namespace BookStore
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        internal static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IBookStoreDbContext, BookStoreDbContext>(new PerResolveLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IBookRepository, BookRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthorRepository, AuthorRepository>(new PerRequestLifetimeManager());

            container.RegisterType<ApplicationUser>(new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new PerRequestLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
        }
    }
}