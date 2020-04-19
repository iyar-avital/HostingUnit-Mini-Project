using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utilities;

namespace BE
{
    [Serializable]
    public class HostingUnit
    {
        [XmlElement("HostingUnitKey")]
        public int HostingUnitKey { get; set; }
        [XmlElement("Owner")]
        public Host Owner { get; set; }
        [XmlElement("HostingUnitName")]
        public string HostingUnitName { get; set; }
        [XmlIgnore]
        public bool[,] Diary { get; set; }
        [XmlArray(ElementName = "Diary")]
        [XmlArrayItem(ElementName = "item")]
        public bool[] DiaryDto
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(10); }
        }




        [XmlElement("Area")]
        public Areas Area { get; set; }
        [XmlElement("Type")]
        public Type_Unit Type { get; set; }
        [XmlElement("Adults")]
        public int Adults { get; set; }
        [XmlElement("Rooms")]
        public int Rooms { get; set; }
        [XmlElement("Children")]
        public int Children { get; set; }
        [XmlElement("Pool")]
        public bool Pool { get; set; }
        [XmlElement("Jacuzzi")]
        public bool Jacuzzi { get; set; }
        [XmlElement("Garden")]
        public bool Garden { get; set; }
        [XmlElement("ChildrensAttractions")]
        public bool ChildrensAttractions { get; set; }


        





        public override string ToString()
        {
            string str = "";
            str += "Hosting Unit Key: " + HostingUnitKey + "\n" +
                "Owner \n" + Owner + "\n" +
                "Hosting Unit Name: " + HostingUnitName + "\n" +
                "Adults: " + Adults + "\n" +
                "Children: " + Children + "\n" +
                "Pool: " + Pool + "\n" +
                "Jacuzzi: " + Jacuzzi + "\n" +
                "Garden: " + Garden + "\n" +
                "Childrens Attractions: " + ChildrensAttractions;
            DateTime startDate = new DateTime(2020, 01, 01);
            DateTime endDate = new DateTime();
            DateTime Date = new DateTime(2020, 01, 02);
            while (Date.Year != 2021)
            {
                if (this[Date] && !this[Date.AddDays(-1)])
                {
                    startDate = Date;
                    Date = Date.AddDays(1);
                }
                else if (this[Date] && Date.AddDays(1).Year != Date.Year)
                {
                    endDate = Date;
                    str += startDate.ToString("dd/MM") + "-" + endDate.ToString("dd/MM") + "\n";
                    Date = Date.AddDays(1);
                }
                else if (this[Date] && !this[Date.AddDays(1)])
                {
                    endDate = Date.AddDays(1);
                    str += startDate.ToString("dd/MM") + "-" + endDate.ToString("dd/MM") + "\n";
                    Date = Date.AddDays(1);
                }
                else
                {
                    Date = Date.AddDays(1);
                }
            }
            return str;
        }

        public bool this[DateTime index]
        {
            get { return Diary[index.Month - 1, index.Day - 1]; }
            set { Diary[index.Month - 1, index.Day - 1] = value; }
        }
    }
}
