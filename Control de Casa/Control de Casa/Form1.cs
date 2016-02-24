using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control_de_Casa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            puertosDisponibles();
        }

        private void puertosDisponibles()
        {
            String[] puertos = System.IO.Ports.SerialPort.GetPortNames();
            cbo_puertos.Items.Clear();

            foreach (String puerto in puertos)
            {
                cbo_puertos.Items.Add(puerto);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbo_puertos.SelectedItem != null)
            {
                if (button1.Text == "Prender")
                {
                    serialPort1.PortName = cbo_puertos.SelectedItem.ToString();
                    serialPort1.BaudRate = 9600;
                    serialPort1.Open();

                    serialPort1.Write("1");
                    serialPort1.Close();

                    button1.Text = "Apagar";

                }
                else
                {
                    serialPort1.PortName = cbo_puertos.SelectedItem.ToString();
                    serialPort1.BaudRate = 9600;
                    serialPort1.Open();

                    serialPort1.Write("a");
                    serialPort1.Close();

                    button1.Text = "Prender";
                }
            }
            else
            {
                MessageBox.Show("Seleccione un puerto de destino", "Se te olvido el puerto");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Seleccione de los Puertos disponibles el que sea propio del microcontrolador ARDUINO", "Ayuda");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            puertosDisponibles();
        }
    }
}
