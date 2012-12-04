using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Cirrious.Sphero.WorkBench.WinRTHackService;

namespace Cirrious.MvvmCross.Plugins.Sphero.WinRT.Tooth
{
    public class HackSingleton
    {
        private readonly ISpheroBluetoothWcfService _service;
        public static readonly HackSingleton Instance = new HackSingleton();

        private const string Address = "http://localhost:9000/Cirrious.Sphero.WorkBench.WinRTHackService/SpheroBluetoothWcfService";

        public HackSingleton()
        {
            var binding = new BasicHttpBinding();
            var factory = new ChannelFactory<ISpheroBluetoothWcfService>(binding, new EndpointAddress(Address));
            _service = factory.CreateChannel();
        }

        public ISpheroBluetoothWcfService Service { get { return _service; } }
    }
}
