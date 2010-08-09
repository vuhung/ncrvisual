namespace NCRVisual.RelationGraph
{
    public class Connection
    {
        public IEntity Entity1 { get; set; }
        public IEntity Entity2 { get; set; }

        public Connection(IEntity e1, IEntity e2)
        {
            this.Entity1 = e1;
            this.Entity2 = e2;
        }
    }
}
