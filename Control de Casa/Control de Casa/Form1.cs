using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Control_de_Casa
{
    public partial class Form1 : Form
    {
        MySqlCommand sql = null;
        MySqlDataReader consulta;


        public Form1()
        {
            InitializeComponent();
            puertosDisponibles();
         
            sql = new MySqlCommand();



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
                if (button1.Text == "Iniciar")
                {
                  
                    button1.Text = "Parar";
                    MessageBox.Show("Iniciado");
                  
                    serialPort1.PortName = cbo_puertos.SelectedItem.ToString();
                    serialPort1.BaudRate = 9600;
                    try
                    {
                        serialPort1.Open();

                    }catch(Exception ex) { MessageBox.Show(ex.ToString()); }



                }
                else
                {

                    serialPort1.Close();
                    button1.Text = "Iniciar";
                    MessageBox.Show("Parado");
                }

            }
            else
            {
                MessageBox.Show("Seleccione un puerto de destino", "Se le olvido el puerto");
            }
            




        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string indata = serialPort1.ReadLine();
            sql.CommandText = "call sp_pasarlista('"+indata+"');";
            sql.Connection = bd.ObtenerConexion();
            consulta = sql.ExecuteReader();

            while (consulta.Read())
            {
                
               MessageBox.Show(consulta.GetString(0));
                
             }


        }



        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Seleccione de los Puertos disponibles, el que sea propio del microcontrolador ARDUINO", "Ayuda");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            puertosDisponibles();
        }

        private void cbo_puertos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           if(serialPort1.IsOpen)
            {
                serialPort1.Close();
            }

            registro_ok ok = new registro_ok();
            this.Hide();
                ok.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
