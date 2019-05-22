using System;
using System.Collections.Generic;
using System.Security;
using CarnGo.Database;
using CarnGo.Model.ThreadTimer;
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
#if DEBUG
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "debug_database")
                .EnableSensitiveDataLogging()
                .Options;

                Container.RegisterType<IAppDbContext, DebugAppDbContext>(new PerThreadLifetimeManager(), new InjectionConstructor(options));
#else
                
                Container.RegisterType<IAppDbContext, AppDbContext>(new PerThreadLifetimeManager());
#endif
            Container.RegisterType<IValidator<string>, EmailValidator>(new InjectionConstructor());
            Container.RegisterType<IValidator<SecureString>, PasswordValidator>(new InjectionConstructor());
            Container.RegisterType<IValidator<List<SecureString>>, PasswordMatchValidator>(new InjectionConstructor());
            Container.RegisterType<IAppToDbModelConverter, ApptoDbModelConverter>(new InjectionConstructor());
            Container.RegisterType<IDbToAppModelConverter, DbToAppModelConverter>(new InjectionConstructor());
            Container.RegisterType<ISendRequestViewModelHelperFunction, SendRequestViewModelHelperFunction>();
            Container.RegisterType<ISearchViewModelHelper, SearchViewModelHelper>();
            Container.RegisterType<ISearchQueries, RealSearchQueries>();
            Container.RegisterType<IQueryDatabase, RealDatabaseQuerier>();
            Container.RegisterType<ThreadTimer>();
            Container.AddExtension(new Diagnostic());
           
        }
        
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

    }
}