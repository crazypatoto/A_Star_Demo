using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCS.Communication
{
    internal enum Command
    {
        CheckConnection,
        GetMapData,
        GetRackInfo,
        AssignNewWorkOrder,
        GetWorkOrderState,
        CancelWorkOrder,
        GetCurrentWorkOrderUUID,
    }
}
