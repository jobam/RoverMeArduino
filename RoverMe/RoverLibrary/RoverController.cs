using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.Devices.Enumeration;
using System.Diagnostics;

namespace RoverLibrary
{
    public class RoverController
    {
        #region Defines for arduino controls

        string foward = "1";
        string left = "2";
        string stop = "3";
        string right = "4";
        string backward = "5";

        #endregion

        #region Properties and Attributes

        private RfcommDeviceService device;
        private StreamSocket socket;
        private DataWriter writer;
        public DeviceInformationCollection Devices { get; set; }
        public bool IsConnected { get; set; }

        #endregion

        public RoverController()
        {
            initialize();
        }

        public async Task<bool> Connect(string deviceName)
        {
            try
            {
                if (this.Devices.Count > 0)
                {
                    // getting selected Device
                    this.device = await RfcommDeviceService.FromIdAsync(
                        Devices.Where(x => x.Name == deviceName).FirstOrDefault().Id);

                    // Oppening Socket
                    this.socket = new StreamSocket();
                    await this.socket.ConnectAsync(device.ConnectionHostName, device.ConnectionServiceName,
                        SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication);

                    // Writer
                    this.writer = new DataWriter(this.socket.OutputStream);
                    this.IsConnected = true;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                this.IsConnected = false;
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                this.socket.Dispose();
                this.device.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Rover Commands

        public async Task<bool> GoFoward()
        {
            try
            {
                string message = this.foward;
                writer.WriteString(message);
                await writer.StoreAsync();
                await writer.FlushAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GoBackward()
        {
            try
            {
                string message = this.backward;
                writer.WriteString(message);
                await writer.StoreAsync();
                await writer.FlushAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GoLeft()
        {
            try
            {
                string message = this.left;
                writer.WriteString(message);
                await writer.StoreAsync();
                await writer.FlushAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GoRight()
        {
            try
            {
                string message = this.right;
                writer.WriteString(message);
                await writer.StoreAsync();
                await writer.FlushAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GoStop()  // The name respect the "Go" convention
        {
            try
            {
                string message = this.stop;
                writer.WriteString(message);
                await writer.StoreAsync();
                return await writer.FlushAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region helpers

        public void initialize()
        {
            // Getting devices List
             RefreshDevicesList();

        }

        public List<string> GetDevicesList()
        {
            try {
                return this.Devices.Select(x => x.Name).ToList();
            }catch (Exception e)
            {
                Debug.WriteLine("Error getting devices list: " + e.Message);
                return new List<string>();
            }
        }

        public async void RefreshDevicesList()
        {
            this.Devices = await DeviceInformation.FindAllAsync(
               RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));
        }

        #endregion
    }
}
