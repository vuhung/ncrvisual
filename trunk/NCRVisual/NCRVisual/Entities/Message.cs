using System;

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
