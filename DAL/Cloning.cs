﻿using System;
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
            t.BranchName = b.BranchName;
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
            t.BankBranchDetails = h.BankBranchDetails.Clone();
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
            t.Diary = new bool[12, 31];
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    t.Diary[i, j] = u.Diary[i, j];
                }
            }
            t.HostingUnitKey = u.HostingUnitKey;
            t.HostingUnitName = u.HostingUnitName;
            t.Owner = u.Owner.Clone();
            t.Area = u.Area;
            t.Adults = u.Adults;
            t.Children = u.Children;
            t.Garden = u.Garden;
            t.Jacuzzi = u.Jacuzzi;
            t.Pool = u.Pool;
            t.Rooms = u.Rooms;
            t.Type = u.Type;
            t.ChildrensAttractions = u.ChildrensAttractions;
            
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
