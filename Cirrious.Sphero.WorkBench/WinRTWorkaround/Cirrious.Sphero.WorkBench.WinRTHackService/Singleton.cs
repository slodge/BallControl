using System;
using System.Linq;
using System.Net.Sockets;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace Cirrious.Sphero.WorkBench.WinRTHackService
{
    public class Singleton : ISpheroBluetoothWcfService
    {
        public readonly static Singleton Instance = new Singleton();

        private BluetoothClient _client;
        private BluetoothDeviceInfo[] _peers;
        private NetworkStream _stream;

        public Singleton()
        {
        }

        public void Reset()
        {
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }

            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }
        }

        public string[] GetAvailableSpheroNames()
        {
            Reset();

            _client = new BluetoothClient();
            _peers = _client.DiscoverDevices();
            var spheros = _peers.Where(x => x.DeviceName.StartsWith("Sphero")).Select(x => x.DeviceName).ToArray();
            return spheros;
        }

        public bool ConnectToSphero(string spheroName)
        {
            if (_client == null)
                return false;

            if (_peers == null)
                return false;

            var device = _peers.FirstOrDefault(x => x.DeviceName == spheroName);
            if (device == null)
                return false;

            try
            {
                var serviceClass = BluetoothService.SerialPort;
                var ep = new BluetoothEndPoint(device.DeviceAddress, serviceClass);
                _client.Connect(ep);
                _stream = _client.GetStream();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SendToSphero(byte[] data)
        {
            try
            {
                _stream.Write(data, 0, data.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public byte[] ReceiveFromSphero(int max)
        {
            try
            {
                var toReturn = new byte[max];
                var read = _stream.Read(toReturn, 0, max);
                if (read <= 0)
                {
                    return null;
                }
                if (read < max)
                {
                    var temp = new byte[read];
                    for (var i = 0; i < read; i++)
                    {
                        temp[i] = toReturn[i];
                    }
                    toReturn = temp;
                }
                return toReturn;
            }
            catch (Exception)
            {
                // in this case we just rethrow
                throw;
            }
        }
    }
}