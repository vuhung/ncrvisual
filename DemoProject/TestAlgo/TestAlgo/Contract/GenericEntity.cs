using System.Collections.Generic;

namespace TestAlgo.Library
{
    public class GenericEntity : IEntity
    {
        public string Name { get; set; }
        public List<IEntity> Connections { get; set; }

        public GenericEntity()
        {
            this.Connections = new List<IEntity>();
        }

        public GenericEntity(string name)
        {
            this.Connections = new List<IEntity>();
            this.Name = name;
        }
    }
}
