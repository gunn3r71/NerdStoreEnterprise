using System;
using MediatR;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages
{
    public abstract class Event : Message, INotification
    {
        protected Event()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
    }
}