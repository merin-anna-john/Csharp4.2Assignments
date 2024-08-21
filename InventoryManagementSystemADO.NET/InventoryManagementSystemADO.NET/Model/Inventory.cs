using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystemADO.NET.Model
{
    public class Inventory
    {
        //Auto-generated fields
        private int Id;

        //Properties
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; }

        //Default Constructor
        public Inventory()
        {

        }

        //Parameterized Cosntructor
        public Inventory(string productCode, string productName, string category,string quantity, string unitPrice)
        {
            this.ProductCode = productCode;
            this.ProductName = productName;
            this.Category = category;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            
        }
    }
}
