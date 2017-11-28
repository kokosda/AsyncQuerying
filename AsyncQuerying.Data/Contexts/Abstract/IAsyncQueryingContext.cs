using AsyncQuerying.Domain.Abstractions.Entities;
using System;
using System.Data.Entity;

namespace AsyncQuerying.Data.Contexts.Abstract
{
    public interface IAsyncQueryingContext<T> : IDisposable where T : Entity
    {
        DbSet<T> Entities { get; }
    }
}
