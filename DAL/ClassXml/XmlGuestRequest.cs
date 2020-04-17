using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DAL.ClassXml.XmlConfiguration;

namespace DAL.ClassXml
{
    public class FactorySingletonXmlGuestRequest
    {
        private static XmlGuestRequest singelonXmlGuestRequest = null;
        private FactorySingletonXmlGuestRequest() { }
        public static XmlGuestRequest GetXmlGuestRequest()
        {
            if (singelonXmlGuestRequest == null)
            {
                singelonXmlGuestRequest = new XmlGuestRequest();
            }
            return singelonXmlGuestRequest;
        }
    }


    public class XmlGuestRequest
    {
        public XmlConfiguration XC = FactorySingletonXmlConfiguration.GetXmlConfiguration();

        public XElement GuestRequestRoot;
        public string GuestRequestPath = @"GuestRequestXml.xml";

        public XmlGuestRequest()
        {
            if (!File.Exists(GuestRequestPath))
                CreateXmlGuestRequest();
            else
                LoadXmlGuestRequest();
        }

        public void CreateXmlGuestRequest()
        {
            GuestRequestRoot = new XElement("GuestRequests");
            GuestRequestRoot.Save(GuestRequestPath);
        }

        public void LoadXmlGuestRequest()
        {
            GuestRequestRoot = XElement.Load(GuestRequestPath);
        }

        public long AddRequest(GuestRequest guestRequest)
        {
            long key = XC.GetConfiguration<long>("GuestRequestKey");
            GuestRequestRoot.Add(
                new XElement("GuestRequest",
                    new XElement("GuestRequestKey", key),
                    new XElement("PrivateName", guestRequest.PrivateName),
                    new XElement("FamilyName", guestRequest.FamilyName),
                    new XElement("MailAddress", guestRequest.MailAddress),
                    new XElement("Status", guestRequest.StatusRequest),
                    new XElement("RegistrationDate", guestRequest.RegistrationDate),
                    new XElement("EntryDate", guestRequest.EntryDate),
                    new XElement("ReleaseDate", guestRequest.ReleaseDate),
                    new XElement("Area", guestRequest.Area),
                    new XElement("SubArea", guestRequest.SubArea),
                    new XElement("Type", guestRequest.Type),
                    new XElement("Adults", guestRequest.Adults),
                    new XElement("Children", guestRequest.Children),
                    new XElement("Pool", guestRequest.Pool),
                    new XElement("Jacuzzi", guestRequest.Jacuzzi),
                    new XElement("Garden", guestRequest.Garden),
                    new XElement("ChildrensAttractions", guestRequest.ChildrensAttractions)
                ));
            GuestRequestRoot.Save(GuestRequestPath);
            XC.UpdateConfiguration<long>("GuestRequestKey", ++key);
            return (XC.GetConfiguration<long>("GuestRequestKey") - 1);
        }

        public GuestRequest GetGuestRequest(long key)
        {
            return (GuestRequest)(from p in GuestRequestRoot.Elements()
                                  where Convert.ToInt32(p.Element("GuestRequestKey").Value) == key
                                  select new GuestRequest()
                                  {
                                      GuestRequestKey = Convert.ToInt32(p.Element("GuestRequestKey").Value),
                                      PrivateName = p.Element("PrivateName").Value,
                                      FamilyName = p.Element("FamilyName").Value,
                                      MailAddress = p.Element("MailAddress").Value,
                                      StatusRequest = (Request_Status)Enum.Parse(typeof(Request_Status), p.Element("Status").Value),
                                      RegistrationDate = DateTime.Parse(p.Element("RegistrationDate").Value),
                                      EntryDate = DateTime.Parse(p.Element("EntryDate").Value),
                                      ReleaseDate = DateTime.Parse(p.Element("ReleaseDate").Value),
                                      Area = (Areas)Enum.Parse(typeof(Areas), p.Element("Area").Value),
                                      SubArea = (Request_SubArea)Enum.Parse(typeof(Request_SubArea), p.Element("SubArea").Value),
                                      Type = (Type_Unit)Enum.Parse(typeof(Type_Unit), p.Element("Type").Value),
                                      Adults = Convert.ToInt32(p.Element("Adults").Value),
                                      Children = Convert.ToInt32(p.Element("Children").Value),
                                      Pool = (Option)Enum.Parse(typeof(Option), p.Element("Pool").Value),
                                      Jacuzzi = (Option)Enum.Parse(typeof(Option), p.Element("Jacuzzi").Value),
                                      Garden = (Option)Enum.Parse(typeof(Option), p.Element("Garden").Value),                 
                                      ChildrensAttractions = (Option)Enum.Parse(typeof(Option), p.Element("ChildrensAttractions").Value)
                                  }).FirstOrDefault();
        }

        public bool IsRequestExit(long key)
        {
            return GuestRequestRoot.Elements().Any(item => Convert.ToInt32(item.Element("GuestRequestKey").Value) == key);
        }

        public void UpdateRequest(GuestRequest guestRequest)
        {
            XElement xElement = GuestRequestRoot.Elements().Where(item => Convert.ToInt32(item.Element("GuestRequestKey").Value) == guestRequest.GuestRequestKey).FirstOrDefault();
            xElement.Element("PrivateName").SetValue(guestRequest.PrivateName);
            xElement.Element("FamilyName").SetValue(guestRequest.FamilyName);
            xElement.Element("MailAddress").SetValue(guestRequest.MailAddress);
            xElement.Element("Status").SetValue(guestRequest.StatusRequest);
            xElement.Element("RegistrationDate").SetValue(guestRequest.RegistrationDate);
            xElement.Element("EntryDate").SetValue(guestRequest.EntryDate);
            xElement.Element("ReleaseDate").SetValue(guestRequest.ReleaseDate);
            xElement.Element("Area").SetValue(guestRequest.Area);
            xElement.Element("SubArea").SetValue(guestRequest.SubArea);
            xElement.Element("Type").SetValue(guestRequest.Type);
            xElement.Element("Adults").SetValue(guestRequest.Adults);
            xElement.Element("Children").SetValue(guestRequest.Children);
            xElement.Element("Pool").SetValue(guestRequest.Pool);
            xElement.Element("Jacuzzi").SetValue(guestRequest.Jacuzzi);
            xElement.Element("Garden").SetValue(guestRequest.Garden);
            xElement.Element("ChildrensAttractions").SetValue(guestRequest.ChildrensAttractions);
            GuestRequestRoot.Save(GuestRequestPath);
        }

    }
}


