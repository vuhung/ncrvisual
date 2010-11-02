using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace NCRVisual.RelationDiagram
{
    public class MailListEntity : IEntity
    {
        #region IEntity Members

        public string Name
        {
            get;
            set;
        }

        public List<IConnection> Connections
        {
            get;
            set;
        }

        public string Id { get; set; }

        #endregion

        public string UserName { get; set; }
        public string Email { get; set; }

        public MailListEntity()
        {
            Connections = new List<IConnection>();
            Connections.Add(new Connection(this, null, 0));
            //MessageSubject = new List<string>();
            //SendDate = new List<string>();
        }
    }
}
