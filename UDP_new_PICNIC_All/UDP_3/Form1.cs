
// TriState new PIC-NIC用　UDP通信によるA/D値　パラレルポート制御　LCD制御　C#プログラム
// VS2019  IP 192.158.1.9   LCD_port = 10000, parallel_port = 10001, 
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace UDP_3
{
    public partial class Form1 : Form
    {
        static int localPort = 9999;
        static int lcdPort = 10000;
        static int parallelPort = 10001;
        //static int serialPort = 10002;
        static string remoteIP = "192.168.1.9";
        static string localIP = "127.0.0.1";

        IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(localIP), localPort);
        //IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
        static IPEndPoint parallelEP = new IPEndPoint(IPAddress.Parse(remoteIP), parallelPort);
        static IPEndPoint lcdEP = new IPEndPoint(IPAddress.Parse(remoteIP), lcdPort);
        
        // ソケット生成
        UdpClient ParallelSock = new UdpClient(parallelEP.AddressFamily);
        UdpClient LcdSock = new UdpClient(lcdEP.AddressFamily);

        bool boo = false;
        byte m;
        double temp;   //byte RJ, RE, TRIS_RJ, TRIS_RE, AD_H, AD_L;
        byte[] status = new byte[8];
        byte[] command = new byte[256];
        string st1 = " 192.168.1.9 ";
        string st2 = "TriState PIC-NIC";

        public Form1()
        {
            InitializeComponent();
        }
        void SetText()
        {
             textBox24.Text = "RJ = " + status[0].ToString();
             textBox16.Text = "RE = " + status[1].ToString();

             temp = ((status[4] & 0x03) * 16.0 + status[5]) * 0.48828125;

            if (m == 3) { listBox1.Items.Add(" " + temp.ToString("f3") + " ℃ "); }
            //listBox1.Items.Add("Temp = " + temp.ToString("f3") + " ℃ ");
            if (m == 0) { textBox1.Text = temp.ToString("f2"); }
                else if (m == 1) { textBox2.Text = temp.ToString("f2"); }
                else if (m == 2) { textBox3.Text = temp.ToString("f2"); }
                else if (m == 3) { textBox4.Text = temp.ToString("f2"); }
                else if (m == 4) { textBox5.Text = temp.ToString("f2"); }
                else if (m == 5) { textBox6.Text = temp.ToString("f2"); }
                else if (m == 6) { textBox7.Text = temp.ToString("f2"); }
                else if (m == 7) { textBox8.Text = temp.ToString("f2"); }
           
            //listBox1.Items.Add("localEP = " + localEP.ToString());
            //listBox1.Items.Add("parallelEP = " + parallelEP.ToString());

            //   [C#]
               if (listBox1.Height < listBox1.PreferredHeight) 
               listBox1.TopIndex = listBox1.Items.Count - 1;

            //listBox1.SelectedIndex = listBox1.Items.Count - 1;
            //listBox1.SelectedIndex = -1;  // to erase cursor line 

        }
       
        // Udp非同期通信のコールバック
        void ReceiveCallback(IAsyncResult ar)
        {
            // データ受信
            status = ParallelSock.EndReceive(ar, ref parallelEP);
            // 受信したデータの処理
            if (this.InvokeRequired)
                Invoke((MethodInvoker) delegate { SetText(); });
            else  SetText();
            // 非同期通信のコールバック関数を再設定。 次のデータ受信のため
            ParallelSock.BeginReceive(ReceiveCallback, ParallelSock);
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ParallelSock.Close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (boo) label4.Text = "⁂"; else label4.Text = "";
            boo = !boo;

            ParallelSock.Connect(parallelEP);
           
            label2.Text = "parallelEP = " + parallelEP.ToString();

            command[0] = 0x04;
            command[1] = (byte)(0x81 + (m * 8));
            command[2] = 0x01;

            ParallelSock.Send(command, command.GetLength(0));
            ParallelSock.BeginReceive(ReceiveCallback, ParallelSock);
            //    if (this.InvokeRequired)
            //        Invoke((MethodInvoker)delegate { SetText(); });
            //    else SetText();
            //    textBox24.Text = "RJ = " + status[0].ToString();
            //    textBox16.Text = "RE = " + status[1].ToString();
            m++;
            if (m > 7)  m = (byte)(m - 8);         }

        private void RJ_SET(byte n)
        {
            command[0] = 0x01;
            command[1] = 0x05;
            command[2] = n;
            ParallelSock.Send(command, command.GetLength(0));
            command[0] = 0x00;
            ParallelSock.Send(command, command.GetLength(0));
        }

        private void RJ_CLEAR(byte n)
        {
            command[0] = 0x02;
            command[1] = 0x05;
            command[2] = n;
            ParallelSock.Send(command, command.GetLength(0));
            command[0] = 0x00;
            ParallelSock.Send(command, command.GetLength(0));
        }

        private void RE_SET(byte n)
        {
            command[0] = 0x01;
            command[1] = 0x06;
            command[2] = n;
            ParallelSock.Send(command, command.GetLength(0));
            command[0] = 0x00;
            ParallelSock.Send(command, command.GetLength(0));
        }

        private void RE_CLEAR(byte n)
        {
            command[0] = 0x02;
            command[1] = 0x06;
            command[2] = n;
            ParallelSock.Send(command, command.GetLength(0));
            command[0] = 0x00;
            ParallelSock.Send(command, command.GetLength(0));
        }
  
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) RE_SET(0);
            else RE_CLEAR(0);
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked) RJ_SET(0);
            else RJ_CLEAR(0);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) RE_SET(1);
            else RE_CLEAR(1);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) RE_SET(2);
            else RE_CLEAR(2);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) RE_SET(3);
            else RE_CLEAR(3);
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked) RE_SET(4);
            else RE_CLEAR(4);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked) RE_SET(5);
            else RE_CLEAR(5);
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked) RE_SET(6);
            else RE_CLEAR(6);
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked) RE_SET(7);
            else RE_CLEAR(7);
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked) RJ_SET(1);
            else RJ_CLEAR(1);
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked) RJ_SET(2);
            else RJ_CLEAR(2);
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked) RJ_SET(3);
            else RJ_CLEAR(3);
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked) RJ_SET(4);
            else RJ_CLEAR(4);
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked) RJ_SET(5);
            else RJ_CLEAR(5);
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked) RJ_SET(6);
            else RJ_CLEAR(6);
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked) RJ_SET(7);
            else RJ_CLEAR(7);
        }

        private void comclear()
        {
            for (int i = 0; i < 255; i++) command[i] = 0x20;
        }

        private void button1_Click(object sender, EventArgs e)    // LCD_0 Write
        {
            LcdSock.Connect(lcdEP);
            comclear();
            label2.Text = "lcdEP = " + lcdEP.ToString();

            command[0] = 0x01;
            command[1] = 0x80;

            for (int i = 2; i < textBox9.Text.Length + 2; i++)
            command[i] = (byte)textBox9.Text[i - 2]; 
            
            LcdSock.Send(command, command.GetLength(0));
        }

        private void button2_Click(object sender, EventArgs e)    // LCD_1 Write
        {
            LcdSock.Connect(lcdEP);
            comclear();
            label2.Text = "lcdEP = " + lcdEP.ToString();

            command[0] = 0x01;
            command[1] = 0xC0;

            for (int i = 2; i < textBox10.Text.Length + 2; i++)
             command[i] = (byte)textBox10.Text[i - 2];
            LcdSock.Send(command, command.GetLength(0));
        }

        private void button3_Click(object sender, EventArgs e)    // Clear the LCD
        {
            LcdSock.Connect(lcdEP);
            comclear();
            label2.Text = "lcdEP = " + lcdEP.ToString();

            command[0] = 0x01;
            command[1] = 0x01;
            LcdSock.Send(command, command.GetLength(0));

            textBox9.Text = "";
            textBox10.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comclear();
            textBox9.Text = st1;
            textBox10.Text = st2;
        }
        private void button4_Click(object sender, EventArgs e)  //Restore Default
        {
            LcdSock.Connect(lcdEP);
            comclear(); 
            label2.Text = "lcdEP = " + lcdEP.ToString();
            command[0] = 0x01;
            command[1] = 0x80;

            for (int i = 2; i < st1.Length + 2; i++)
            command[i] = (byte)st1[i-2]; 
            LcdSock.Send(command, command.GetLength(0));

            comclear();
            command[0] = 0x01;
            command[1] = 0xC0;
            for (int i = 2; i < st2.Length + 2; i++)
            command[i] = (byte)st2[i-2]; 
            LcdSock.Send(command, command.GetLength(0));

            textBox9.Text = st1;
            textBox10.Text = st2;
        }
    }
}