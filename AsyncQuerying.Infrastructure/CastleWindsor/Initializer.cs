using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Web.Mvc;

namespace AsyncQuerying.Infrastructure.CastleWindsor
{
    public sealed class Initializer : IDisposable
    {
        public Initializer()
        {
            container = new WindsorContainer();

            Configure();
        }

        public void Dispose()
        {
            container.Dispose();
        }

        public IWindsorContainer Container
        {
            get
            {
                return container;
            }
        }

        public void Register()
        {
            ComponentRegistrar.AddComponentsTo(Container);

            var windsorServiceLocator = new WindsorServiceLocator(Container);
            ServiceLocator.SetLocatorProvider(() => windsorServiceLocator);

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container));
        }

        #region private

        private void Configure()
        {
            container.Install(FromAssembly.This());
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            DependencyResolver.SetResolver(new WindsorDependencyResolver(container));
        }

        private readonly IWindsorContainer container;

        #endregion
    }
}