using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BE
{
    public enum Request_Type { Tent, Zimmer, HotelRoom, HostingUnit }
    public enum Request_Area { North, South, Center, Jerusalem, All }
    public enum Request_SubArea { Eilat, Hermon, Tel_Aviv, Jerusalem }
    public enum OrderStatus { Has_not_been_treated, Sent_Mail, Closed_from_customer_not_respone, Closed_on_customer_response }
    public enum Request_Status { Active, Closed_by_Web, Closed_expired }
    public enum Request_Pool { Necessary, Possible, Not_interested }
    public enum Request_Jacuzzi { Necessary, Possible, Not_interested }
    public enum Request_Garden { Necessary, Possible, Not_interested }
    public enum Request_ChildrensAttractions { Necessary, Possible, Not_interested }
    public enum Hosting_Type { Tent, Zimmer, HotelRoom, HostingUnit }
    public enum Hosting_Area { North, South, Center, Jerusalem, All }
    public class Definitions
    {
       
      }
}
