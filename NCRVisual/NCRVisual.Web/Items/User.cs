using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCRVisual.Web.Items
{
    public class User : IEqualityComparer<User>
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public bool Equals(User x, User y)
        {
            if (x.UserId == y.UserId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(User obj)
        {
            throw new NotImplementedException();
        }
    }
}