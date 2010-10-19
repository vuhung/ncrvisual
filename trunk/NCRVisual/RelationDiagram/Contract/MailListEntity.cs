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
        //public int UserTo { get; set; }

        #endregion

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public MailListEntity()
        {
            Connections = new List<IConnection>();
            //MessageSubject = new List<string>();
            //SendDate = new List<string>();
        }
    }
}
