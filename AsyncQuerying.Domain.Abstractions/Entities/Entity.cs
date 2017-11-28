using System;

namespace AsyncQuerying.Domain.Abstractions.Entities
{
    public abstract class Entity
    {
        public Int64 Id { get; protected set; }
    }
}
