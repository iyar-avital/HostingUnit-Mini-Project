using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL.ClassXml
{
    public class FactorySingletonXmlHostingUnit
    {
        private static XmlHostingUnit singelonXmlHostingUnit = null;
        private FactorySingletonXmlHostingUnit() { }
        public static XmlHostingUnit GetXmlHostingUnit()
        {
            if (singelonXmlHostingUnit == null)
            {
                singelonXmlHostingUnit = new XmlHostingUnit();
            }
            return singelonXmlHostingUnit;
        }
    }

    public class XmlHostingUnit
    {
        public XElement HostingUnitRoot;
        public string HostingUnitPath = @"HostingUnitXml.xml";

        public XmlHostingUnit()
        {
            if (!File.Exists(HostingUnitPath))
                CreateXmlHostingUnit();
            else
                LoadXmlHostingUnit();
        }

        public void CreateXmlHostingUnit()
        {
            HostingUnitRoot = new XElement("HostingUnits");
            HostingUnitRoot.Save(HostingUnitPath);
        }

        public void LoadXmlHostingUnit()
        {
            HostingUnitRoot = XElement.Load(HostingUnitPath);
        }
        
    }
}
