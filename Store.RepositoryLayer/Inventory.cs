using System;
using System.Collections.Generic;
using System.Text;

namespace Store.RepositoryLayer
{
    public class Inventory
    {
        public Guid InventoryId { get; set; }
        public String Name { get; set; }
        public int Quantity { get; set; }
    }
}
