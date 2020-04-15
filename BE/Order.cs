using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.Definitions;

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
            return HostingUnitKey.ToString() + GuestRequestKey.ToString() + OrderKey.ToString()
                + CreateDate.ToString() + OrderDate.ToString();
        }
    }
}
