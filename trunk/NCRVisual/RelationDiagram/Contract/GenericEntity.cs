using System.Collections.Generic;

namespace NCRVisual.RelationDiagram
{
    /// <summary>
    /// A default entity implement IEntity interface
    /// </summary>
    public class GenericEntity : IEntity
    {
        /// <summary>
        /// Get or set the Name of the Entity (May use for displaying also)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the Collection of Connections with this entity
        /// </summary>
        public List<IEntity> Connections { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GenericEntity()
        {
            this.Connections = new List<IEntity>();
        }

        /// <summary>
        /// Create a GenericEntity object with the specific entity's name
        /// </summary>
        /// <param name="name">Name of the entity</param>
        public GenericEntity(string name)
        {
            this.Connections = new List<IEntity>();
            this.Name = name;
        }
    }
}
