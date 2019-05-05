using System;
using System.Collections.Generic;
using System.Security;
using CarnGo.Security;
using Prism.Events;
using Prism.Ioc;
using Unity;
using Unity.Injection;

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
            Container.RegisterType<IValidator<string>, EmailValidator>(new InjectionConstructor());
            Container.RegisterType<IValidator<SecureString>, PasswordValidator>(new InjectionConstructor());
            Container.RegisterType<IValidator<List<SecureString>>, PasswordMatchValidator>(new InjectionConstructor());
            Container.RegisterType<IQueryDatabase, RealDatabaseQuerier>(new InjectionConstructor()); //Update with real database querier
            Container.AddExtension(new Diagnostic());
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

    }
}