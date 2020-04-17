using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class Order
    {
        [XmlElement("HostingUnitKey")]
        public int HostingUnitKey { get; set; }
        [XmlElement("GuestRequestKey")]
        public int GuestRequestKey { get; set; }
        [XmlElement("Status")]
        public  OrderStatus StatusOrder { get; set; }
        [XmlElement("OrderKey")]
        public int OrderKey { get; set; }
        [XmlElement("CreateDate")]
        public DateTime CreateDate { get; set; }
        [XmlElement("OrderDate")]
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
