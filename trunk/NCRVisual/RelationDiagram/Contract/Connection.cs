namespace NCRVisual.RelationDiagram
{
    /// <summary>
    /// The connection between 2 entities
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Get or set the source entity
        /// </summary>
        public IEntity Source { get; set; }

        /// <summary>
        /// Get or set the destination entity
        /// </summary>
        public IEntity Destination { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="e1">The source entity</param>
        /// <param name="e2">The destination entity</param>
        public Connection(IEntity e1, IEntity e2)
        {
            this.Source = e1;
            this.Destination = e2;
        }
    }
}
