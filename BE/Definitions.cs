using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BE
{
    public enum Type_Unit { Tent, Zimmer, HotelRoom, HostingUnit }
    public enum Areas { North, South, Center, Jerusalem, All }
    public enum Request_SubArea { Eilat, Hermon, Tel_Aviv, Jerusalem }
    public enum OrderStatus { Sent_Mail, Closed_from_customer_not_respone, Closed_on_customer_response }
    public enum Request_Status { Active, Closed_by_Web, Closed_expired }
    public enum Option { Necessary, Possible, Not_interested }
}
