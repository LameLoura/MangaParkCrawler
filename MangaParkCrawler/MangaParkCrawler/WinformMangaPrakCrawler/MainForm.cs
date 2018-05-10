using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace WinformMangaPrakCrawler
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //WebRequest req = HttpWebRequest.Create("http://google.com");
            //req.Method = "GET";

            //string source;
            //using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            //{
            //    source = reader.ReadToEnd();
            //}

            //Console.WriteLine("http://mangapark.me/manga/shishunki-na-adam");

            using (WebClient client = new WebClient())
            {
                //client.QueryString.Add("p", "1500"); //add parameters
                string htmlCode = client.DownloadString("http://mangapark.me/manga/ganbare-nakamura-kun");
                //...
                string a = "";
            }

        }
    }
}
