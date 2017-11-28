using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AsyncQuerying.Infrastructure.CastleWindsor
{
    internal sealed class WindsorDependencyResolver : IDependencyResolver, IDisposable
    {
        public WindsorDependencyResolver(IWindsorContainer container)
        {
            this.container = container;
        }

        public Object GetService(Type serviceType)
        {
            return container.Kernel.HasComponent(serviceType) ? container.Resolve(serviceType) : null;
        }

        public IEnumerable<Object> GetServices(Type serviceType)
        {
            if (!container.Kernel.HasComponent(serviceType))
            {
                return new Object[0];
            }

            return container.ResolveAll(serviceType).Cast<Object>();
        }

        public void Dispose()
        {
            container.Dispose();
        }

        #region private

        private readonly IWindsorContainer container;

        #endregion
    }
}