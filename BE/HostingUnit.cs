using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit
    {
        public int HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        public bool[,] Diary { get; set; }
        public Areas Area { get; set; }
        public Type_Unit Type { get; set; }
        public int Adults { get; set; }
        public int Rooms { get; set; }
        public int Children { get; set; }
        public bool Pool { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Garden { get; set; }
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
