namespace DomainObject
{
    /// <summary>
    /// Eine Gruppe von Domänenobjekten, die eine Konsistenzgrenze definieren. Gewährleistet die Transaktionskonsistenz und schützt die geschäftlichen Invarianten der Domäne. Wird durch seine ID eindeutig identifiziert.
    /// </summary>
    /// <typeparam name="TAggregate">Der Typ des Aggregats/Der Typ, der diese Klasse implementiert.</typeparam>
    /// <typeparam name="TKeyType">Der Typ, der zur Identifizierung dieses Aggregats verwendet wird.</typeparam>
    public class Aggregate<TAggregate, TKeyType> : Entity<TAggregate, TKeyType>, IEntity<TAggregate>
        where TAggregate : Entity<TAggregate, TKeyType>
        where TKeyType : Id, IId<TAggregate>
    {
        /// <summary>
        /// Erstellt eine neue Instanz des Aggregats<<typeparamref name="TAggregate"/>, <typeparamref name="TKeyType"/>> class
        /// </summary>
        /// <param name="id">Eine eindeutige Kennung, die dieses Objekt identifiziert.</param>
        protected Aggregate(TKeyType id) : base(id)
        {
        }
    }

    /// <summary>
    /// Eine Gruppe von Domänenobjekten, die eine Konsistenzgrenze definieren. Gewährleistet die Transaktionskonsistenz und schützt die geschäftlichen Invarianten der Domäne. Wird durch seine ID eindeutig identifiziert.
    /// </summary>
    /// <typeparam name="T">Der Typ des Aggregats/Der Typ, der diese Klasse implementiert.</typeparam>
    public class Aggregate<T> : Entity<T> where T : Aggregate<T>
    {
        /// <summary>
        /// Erstellt eine neue Instanz des Aggregats<<typeparamref name="T"/>> class.
        /// </summary>
        /// <param name="id">Eine eindeutige Kennung, die dieses Objekt identifiziert.</param>
        protected Aggregate(Id<T> id) : base(id)
        {
        }
    }
}
