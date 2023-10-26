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
using System.Data.Sql;
using System.Net.Mail;
using System.Net;


namespace KayitApp
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            this.Size = new Size(520, 500); // açılan pencerenin boyutu
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //yeniden boyutlandırmayı engeller
        }
        SqlConnection baglanti;
        SqlDataReader veriOkuma;
        SqlCommand komut;

        string YenilemeMaili = "denemelikhesap03@hotmail.com";
        string YenilemeSifresi = "Denemelik003";
        string yeniSifre;

        private void button1_Click(object sender, EventArgs e)
        {


            baglanti = new SqlConnection(@"Data Source=DESKTOP-NMTOFIC;Initial Catalog=kullaniciBilgileri;Integrated Security=True");
            komut = new SqlCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Select * From parola where ad= '" + textBox1.Text + "' And email='" + textBox2.Text + "' ";
            veriOkuma = komut.ExecuteReader();

            String girilenKod = textBox3.Text;
            String dogrulamaKodu = label3.Text;

            if (girilenKod == dogrulamaKodu)
            {
                if (veriOkuma.Read())
                {
                    Random rand = new Random();
                    yeniSifre = rand.Next(10000, 100000).ToString();
                    veriOkuma.Close();
                    komut = new SqlCommand("UPDATE parola SET şifre = @YeniSifre WHERE ad = @K_Adi", baglanti);
                    komut.Parameters.AddWithValue("@YeniSifre", yeniSifre);
                    komut.Parameters.AddWithValue("@K_Adi", textBox1.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Şifre Yenileme İşleminiz Başarılı . E-Postanızı kontrol ediniz.");
                }

                else
                {
                    MessageBox.Show("Bilgilerin Yanlış");
                }

                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.outlook.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential(YenilemeMaili, YenilemeSifresi);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(YenilemeMaili, "Şifre Sıfırlama");
                mail.To.Add(textBox2.Text);
                mail.Subject = "Şifre Sıfırlama İsteği";
                mail.IsBodyHtml = true;
                mail.Body = $" Sistemimiz tarafından oluşturulan yeni şifreniz : {yeniSifre} ";

                sc.Send(mail);
            }

            else {

                MessageBox.Show("Girilen kod yanlış.Lütfen tekrar deneyiniz");
                button2.Visible = true;
                label3.Visible = false;
        }


            }

        private void button2_Click(object sender, EventArgs e)
        {

            Random random = new Random();
            int kod = random.Next(0000, 9999);


            button2.Visible = false;

            label3.Text =  kod.ToString("D4");
            label3.Visible = true;
        }
    }
}