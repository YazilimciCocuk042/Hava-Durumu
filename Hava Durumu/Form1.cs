using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Http;

namespace Hava_Durumu
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void islem1()
        {
            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("Lütfen ilk önce bir konum gir!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Form1 frm = new Form1();
                string htmlCode = "";
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    htmlCode = client.DownloadString($"http://api.openweathermap.org/data/2.5/weather?q={guna2TextBox1.Text}&appid=c69926e5a99b43afe8808e2bac6a6be8&lang=tr");
                }
                dynamic stuff = JObject.Parse(htmlCode);

                dynamic sicaklik = stuff.main.temp;
                int veriSicaklik = (sicaklik - 273);
                L1.Text = veriSicaklik.ToString() + " °C";
                L1.Location = new Point(frm.Size.Width / 2 - L1.Size.Width / 2 - 10, 113);

                dynamic picture = stuff.weather[0].icon;
                guna2PictureBox1.Load($"http://openweathermap.org/img/w/{picture}.png");

                dynamic country = stuff.sys.country;
                dynamic name = stuff.name;
                L3.Text = $"{name}, {country}";
                L3.Location = new Point(frm.Size.Width / 2 - L3.Size.Width / 2 - 10, 310);

                dynamic status = stuff.weather[0].description;
                L2.Text = status;
                L2.Location = new Point(frm.Size.Width / 2 - L2.Size.Width / 2 - 10, 168);

                guna2TextBox1.Text = "";
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Thread islem = new Thread(() => islem1());
            islem.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            L1.Text = "";
            L2.Text = "";
            L3.Text = "";
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/yazilimci.cocuk042/");
        }
    }
}
