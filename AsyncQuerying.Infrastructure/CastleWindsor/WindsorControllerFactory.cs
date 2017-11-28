using Castle.Windsor;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AsyncQuerying.Infrastructure.CastleWindsor
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        public WindsorControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        public override void ReleaseController(IController controller)
        {
            container.Release(controller.GetType());
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                return (IController)container.Resolve(controllerType);
            }
            catch (ArgumentNullException)
            {
                throw new HttpException((Int32)HttpStatusCode.NotFound, String.Format("Not found: {0}", requestContext.HttpContext.Request.Url));
            }
        }

        #region private

        private readonly IWindsorContainer container;

        #endregion
    }
}