using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AsyncQuerying.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            cwInitializer = new AsyncQuerying.Infrastructure.CastleWindsor.Initializer();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            WindsorContainerConfig.Register(cwInitializer);
        }

        #region private

        private readonly AsyncQuerying.Infrastructure.CastleWindsor.Initializer cwInitializer;

        #endregion
    }
}
