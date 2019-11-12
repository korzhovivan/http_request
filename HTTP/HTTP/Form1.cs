using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(GetHTML));
            thread.Start();
            
        }

        private void GetHTML()
        {
            HttpWebRequest reqw = (HttpWebRequest)HttpWebRequest.Create(textBox1.Text);
            HttpWebResponse resp = (HttpWebResponse)reqw.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);

            UpdateTextBox(sr.ReadToEnd());

            sr.Close();
        }
        private void UpdateTextBox(string str)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action<string>(UpdateTextBox), str);
            }
            else
            {
                textBox2.Text = str;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(GetHeaders));
            thread.Start();
           
        }

        private void GetHeaders()
        {
            HttpWebRequest reqw = (HttpWebRequest)HttpWebRequest.Create(textBox1.Text);
            HttpWebResponse resp = (HttpWebResponse)reqw.GetResponse();
            string str = "";
            foreach (string header in resp.Headers)
            {
                str += header + ":" + resp.Headers[header];
            }
            UpdateTextBox(str);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(GetPicture));
            thread.Start();
           
        }

        private void GetPicture()
        {
            var request = WebRequest.Create("https://i2.rozetka.ua/goods/4706196/koss_193582_images_4706196008.jpg");

            Image img = null;
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                img = Bitmap.FromStream(stream);
                SetPictuture(img);
            }

            //pictureBox1.ImageLocation = "https://i2.rozetka.ua/goods/4706196/koss_193582_images_4706196008.jpg";
        }
        private void SetPictuture(Image img)
        {
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new Action<Image>(SetPictuture), img);
            }
            else
            {
                pictureBox1.Image = img;
            }
        }
    }
}
