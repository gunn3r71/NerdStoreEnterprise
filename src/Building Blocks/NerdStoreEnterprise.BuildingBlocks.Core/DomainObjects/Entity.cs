using System;

namespace NerdStoreEnterprise.BuildingBlocks.Core.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

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

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        public override int GetHashCode() => 
            (GetType().GetHashCode() * new Random().Next(100, 200)) + Id.GetHashCode();

        public override string ToString() =>
            $"{GetType().Name} [Id={Id}]";
    }
}
