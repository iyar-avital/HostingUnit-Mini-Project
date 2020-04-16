using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order
    {
        public int HostingUnitKey { get; set; }
        public int GuestRequestKey { get; set; }
        public  OrderStatus StatusOrder { get; set; }
        public int OrderKey { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
        public override string ToString()
        {
            return "Hosting Unit Key: " + HostingUnitKey + "\n" +
                "Guest Request Key: " + GuestRequestKey + "\n" +
                "Order Key: " + OrderKey + "\n" +
                "Create Date: " + CreateDate.ToString("dd/MM") + "\n" +
                "Order Date: " + OrderDate.ToString("dd/MM");
        }
    }
}
