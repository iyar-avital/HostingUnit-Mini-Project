using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.Definitions;

namespace BE
{
    public class HostingUnit
    {
        public int HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        public  bool[,] Diary  { get; set; }
        public Hosting_Area Area { get; set; }
        public Hosting_Type Type { get; set; }
        public int Adults { get; set; }
        public int Rooms { get; set; }
        public int Children { get; set; }
        public bool Pool { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Garden { get; set; }
        public bool ChildrensAttractions { get; set; }
        public List<DateTime> AllOrders { get; set; }
        public List<string> picture { get; set; }
        public override string ToString()
        {
            return HostingUnitKey.ToString() + Owner.ToString() + HostingUnitName.ToString() + Diary.ToString() + Adults.ToString()
                 + Children.ToString()+Pool.ToString()+Jacuzzi.ToString()+Garden.ToString()+ChildrensAttractions.ToString();
        }


        /*public HostingUnit()
        {
            HostingUnitKey = 908089;
            Owner = new Host();
            HostingUnitName = "fff";
            Diary = new bool[13, 32];
            Adults = 8;
            Children = 0;
            Pool = true;
            Jacuzzi = true;
            Garden = true;
            ChildrensAttractions = true;
        }*/
    }
}
