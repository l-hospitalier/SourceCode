using HidLibrary;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace USB_IO_4   // for Km2net USB-IO FS-30
{
    public partial class Form1 : Form
    {
        const int duration_1 = 50;
        static object sync = new object();
        public bool flag_1 = true;
        USBIO2Device device = USBIO2Device.Connect();
         
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  // Flashing !
        {
            button1.Enabled = false;
            flag_1 = true;
            //var device = USBIO2Device.Connect();

            Task task = Task.Factory.StartNew(() =>
            {   // TASK のラムダ式構文
                do
                {
                    device.SendReceive(0xFF, 0xFF, 0xFF, 0xFF);
                    //await Task.Delay(TimeSpan.FromMilliseconds(100));
                    Thread.Sleep(duration_1);
                    device.SendReceive(0x00, 0x00, 0x00, 0x00);
                    //await Task.Delay(TimeSpan.FromMilliseconds(100));
                    Thread.Sleep(duration_1);
                }
                while (flag_1);
                device.SendReceive(0, 0, 0, 0);
            });
        }

         private void button2_Click(object sender, EventArgs e)   // Passing !
        {
            byte J1, J2, J3, J4;
            button2.Enabled = false;
            flag_1 = true;
            //var device = USBIO2Device.Connect();

            Task task = Task.Factory.StartNew(() =>
            {   // TASK のラムダ式構文
                do
                {
                    for (byte i = 0; i < 255; i++)
                    {
                        //lock (sync)
                        {
                            /*  J1 = (byte)Math.Pow(2, i);
                              J2 = (byte)Math.Pow(2, i); 
                              J3 = (byte)Math.Pow(2, i); 
                              J4 = (byte)Math.Pow(2, i); */
                            J1 = J2 = J3 = J4 = i;
                            device.SendReceive(J1, J2, J3, J4);
                            Thread.Sleep(duration_1);
                        }
                    }
                } while (flag_1);
                device.SendReceive(0, 0, 0, 0);
            });
        }

        private void button3_Click(object sender, EventArgs e)    // Halt
        {
            flag_1 = false;
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)   // $FF  All set
        {
            device.SendReceive(0xFF, 0xFF, 0xFF, 0xFF);
        }

        private void button5_Click(object sender, EventArgs e)  // $00 All clear
        {
            device.SendReceive(0x00, 0x00, 0x00, 0x00);
        }
    }

    public class USBIO2Device : IDisposable
    {
        private const int VendorID = 0x1352;
    //  private const int ProductID = 0x0121;
        private const int ProductID = 0x0111; // FS-30

        bool disposed = false;
        readonly HidDevice device;

        public static HidDevice[] ListDevices() => HidDevices.Enumerate(VendorID, ProductID).ToArray();
        
        public static USBIO2Device Connect(HidDevice device) => new USBIO2Device(device);

        /*public static USBIO2Device Connect(HidDevice device)
            {  return new USBIO2Device(device); }*/
        
        public static USBIO2Device Connect()
        {
            var device = HidDevices.Enumerate(VendorID, ProductID).FirstOrDefault();
            if (device == null)
            {
                throw new InvalidOperationException("Deviceが見つかりません");
            }
            return new USBIO2Device(device);
        }

        private USBIO2Device(HidDevice device)
        {
            this.device = device;
        }

        public void Dispose()
        {
            Dispose(true);    // Dispose of unmanaged resources.
            GC.SuppressFinalize(this);    // Suppress finalization.
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) device.Dispose();
            disposed = true;
        }
        
        public (byte B1, byte B2, byte B3, byte B4) SendReceive(byte B1, byte B2, byte B3, byte B4)
        {
            var sendData = new byte[64];

            sendData[1] = 0x20;  // コマンド 
            sendData[2] = 0x01;  // データ
            sendData[3] = B1;
            sendData[4] = 0x02;
            sendData[5] = B2;
            sendData[6] = 0x03;  // コマンド 
            sendData[7] = B3;    // データ
            sendData[8] = 0x04;
            sendData[9] = B4;

            var res = device.Write(sendData);
            if (!res)
                throw new InvalidOperationException("送信に失敗しました");
            var receiveData = device.Read();
            if (receiveData.Status != HidDeviceData.ReadStatus.Success)
                throw new InvalidOperationException($"受信に失敗しました: {receiveData.Status}");
            return (receiveData.Data[3], receiveData.Data[5], receiveData.Data[7], receiveData.Data[9]);
        }
    }
}
