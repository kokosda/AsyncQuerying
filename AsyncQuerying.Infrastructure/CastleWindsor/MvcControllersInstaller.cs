using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Mvc;

namespace AsyncQuerying.Infrastructure.CastleWindsor
{
    public sealed class MvcControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("AsyncQuerying.Web")
                                      .BasedOn<Controller>()
                                      .LifestylePerWebRequest());
        }
    }
}