using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Cirrious.Sphero.WorkBench.WinRTHackService
{
    public partial class SpheroBlueToothService : ServiceBase
    {
        private ServiceHost _serviceHost;

        public SpheroBlueToothService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
            }

            // Create a ServiceHost for the BluetoothService type and 
            // provide the base address.
            _serviceHost = new ServiceHost(typeof(SpheroBluetoothWcfService));

            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            _serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
        }
    }
}
