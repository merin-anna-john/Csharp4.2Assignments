using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystemADO.NET.Model
{
    public class Order
    {
        //Auto-generated fields
        private int Id;

        //Properties
        public string ProductCode { get; set; }
        public string CustomerName { get; set; }
        public string Quantity { get; set; }
        public string TotalPrice { get; set; }

        //Default Constructor
        public Order()
        {

        }

        //Parameterized Cosntructor
        public Order(string productCode, string customerName, string quantity, string totalPrice)
        {
            this.ProductCode = productCode;
            this.CustomerName = customerName;
            this.Quantity = quantity;
            this.TotalPrice = totalPrice;
        }

    }
}
