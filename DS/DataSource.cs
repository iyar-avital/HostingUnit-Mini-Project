using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DS
{
    public class DataSource
    {
        public static List<HostingUnit> ListHostingUnits = new List<HostingUnit>
        {
            new HostingUnit
            {
                HostingUnitKey=00000001,
                Owner = new Host
                {
                    HostKey = 00000002,
                    PrivateName="Yoosi",
                    FamilyName = "Cohen",
                    PhoneNumber = 0509876543,
                    MailAddress="ddd@gmail.com",
                    BankBranchDetails= new BankBranch
                    {
                     BankNumber=12,
                     BankName="Discont",
                     BranchNumber=78,
                     BranchAddress=" yafo 67",
                     BranchCity=" Tel-Aviv",
                    },
                    BankAccountNumber=166685,
                    CollectionClearance=true,
                },
                  HostingUnitName= "Zimmer Nof",
                  Diary = new bool[12, 31]
            },

            new HostingUnit
            {
                HostingUnitKey=00000002,
                Owner = new Host
                {
                    HostKey = 00000004,
                    PrivateName="Yaron",
                    FamilyName = "fox",
                    PhoneNumber = 0509996543,
                    MailAddress="Yfox@gmail.com",

                    BankBranchDetails= new BankBranch
                    {
                     BankNumber=12,
                     BankName="Discont",
                     BranchNumber=70,
                     BranchAddress=" yafo 67",
                     BranchCity=" Tel-Aviv",
                    },

                    BankAccountNumber=166789,
                    CollectionClearance=true,
                },
                  HostingUnitName= "Zimmer Glili",
                  Diary = new bool[12, 31]
            },

            new HostingUnit
            {
                HostingUnitKey=00000003,

                Owner =new Host
                {
                    HostKey = 00000006,
                    PrivateName="Shir",
                    FamilyName = "Saon",
                    PhoneNumber = 0509876543,
                    MailAddress="ShirShir@gmail.com",

                    BankBranchDetails= new BankBranch
                    {
                     BankNumber=13,
                     BankName="Mercantil",
                     BranchNumber=78,
                     BranchAddress=" Erez 8",
                     BranchCity=" Yahod",
                    },
                    BankAccountNumber=188685,
                    CollectionClearance=true,
                },
                  HostingUnitName= "Maaahal",
                  Diary = new bool[12, 31]
            }
        };


        public static List<GuestRequest> ListGuestRequest = new List<GuestRequest>
        {
            new GuestRequest
            {
                GuestRequestKey = 10000000,
                PrivateName = "Yael",
                FamilyName = "Gold",
                MailAddress = "yg@gamil.com",
                StatusRequest = Request_Status.Active,
                RegistrationDate = DateTime.Now,
                EntryDate = new DateTime(2020, 2, 4),
                ReleaseDate= new DateTime(2020, 2, 7),
                Area = Request_Area.North,
                SubArea = Request_SubArea.Hermon,
                Type = Request_Type.HostingUnit,
                Adults = 3,
                Children = 4,
                Pool = Request_Pool.Necessary,
                Jacuzzi = Request_Jacuzzi.Not_interested,
                Garden = Request_Garden.Possible,
                ChildrensAttractions = Request_ChildrensAttractions. Necessary,
            },

            new GuestRequest
            {
                GuestRequestKey = 20000000,
                PrivateName = "Shira",
                FamilyName = "Hadar",
                MailAddress = "shirah@gamil.com",
                StatusRequest = Request_Status.Active,
                RegistrationDate = DateTime.Now,
                EntryDate = new DateTime(2020, 3, 5),
                ReleaseDate= new DateTime(2020, 3, 6),
                Area = Request_Area.Center,
                SubArea = Request_SubArea.Tel_Aviv,
                Type = Request_Type.Zimmer,
                Adults = 5,
                Children = 1,
                Jacuzzi = Request_Jacuzzi.Possible,
                Garden = Request_Garden.Not_interested,
                Pool = Request_Pool.Necessary,
                ChildrensAttractions = Request_ChildrensAttractions. Not_interested,
            },

            new GuestRequest
            {
                GuestRequestKey = 30000000,
                PrivateName = "Noa",
                FamilyName = "Halfon",
                MailAddress = "noa@gamil.com",
                StatusRequest = Request_Status.Active,
                RegistrationDate = DateTime.Now,
                EntryDate = new DateTime(2020, 2, 8),
                ReleaseDate= new DateTime(2020, 2, 10),
                Area = Request_Area.Jerusalem,
                SubArea =Request_SubArea.Jerusalem,
                Type = Request_Type.HotelRoom,
                Adults = 2,
                Children = 2,
                Pool = Request_Pool.Possible,
                Jacuzzi =Request_Jacuzzi.Not_interested,
                Garden = Request_Garden.Not_interested,
                ChildrensAttractions = Request_ChildrensAttractions. Possible,
            }
        };


        public static List<Order> ListOrder = new List<Order>
        {
            new Order
            {
                HostingUnitKey = 00000001,
                GuestRequestKey = 10000000,
                OrderKey = 001,
                StatusOrder =OrderStatus.Sent_Mail,
                CreateDate = new DateTime(2020, 2, 10),
                OrderDate = new DateTime(2020, 3, 10),
            },

            new Order
            {
                HostingUnitKey = 00000002,
                GuestRequestKey = 20000000,
                OrderKey = 002,
                StatusOrder = OrderStatus.Sent_Mail,
                CreateDate = new DateTime(2020, 4, 20),
                OrderDate = new DateTime(2020, 4, 23),
            },

            new Order
            {
                HostingUnitKey = 00000003,
                GuestRequestKey = 30000000,
                OrderKey = 003,
                StatusOrder = OrderStatus.Sent_Mail,
                CreateDate = new DateTime(2020, 3, 1),
                OrderDate = new DateTime(2020, 3, 15),
            }

        };

        public bool[,] Diary = new bool[13, 32];
        public static List<BankBranch> ListBankBranch = new List<BankBranch>
        {
            new BankBranch
            {
            BankNumber = 12,
            BankName="הפועלים",
            BranchNumber=123,
            BranchAddress="רבי עקיבא 67",
            BranchCity="בני ברק",
            }
        };
    }
}
