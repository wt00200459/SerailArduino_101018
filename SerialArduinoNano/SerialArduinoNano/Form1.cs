using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace SerialArduinoNano
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            //4.use delegate function in form because Form1 will run one time
            //after Run Program
            myDelegate = new AddDataDelegate(AddData);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(ports);
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(ports);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.BaudRate = Int32.Parse(comboBox2.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                
                return;
            } else
            {
                serialPort1.Open();
                button2.Enabled = false;
                button3.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                return;

            } else
            {
                serialPort1.Close();
                button2.Enabled = true;
                button3.Enabled = false;
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string str = sp.ReadExisting();
            Console.WriteLine(str);
            textBox1.Invoke(myDelegate, str);//cross thread problem

        }
        //1.declare delegate 
        public void AddData(string str)
        {
            textBox1.AppendText(str);
        }
        //2.define delegate
        public delegate void AddDataDelegate(string str);
        //3.create delegate ref
        public AddDataDelegate myDelegate;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
