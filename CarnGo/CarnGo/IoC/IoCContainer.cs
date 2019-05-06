using System;
using System.Collections.Generic;
using System.Security;
using CarnGo.Database;
using CarnGo.Security;
using Microsoft.EntityFrameworkCore;
using Prism.Events;
using Prism.Ioc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace CarnGo
{
    /// <summary>
    /// Inversion of Control Container for the application
    /// https://www.tutorialsteacher.com/ioc/unity-container
    /// </summary>
    public static class IoCContainer
    {
        public static UnityContainer Container { get; set; } = new UnityContainer();

        public static IApplication Application => Container.Resolve<IApplication>();

        public static void Setup()
        {
            Container.RegisterSingleton<IApplication, ApplicationViewModel>();
            Container.RegisterSingleton<IEventAggregator,EventAggregator>();
            Container.RegisterType<IAppDbContext, AppDbContext>(new PerThreadLifetimeManager(), new InjectionConstructor());
            Container.RegisterType<IValidator<string>, EmailValidator>(new InjectionConstructor());
            Container.RegisterType<IValidator<SecureString>, PasswordValidator>(new InjectionConstructor());
            Container.RegisterType<IValidator<List<SecureString>>, PasswordMatchValidator>(new InjectionConstructor());
            Container.RegisterType<IAppToDbModelConverter, TestApptoDbModelConverter>(new InjectionConstructor());
            Container.RegisterType<IDbToAppModelConverter, TestDbToAppModelConverter>(new InjectionConstructor());
            Container.RegisterType<IQueryDatabase, RealDatabaseQuerier>(); //Update with real database querier
            Container.AddExtension(new Diagnostic());
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

    }
}