using HidLibrary;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SetIUSBIO_FS30
{
    public partial class Form1 : Form
    {
        const int duration_1 = 150;
        static object sync = new object();
        public bool flag_1 = false;

        static byte[] sendData = new byte[64];
        USBIO2Device device = USBIO2Device.Connect();

        public Form1()
        {
            InitializeComponent();
        }

        public class USBIO2Device : IDisposable
        {
            private const int VendorID = 0x1352;
            private const int ProductID = 0x0111; // FS-30

            bool disposed = false;
            readonly HidDevice device;

            public static HidDevice[] ListDevices() => HidDevices.Enumerate(VendorID, ProductID).ToArray();

            public static USBIO2Device Connect(HidDevice device) => new USBIO2Device(device);
         
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

            public void SendReceive()
            {
                var res = device.Write(sendData);
                if (!res)
                    throw new InvalidOperationException("送信に失敗しました");
                var receiveData = device.Read();
                if (receiveData.Status != HidDeviceData.ReadStatus.Success)
                    throw new InvalidOperationException($"受信に失敗しました: {receiveData.Status}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 0  固定
            sendData[1] = 0xF9;
            sendData[2] = 0xFF;
            // 3　不使用
            // 4  不使用
            sendData[5] = (byte)Convert.ToInt32(textBox2.Text, 2);  // 
            sendData[6] = (byte)Convert.ToInt32(textBox3.Text, 2);  //
            sendData[7] = (byte)Convert.ToInt32(textBox4.Text, 2);  // 
            sendData[8] = (byte)Convert.ToInt32(textBox5.Text, 2);  //
            sendData[9] = (byte)Convert.ToInt32(textBox6.Text, 2);  // 
            sendData[10] = (byte)Convert.ToInt32(textBox7.Text, 2);  // 
            sendData[11] = (byte)Convert.ToInt32(textBox8.Text, 2);  // 
            sendData[12] = (byte)Convert.ToInt32(textBox9.Text, 2);  // 
             // 13 割り込みタイミング
            var device = USBIO2Device.Connect();
            device.SendReceive();
            MessageBox.Show("設定完了、デバイスを抜き差ししてください", "", MessageBoxButtons.OK);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            sendData[2] = byte.Parse(textBox1.Text);  //AD チャンネル数
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) 
            { sendData[3] = (byte) (sendData[3] ^ 0x01); }   //ポート 2 プルアップ無効 Bit 1
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            { sendData[3] = (byte)(sendData[3] ^ 0x04);  }   //ポート 4 プルアップ無効 Bit 3
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) sendData[14] = (byte)(sendData[14] ^ 0xFF);  //PWM 有効
        }
    }
}