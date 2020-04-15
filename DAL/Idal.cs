using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
    {
        bool AddClientRequest(GuestRequest Grect);
        bool UpdateClientRequest(GuestRequest Up);
        bool AddHostingUnit(HostingUnit Hunit);
        bool DeleteHostingUnit(HostingUnit Dunit);
        bool UpdateHostingUnit(HostingUnit Uunit);
        bool AddOrder(Order Aor);
        bool UpdateOrder(Order Uor);
        List <HostingUnit> Lunit { get; }
        List<GuestRequest> LGrequest { get; }
        List<Order> Lorder { get;}
        List<BankBranch> Lbank { get; }
    }
}
