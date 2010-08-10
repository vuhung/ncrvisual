using System;
using TestAlgo.Library;

namespace TestAlgo
{
    public class EntityControlClickedEventArgs : EventArgs
    {
        public EntityControlClickedEventArgs(IEntity entity)
        {
            this.Entity = entity;
        }

        public IEntity Entity { get; set; }
    }
}
