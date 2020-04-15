using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public static class Cloning
    {
        public static BankBranch Clone(this BankBranch b)
        {
            BankBranch t = new BankBranch();
            t.BankName = b.BankName;
            t.BankNumber = b.BankNumber;
            t.BranchAddress = b.BranchAddress;
            t.BranchCity = b.BranchCity;
            t.BranchNumber = b.BranchNumber;
            return t;
        }
        public static GuestRequest Clone(this GuestRequest g)
        {
            GuestRequest t = new GuestRequest();
            t.Adults = g.Adults;
            t.Area = g.Area;
            t.Children = g.Children;
            t.ChildrensAttractions = g.ChildrensAttractions;
            t.EntryDate = g.EntryDate;
            t.FamilyName = g.FamilyName;
            t.Garden = g.Garden;
            t.GuestRequestKey = g.GuestRequestKey;
            t.Jacuzzi = g.Jacuzzi;
            t.MailAddress = g.MailAddress;
            t.Pool = g.Pool;
            t.PrivateName = g.PrivateName;
            t.RegistrationDate = g.RegistrationDate;
            t.ReleaseDate = g.ReleaseDate;
            t.StatusRequest = g.StatusRequest;
            t.SubArea = g.SubArea;
            t.Type = g.Type;
            return t;
        }

        public static Host Clone(this Host h)
        {
            Host t = new Host();
            t.BankAccountNumber = h.BankAccountNumber;
            t.BankBranchDetails = h.BankBranchDetails;
            t.CollectionClearance = h.CollectionClearance;
            t.FamilyName = h.FamilyName;
            t.PhoneNumber = h.PhoneNumber;
            t.HostKey = h.HostKey;
            t.MailAddress = h.MailAddress;
            t.PrivateName = h.PrivateName;
            return t;
        }

        public static HostingUnit Clone(this HostingUnit u)
        {
            HostingUnit t = new HostingUnit();
            t.Diary = u.Diary;
            t.HostingUnitKey = u.HostingUnitKey;
            t.HostingUnitName = u.HostingUnitName;
            t.Owner = u.Owner;
            return t;
        }
        public static Order Clone(this Order o)
        {
            Order t = new Order();
            t.CreateDate = o.CreateDate;
            t.GuestRequestKey = o.GuestRequestKey;
            t.HostingUnitKey = o.HostingUnitKey;
            t.OrderDate = o.OrderDate;
            t.OrderKey = o.OrderKey;
            t.StatusOrder = o.StatusOrder;
            return t;
        }
    }
}
