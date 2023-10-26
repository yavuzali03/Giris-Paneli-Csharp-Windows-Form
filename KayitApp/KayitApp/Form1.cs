using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace KayitApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(700, 500); // açılan pencerenin boyutu
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //yeniden boyutlandırmayı engeller

        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            string connectionString = @" //Sql bağlantı adresi"; //Sql Bağlantı adresini yazınız.
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();

            string sqlAdress = "SELECT COUNT(*) FROM parola WHERE ad = @ad AND şifre = @şifre"; //Sql'den kullanıcı bilgilerinin alındığı adresler
            SqlCommand komut = new SqlCommand(sqlAdress, baglanti);

            //textbox'lara girilen bilgilerle Sql'deki bilgilerin karşılaştırılması
            komut.Parameters.AddWithValue("@ad", textBox1.Text); 
                komut.Parameters.AddWithValue("@şifre", textBox2.Text);

                int result = (int)komut.ExecuteScalar();

                if (result > 0)
                {
                    Form2 form2 = new Form2();
                    form2.Show();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış.");
                }
            

        }
                //Şifremi Unuttum butonuna tıklanınca form3'e geçme komutu
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 fr3 = new Form3();
            fr3.Show();
        }
    }
}