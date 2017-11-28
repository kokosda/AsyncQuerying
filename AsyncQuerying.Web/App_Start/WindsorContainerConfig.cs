
namespace AsyncQuerying.Web
{
    internal sealed class WindsorContainerConfig
    {
        public static void Register(AsyncQuerying.Infrastructure.CastleWindsor.Initializer initializer)
        {
            initializer.Register();
        }
    }
}
