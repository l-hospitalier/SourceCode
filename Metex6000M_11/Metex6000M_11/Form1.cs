using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

using System.Threading;

namespace Metex6000M_11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM1"; // COM1
            serialPort1.BaudRate = 19200;
            serialPort1.DataBits = 7;
            serialPort1.Parity = Parity.Odd;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Handshake = Handshake.None;

            serialPort1.RtsEnable = true;
            serialPort1.DtrEnable = true;
            //ApplicationExitイベントハンドラを追加
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            if (serialPort1.IsOpen==false) serialPort1.Open();
            serialPort1.RtsEnable = true;
            serialPort1.DtrEnable = true;
            SendCommBreak(150);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) serialPort1.Open();
            serialPort1.RtsEnable = true;
            serialPort1.DtrEnable = true;
            SendCommBreak(200);
        }

        string s1, s2;
        bool first;

        private void SetReceiveData(string dataString)
        {
            s1 = dataString.Substring(5, 1);
            if (s1 == ">")
            {
                s2 = dataString.Substring(0, 5) + " Lux\r\n";
                while (s2.Substring(0, 1) == "0") s2 = s2.Substring(1);
            }
            else if (s1 == "<") 
            {
                s2 = dataString.Substring(0, 4) + "." + dataString.Substring(4, 1) + " dB\r\n";
                while (s2.Substring(0, 1) == "0") s2 = s2.Substring(1);
            }
            if (first) textBox1.AppendText(s2);
            first=!first;
        }

        /* SendCommBreak(int interval) intrval msec Break信号を送る。*/
        private void SendCommBreak(int interval)
        {

            if (serialPort1.IsOpen == false) serialPort1.Open();
            
            if (serialPort1.BreakState == true) 
                   MessageBox.Show(" BreakeState is " + serialPort1.BreakState.ToString());
                        
            serialPort1.BreakState = true;
            Thread.Sleep(interval);
            serialPort1.BreakState = false;
            Thread.Sleep(interval);
        }

		//Delegate 宣言
		private delegate void ReceiveDataDelegate(string receiveData);
		private void serialPort1_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            string receiveData;
            ReceiveDataDelegate receive = new ReceiveDataDelegate(SetReceiveData);

            try
            {
                receiveData = serialPort1.ReadLine();
            }
            catch (Exception ex)
            {
                receiveData = ex.Message;
            }
            Invoke(receive, receiveData);  // receive delegate を invoke で呼び出す
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            //MessageBox.Show("アプリケーションが終了されます。");
            if (serialPort1.IsOpen == false) serialPort1.Open();
            serialPort1.RtsEnable = true;
            serialPort1.DtrEnable = true;
            SendCommBreak(150);
            serialPort1.Close();
            //ApplicationExitイベントハンドラを削除
            Application.ApplicationExit -= new EventHandler(Application_ApplicationExit);
        }
 
        private void button2_Click(object sender, EventArgs e)
		{
			// すべてのシリアル・ポート名を取得する
			string[] ports = SerialPort.GetPortNames();

			// 取得したシリアル・ポート名を出力する
			foreach (string port in ports)
			{
				listBox1.Items.Add(port);
			}
		}
    }
}

