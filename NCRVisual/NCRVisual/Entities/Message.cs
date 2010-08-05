using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace NCRVisual.Entities
{
    public class Message
    {
        private User sender;

        private String subject;

        private DateTime sentDate;

        public User Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        public String Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public DateTime SentDate
        {
            get { return sentDate; }
            set { sentDate = value; }
        }
    }
}
