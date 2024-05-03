using MediatR;

namespace EshopMonolithic.Domain.SeedWork
{
    /// <summary>
    /// In Domain-Driven Design (DDD), declaring an abstract Entity base class like the one you’ve shown is a foundational practice for several reasons:
    /// Identity: Entities are defined by their identity, not their attributes. The Id property ensures that each entity can be uniquely identified, which is crucial for tracking and persisting objects across different system states and databases.
    /// Domain Events: The ability to handle domain events (AddDomainEvent, RemoveDomainEvent, ClearDomainEvents) allows entities to communicate changes within the domain model effectively. This is part of implementing a Domain Event Pattern, which helps in maintaining the consistency of the domain model and can be used to trigger side effects in response to state changes.
    /// Transience Checking: The IsTransient method provides a way to check if an entity has been persisted or not. This is useful for determining whether an entity is new and not yet saved to the database.
    /// Equality: Properly overriding Equals and GetHashCode methods ensures that entity equality is based on identity (the Id), not on reference or attribute equality. This is important for consistency when entities are used in collections or compared.
    /// Operator Overloading: Overloading the == and != operators provides a more intuitive way to compare entities by their identity rather than by reference, which is the default behavior in C#.
    /// </summary>
    public abstract class Entity
    {
        int? _requestedHashCode;
        int _Id;
        public virtual int Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return this.Id == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
