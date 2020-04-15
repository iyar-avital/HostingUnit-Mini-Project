using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        bool AddClientRequest(GuestRequest Grect);
        bool UpdateClientRequest(GuestRequest Up);
        bool AddHostingUnit(HostingUnit Hunit);
        bool DeleteHostingUnit(HostingUnit Dunit);
        bool UpdateHostingUnit(HostingUnit Uunit);
        bool AddOrder(Order Aor);
        bool UpdateOrder(Order Uor);

        GuestRequest GetClientRequest(int GKey);
        HostingUnit GetHostingUnit(int HKey);
        Order GetOrder(int OKey);

        List<HostingUnit> Lunit();
        List<GuestRequest> LGrequest();
        List<Order> Lorder();
        List<BankBranch> Lbank();
    }
}
