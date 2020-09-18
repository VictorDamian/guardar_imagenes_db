using System;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace SaveIMG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conexion = new SqlConnection("Data Source=DANTES\\DANTES; DataBase=Practica_Imagenes; Integrated Security=True");
        SqlCommand cmd;

        string ruta = "";

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "PNG files (*.png)|*.png|jpg files (*.jpg)|*.jpg|All files(*.*)|*.*";
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                ruta = abrir.FileName.ToString();
                pictureBox1.ImageLocation = ruta;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] imagen = null;
            FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(stream);
            imagen = brs.ReadBytes((int)stream.Length);

            conexion.Open();
            string query = "insert into IMAGENES(IMAGEN)Values(@imagen)";
            cmd = new SqlCommand(query, conexion);
            cmd.Parameters.Add(new SqlParameter("@IMAGEN",imagen));
            int n = cmd.ExecuteNonQuery();
            conexion.Close();
            MessageBox.Show(n.ToString()+"Agregado");
        }

        private void cargarTrabajos(string img)
        {
            conexion.Open();
            string query = "select IMAGEN from IMAGENES where IDIMG="+img;
            cmd = new SqlCommand(query, conexion);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                byte[] imgn = ((byte[])rd[0]);
                if (imgn == null)
                {
                    pictureBox2.Image = null;
                }
                else
                {
                    MemoryStream mS = new MemoryStream(imgn);
                    pictureBox2.Image = Image.FromStream(mS);
                }
            }
            conexion.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cargarTrabajos(textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox2.Dispose();
        }
    }
}
