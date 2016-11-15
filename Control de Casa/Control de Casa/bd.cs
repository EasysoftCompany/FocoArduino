using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Control_de_Casa
{
    public class bd
    {

        public static MySqlConnection ObtenerConexion()
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "localhost";
                builder.UserID = "root";
                builder.Password = "34syS0ft";
                builder.Database = "superpase";


                MySqlConnection conn = new MySqlConnection(builder.ToString());
                conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error.. " + ex);
                return null;
            }
        }


    }
}