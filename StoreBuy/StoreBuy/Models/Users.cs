using System;
using System.Collections.Generic;
using System.Text;

namespace StoreBuy.Models
{
    public class Users
    {
        public virtual long UserId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string UserPassword { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
    }
}
