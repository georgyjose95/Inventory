using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Store.RepositoryLayer
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

      //  [DataType(DataType.Password)]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string  LastName { get; set; }
      //  [DataType(DataType.EmailAddress)]
        public string  Email { get; set; }
        public int Membership { get; set; }

        enum MembershipDetails
        {
            Trial = 0,
            Normal = 1,
            Premium = 2
        }
        
    }
}
