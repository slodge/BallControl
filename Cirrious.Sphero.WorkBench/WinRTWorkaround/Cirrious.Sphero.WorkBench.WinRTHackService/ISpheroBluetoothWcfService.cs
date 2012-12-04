using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cirrious.Sphero.WorkBench.WinRTHackService
{
    [ServiceContract]
    public interface ISpheroBluetoothWcfService
    {
        [OperationContract]
        void Reset();

        [OperationContract]
        string[] GetAvailableSpheroNames();

        [OperationContract]
        bool ConnectToSphero(string spheroName);

        [OperationContract]
        bool SendToSphero(byte[] data);

        [OperationContract]
        byte[] ReceiveFromSphero(int max);
    }
}
