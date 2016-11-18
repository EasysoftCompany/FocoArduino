using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using MySql.Data.MySqlClient;
namespace Control_de_Casa
{
    public partial class pdf : Form
    {
        public pdf()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 5;
            progressBar1.Step = 1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void nombre_pdf_TextChanged(object sender, EventArgs e)
        {

        }

        private void pdf_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.PerformStep();
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@"C:\Users\g_ric\Desktop\" + nombre_pdf.Text + ".pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Pase de liste");
            doc.AddCreator("SUPERPASE-EASYSOFT COMPANY");

            // Abrimos el archivo
            doc.Open();
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph("Pase de Lista"));
            doc.Add(Chunk.NEWLINE);

            // Creamos una tabla que contendrá el nombre, apellido y país
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(3);
            tblPrueba.WidthPercentage = 100;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", _standardFont));
            clNombre.BorderWidth = 0;
            clNombre.BorderWidthBottom = 0.75f;

            PdfPCell clboleta = new PdfPCell(new Phrase("Boleta", _standardFont));
            clboleta.BorderWidth = 0;
            clboleta.BorderWidthBottom = 0.75f;

            PdfPCell cltel = new PdfPCell(new Phrase("Fecha y hora", _standardFont));
            cltel.BorderWidth = 0;
            cltel.BorderWidthBottom = 0.75f;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clboleta);
            tblPrueba.AddCell(cltel);

            progressBar1.PerformStep();
            // Llenamos la tabla con información

            try
            {
                MySqlCommand sql = null;
                sql = new MySqlCommand();

                //Indicamos el Query a ejecutar por el commando;
                sql.CommandText = "sp_get_lista();";
                sql.Connection =bd.ObtenerConexion();


                MySqlDataReader consulta;

                //Como nos interesa recuperar un valor concreto de la base de datos ejecutamos un DataReader
                consulta = sql.ExecuteReader();

                while (consulta.Read())
                {
                    clNombre = new PdfPCell(new Phrase(consulta.GetString(0), _standardFont));
                    clNombre.BorderWidth = 0;

                    clboleta = new PdfPCell(new Phrase(consulta.GetString(1), _standardFont));
                    clboleta.BorderWidth = 0;

                    cltel = new PdfPCell(new Phrase(consulta.GetString(2), _standardFont));
                    cltel.BorderWidth = 0;

                    // Añadimos las celdas a la tabla
                    tblPrueba.AddCell(clNombre);
                    tblPrueba.AddCell(clboleta);
                    tblPrueba.AddCell(cltel);
                }
                progressBar1.PerformStep();

            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.ToString()); }

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(tblPrueba);
            progressBar1.PerformStep();
            doc.Close();
            writer.Close();
            progressBar1.PerformStep();
            MessageBox.Show("PDF generado con exito, se guadro en la ruta C:/Users/g_ric/Desktop/" + nombre_pdf.Text + ".pdf");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            this.Hide();
            fm.Show();
        }
    }
}
