using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Store.RepositoryLayer
{
    class Order
    {
        
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
    }
}
