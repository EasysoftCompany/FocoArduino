using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Control_de_Casa
{
    public partial class registro_ok : Form
    {
        public registro_ok()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string txt1 = textBox1.Text;
            string txt2 = textBox2.Text;
            string txt3 = textBox3.Text;
            string txt4 = textBox4.Text;
            string txt5 = textBox5.Text;


            MySqlCommand sql = null;
            MySqlDataReader consulta;
            sql = new MySqlCommand();
            sql.CommandText = "call sp_registrar('" + txt1 + "','" + txt2 + "','" + txt3 + "','" + txt4 + "','" + txt5 + "');";
            sql.Connection = bd.ObtenerConexion();
            consulta = sql.ExecuteReader();

            while (consulta.Read())
            {

                MessageBox.Show(consulta.GetString(0));

            }
            Form1 main = new Form1();
            this.Hide();
            main.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 main = new Form1();
            this.Hide();
            main.Show();
        }
    }
}
