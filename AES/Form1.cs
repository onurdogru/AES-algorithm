using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        aesSifrecoz AES_Ogrendik = new aesSifrecoz(); //BU CLASS'IN tüm özelliklerini buraya taşımış oluruz.

        public class INIKaydet
        {

            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

            public INIKaydet(string dosyaYolu)
            {
                DOSYAYOLU = dosyaYolu;
            }
            private string DOSYAYOLU = String.Empty;
            public string Varsayilan { get; set; }
            public string Oku(string bolum, string ayaradi)
            {
                Varsayilan = Varsayilan ?? string.Empty;
                StringBuilder StrBuild = new StringBuilder(256);
                GetPrivateProfileString(bolum, ayaradi, Varsayilan, StrBuild, 255, DOSYAYOLU);
                return StrBuild.ToString();
            }
            public long Yaz(string bolum, string ayaradi, string deger)
            {
                return WritePrivateProfileString(bolum, ayaradi, deger, DOSYAYOLU);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            INIKaydet ini = new INIKaydet(Application.StartupPath + @"\Ayarlar.ini");
            ini.Yaz("Gizli Bilgi", "Gizli Metin Kutusu", richTextBox3.Text);
            ini.Yaz("Gizli CheckBox", "Gizli Bilgi", checkBox1.Checked.ToString());
            MessageBox.Show("Ayarlar kayıt altına alındı");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Application.StartupPath + @"\Ayarlar.ini"))
                {
                    INIKaydet ini = new INIKaydet(Application.StartupPath + @"\Ayarlar.ini");
                    richTextBox3.Text = ini.Oku("Gizli Bilgi", "Gizli Metin Kutusu"); // [Gizli Bilgi] Gizli Metin Kutus = "bıla bıla"
                    checkBox1.Checked = Convert.ToBoolean(ini.Oku("Gizli CheckBox", "Gizli Bilgi"));
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("ini dosyası hasarlı" + hata.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Application.StartupPath + @"\Ayarlar.ini"))
                {
                    INIKaydet ini = new INIKaydet(Application.StartupPath + @"\Ayarlar.ini");
                    richTextBox3.Text = ini.Oku("Gizli Bilgi", "Gizli Metin Kutusu"); // [Gizli Bilgi] Gizli Metin Kutus = "bıla bıla"
                    checkBox1.Checked = Convert.ToBoolean(ini.Oku("Gizli CheckBox", "Gizli Bilgi"));
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("ini dosyası hasarlı" + hata.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = AES_Ogrendik.AESsifrele(richTextBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(richTextBox2.Text == string.Empty)
            {
                MessageBox.Show("Çözülecek Şifre Bulunamadı");
            }
            else
            {
                richTextBox3.Text = AES_Ogrendik.AESsifre_Coz(richTextBox2.Text);
            }
            
        }
    }
}
