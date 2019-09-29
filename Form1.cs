using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace PuertoSerie
{
    
    public partial class Form1 : Form
    {
        string dataOUT;
        string dataIN;
        bool status1 = false;
        bool status2 = false;
        public Form1()
        {
            InitializeComponent();
            backgroundWorker2.WorkerReportsProgress = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxComPort.Items.AddRange(ports);
        }
            private void BtnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxComPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(cboxBaudRate.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDataBits.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopBits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxPatityBits.Text);

                serialPort1.Open();
                status2 = true;
                backgroundWorker2.RunWorkerAsync();

                progressBar1.Value = 100;
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void BtnColse_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                status2 = false;
                Thread.Sleep(1000);
                serialPort1.Close();
                progressBar1.Value = 0;
            }
        }

        private void BtnSendData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOUT = tBoxDataOut.Text;
                serialPort1.WriteLine(dataOUT);
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            
            while (status2)
            {
                dataIN = serialPort1.ReadLine();
                backgroundWorker2.ReportProgress(1);
                //if (serialPort1.IsOpen)
                //{
                //   dataIN = "";
                //    while (serialPort1.BytesToRead > 0)
                //    {
                //        dataIN += serialPort1.ReadChar();
                //    }
                //    backgroundWorker2.ReportProgress(1);
                //}

            }
        }

        private void BackgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tBoxDataIn.Text += dataIN;
        }
    }
}
