using AsyncQuerying.Data.Contexts.Abstract;
using AsyncQuerying.Domain.Abstractions.Entities;
using System.Data.Entity;

namespace AsyncQuerying.Data.Contexts
{
    public sealed class AsyncQueryingContext<T> : DbContext, IAsyncQueryingContext<T> where T:Entity
    {
        public AsyncQueryingContext()
            : base("default")
        {
            this.Database.Initialize(false);
        }

        public DbSet<T> Entities { get; set; }
    }
}
