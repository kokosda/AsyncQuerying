using Castle.MicroKernel.Registration;
using Castle.Windsor;
using AsyncQuerying.Data.Contexts;
using AsyncQuerying.Data.Contexts.Abstract;
using AsyncQuerying.Data.Queries.Abstract;
using AsyncQuerying.Helpers.Caching;
using AsyncQuerying.Helpers.Caching.Abstract;

namespace AsyncQuerying.Infrastructure.CastleWindsor
{    
    public sealed class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            AddModelBuildersTo(container);
            AddDataComponentsTo(container);
            AddHelpersComponentsTo(container);
        }

        #region private

        private static void AddModelBuildersTo(IWindsorContainer container)
        {
            container.Register(
                Component.For<AsyncQuerying.Web.Models.Builders.Abstract.IUsersListModelBuilder>()
                         .ImplementedBy<AsyncQuerying.Web.Models.Builders.UsersListModelBuilder>()
                         .LifeStyle.Singleton
                );
        }

        private static void AddDataComponentsTo(IWindsorContainer container)
        {
            container.Register(
                Component.For<AsyncQuerying.Data.Queries.DelayedQuery.Executors.Abstract.IUsersQueryExecutor>()
                         .ImplementedBy<AsyncQuerying.Data.Queries.DelayedQuery.Executors.UsersQueryExecutor>()
                         .LifeStyle.Singleton,
                Component.For(typeof(IAsyncQueryingContext<>))
                         .ImplementedBy(typeof(AsyncQueryingContext<>))
                         .LifeStyle.Transient,
                Component.For<AsyncQuerying.Data.Queries.Users.Abstract.IUsersListByFilterQuery>()
                         .ImplementedBy<AsyncQuerying.Data.Queries.Users.UsersListByFilterQuery>()
                         .LifeStyle.Transient
                );
        }

        private static void AddHelpersComponentsTo(IWindsorContainer container)
        {
            container.Register(
                Component.For<ICacheKeyGenerator>().ImplementedBy<CacheKeyGenerator>().LifeStyle.Singleton
                );
        }

        #endregion
    }
}