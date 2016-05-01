using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data
{
    public enum ServiceStatus :int
    {
        pending=0,
        Processing=10,
        Complated=30,
    }
}
