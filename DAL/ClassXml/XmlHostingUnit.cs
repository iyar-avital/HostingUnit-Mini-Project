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

        //public static void SaveToXML(List<HostingUnit> source, string path)
        //{
        //    FileStream file = new FileStream(path, FileMode.Create);
        //    XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
        //    xmlSerializer.Serialize(file, source);
        //    file.Close();
        //}

        //public static List<HostingUnit> LoadFromXML(string path)
        //{
        //    FileStream file = new FileStream(path, FileMode.Open);
        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HostingUnit>));
        //    List<HostingUnit> result = (List<HostingUnit>)xmlSerializer.Deserialize(file);
        //    file.Close();
        //    return result;
        //}

        //public void AddUnit(HostingUnit hostingUnit)
        //{
        //    HostingUnitRoot.Add(
        //        new XElement("HostingUnit",
        //            new XElement("HostingUnitKey", hostingUnit.HostingUnitKey),
        //            new XElement("Owner",
        //                new XElement("HostKey", hostingUnit.Owner.HostKey),
        //                new XElement("PrivateName", hostingUnit.Owner.PrivateName),
        //                new XElement("FamilyName", hostingUnit.Owner.FamilyName),
        //                new XElement("PhoneNumber", hostingUnit.Owner.PhoneNumber),
        //                new XElement("MailAddress", hostingUnit.Owner.MailAddress),
        //                new XElement("BankAccountDetails",
        //                    new XElement("BankNumber", hostingUnit.Owner.BankAccountDetails.BankNumber),
        //                    new XElement("BankName", hostingUnit.Owner.BankAccountDetails.BankName),
        //                    new XElement("BranchNumber", hostingUnit.Owner.BankAccountDetails.BranchNumber),
        //                    new XElement("BranchAddress", hostingUnit.Owner.BankAccountDetails.BranchAddress),
        //                    new XElement("BranchCity", hostingUnit.Owner.BankAccountDetails.BranchCity),
        //                    new XElement("BankAccountNumber", hostingUnit.Owner.BankAccountDetails.BankAccountNumber)),
        //                new XElement("BankAccountNumber", hostingUnit.Owner.BankAccountNumber),
        //                new XElement("CollectionClearance", hostingUnit.Owner.CollectionClearance)),
        //            new XElement("HostingUnitName", hostingUnit.HostingUnitName),
        //            new XElement("PhotoList", hostingUnit.PhotoList),
        //            new XElement("Diary", hostingUnit.Diary/*.Select(x => new XElement("item", x))*/),
        //            new XElement("Area", hostingUnit.Area),
        //            new XElement("SubArea", hostingUnit.SubArea),
        //            new XElement("Type", hostingUnit.Type),
        //            new XElement("Adults", hostingUnit.Adults),
        //            new XElement("Children", hostingUnit.Children),
        //            new XElement("Pool", hostingUnit.Pool),
        //            new XElement("Jacuzzi", hostingUnit.Jacuzzi),
        //            new XElement("Garden", hostingUnit.Garden),
        //            new XElement("Porch", hostingUnit.Porch),
        //            new XElement("ChildrensAttractions", hostingUnit.ChildrensAttractions)
        //        ));
        //}

        //public HostingUnit GetHostingUnit(long key)
        //{
        //    //HostingUnitRoot.Elements().
        //    //    Where(item => (long)Convert.ToInt32(item.Element("HostingUnitKey").Value) == key).
        //    //    Select(item => new HostingUnit()
        //    //    {

        //    //    }).FirstOrDefault();
        //    return null;
        //}
    }
}
