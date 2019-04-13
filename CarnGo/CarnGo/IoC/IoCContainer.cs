using System;
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

        public static void Setup()
        {
            Container.RegisterSingleton<ApplicationViewModel>();
            Container.RegisterSingleton<IEventAggregator,EventAggregator>();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

    }
}