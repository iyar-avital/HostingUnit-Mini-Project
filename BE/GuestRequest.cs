using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
     public class GuestRequest //: Inumerable
    {
        [XmlElement("GuestRequestKey")]
        public int GuestRequestKey { get; set; }
        [XmlElement("PrivateName")]
        public string PrivateName { get; set; }
        [XmlElement("FamilyName")]
        public string FamilyName { get; set; }
        [XmlElement("MailAddress")]
        public string MailAddress { get; set; }
        [XmlElement("Status")]
        public Request_Status StatusRequest { get; set; }
        [XmlElement("RegistrationDate")]
        public DateTime RegistrationDate { get; set; }
        [XmlElement("EntryDate")]
        public DateTime EntryDate { get; set; }
        [XmlElement("ReleaseDate")]
        public DateTime ReleaseDate { get; set; }
        [XmlElement("Area")]
        public Areas Area { get; set; }
        [XmlElement("SubArea")]
        public Request_SubArea SubArea { get; set; }//לא חובה
        [XmlElement("Type")]
        public Type_Unit Type { get; set; }
        [XmlElement("Adults")]
        public int Adults { get; set; }
        [XmlElement("Children")]
        public int Children { get; set; }
        [XmlElement("Pool")]
        public Option Pool { get; set; }
        [XmlElement("Jacuzzi")]
        public Option Jacuzzi { get; set; }
        [XmlElement("Garden")]
        public Option Garden { get; set; }
        [XmlElement("ChildrensAttractions")]
        public Option ChildrensAttractions { get; set; }
        
        public string FullName
        {
            get { return PrivateName + " " + FamilyName; }
            set { }
        }




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
