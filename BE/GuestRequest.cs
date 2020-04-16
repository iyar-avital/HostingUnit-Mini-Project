using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
     public class GuestRequest //: Inumerable
    {
        public int GuestRequestKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public Request_Status StatusRequest { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Areas Area { get; set; }
        public Request_SubArea SubArea { get; set; }//לא חובה
        public Type_Unit Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public Option Pool { get; set; }
        public Option Jacuzzi { get; set; }
        public Option Garden { get; set; }
        public Option ChildrensAttractions { get; set; }


        public override string ToString()
        {
            return "GuestRequestKey: " + GuestRequestKey + "\n" +
                "PrivateName: " + PrivateName + "\n" +
                "FamilyName: " + FamilyName + "\n" +
                "MailAddress: " + MailAddress + "\n" +
                "RegistrationDate: " + RegistrationDate + "\n" +
                "EntryDate: " + EntryDate + "\n" +
                "ReleaseDate: " + ReleaseDate + "\n" +
                "SubArea: " + SubArea + "\n" +
                "Adults: " + Adults + "\n" +
                "Children: " + Children;
        }
    }
   
}
