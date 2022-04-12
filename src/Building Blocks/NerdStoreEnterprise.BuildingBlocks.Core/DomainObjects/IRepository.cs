using System;

namespace NerdStoreEnterprise.BuildingBlocks.Core.DomainObjects
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}