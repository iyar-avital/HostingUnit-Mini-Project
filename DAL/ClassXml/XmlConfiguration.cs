using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DAL.ClassXml
{
    public class XmlConfiguration
    {
        public class FactorySingletonXmlConfiguration
        {
            private static XmlConfiguration singelonXmlConfiguration = null;
            private FactorySingletonXmlConfiguration() { }
            public static XmlConfiguration GetXmlConfiguration()
            {
                if (singelonXmlConfiguration == null)
                {
                    singelonXmlConfiguration = new XmlConfiguration();
                }
                return singelonXmlConfiguration;
            }
        }

        public XElement ConfigurationRoot;
        public string ConfigurationPath = @"ConfigurationXml.xml";

        public XmlConfiguration()
        {
            if (!File.Exists(ConfigurationPath))
                CreateXmlConfiguration();
            else
                LoadXmlConfiguration();
        }

        public void CreateXmlConfiguration()
        {
            ConfigurationRoot = new XElement("Configurations");
            ConfigurationRoot.Save(ConfigurationPath);
        }

        public void LoadXmlConfiguration()
        {
            ConfigurationRoot = XElement.Load(ConfigurationPath);
        }

        public void AddConfiguration()
        {
            ConfigurationRoot.Add(new XElement("Configuration",
                new XElement("OrderKey", Configuration.OrderKeySeq),
                new XElement("HostUnitKey", Configuration.HostingUnitKeySeq),
                new XElement("GuestRequestKey", Configuration.guestRequestKeySeq),
                new XElement("SumConnission", Configuration.fee),
                new XElement("NumDaysUntillExpired", Configuration.NumDaysUntillExpired),
                new XElement("mailAddress", Configuration.mailAddress),
                new XElement("Password", Configuration.Password)
                ));
            ConfigurationRoot.Save(ConfigurationPath);
        }

        public void UpdateConfiguration<T>(string name, T value)
        {
            XElement xElement = ConfigurationRoot.Element("Configuration");
            xElement.Element(name).SetValue(value);
            ConfigurationRoot.Save(ConfigurationPath);
        }

        public T GetConfiguration<T>(string name)
        {
            XElement xElement = ConfigurationRoot.Element("Configuration");
            return (T)Convert.ChangeType(xElement.Element(name).Value, typeof(T));
        }

        public void SetConfiguration()
        {
            XElement xElement = ConfigurationRoot.Element("Configuration");
            Configuration.OrderKeySeq = Convert.ToInt32(xElement.Element("OrderKey").Value);
            Configuration.HostingUnitKeySeq = Convert.ToInt32(xElement.Element("HostUnitKey").Value);
            Configuration.guestRequestKeySeq = Convert.ToInt32(xElement.Element("GuestRequestKey").Value);
            Configuration.fee = float.Parse(xElement.Element("SumConnission").Value);
            Configuration.NumDaysUntillExpired = Convert.ToInt32(xElement.Element("NumDaysUntillExpired").Value);
            Configuration.mailAddress = new MailAddress(xElement.Element("mailAddress").Value);
            Configuration.Password = xElement.Element("Password").Value;

        }

    }
}
