using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cirrious.Sphero.WorkBench.WinRTHackService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISpheroBluetoothService" in both code and config file together.
    [ServiceContract]
    public interface ISpheroBluetoothService
    {
        [OperationContract]
        void DoWork();
    }
}
