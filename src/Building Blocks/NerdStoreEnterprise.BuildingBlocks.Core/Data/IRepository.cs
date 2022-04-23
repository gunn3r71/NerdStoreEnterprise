using System;
using NerdStoreEnterprise.BuildingBlocks.Core.DomainObjects;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}