using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cirrious.Sphero.WorkBench.WinRTHackService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SpheroBluetoothWcfService" in both code and config file together.
    public class SpheroBluetoothWcfService : ISpheroBluetoothWcfService
    {
        public void Reset()
        {
            Singleton.Instance.Reset();
        }

        public string[] GetAvailableSpheroNames()
        {
            return Singleton.Instance.GetAvailableSpheroNames();
        }

        public bool ConnectToSphero(string spheroName)
        {
            return Singleton.Instance.ConnectToSphero(spheroName);
        }

        public bool SendToSphero(byte[] data)
        {
            return Singleton.Instance.SendToSphero(data);
        }

        public byte[] ReceiveFromSphero(int max)
        {
            return Singleton.Instance.ReceiveFromSphero(max);
        }
    }
}
