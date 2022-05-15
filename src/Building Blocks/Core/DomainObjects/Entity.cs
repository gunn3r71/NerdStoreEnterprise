using System;
using System.Collections.Generic;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects
{
    public abstract class Entity
    {
        private List<Event> _events;

        protected Entity(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public IReadOnlyList<Event> Events => _events?.AsReadOnly();

        public void AddEvent(Event ev)
        {
            _events ??= new List<Event>();
            _events.Add(ev);
        }

        public void RemoveEvent(Event ev)
        {
            _events?.Remove(ev);
        }

        public void ClearEvents()
        {
            _events?.Clear();
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;

            return compareTo is { } && Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;

            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() * new Random().Next(100, 200) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}