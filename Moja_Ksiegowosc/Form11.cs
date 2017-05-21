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
using System.Windows.Forms;
using System.Xml.XPath;

namespace Moja_Ksiegowosc
{
    public partial class Form11 : Form
    {
        private bool polaczenie;
        private void aktualizacja_kursów()
        {
            List<string> files = new List<string>();

            WebClient wc = new WebClient();
            wc.DownloadFile("http://www.nbp.pl/Kursy/xml/dir.txt", "dir.txt");

            StreamReader sr = new StreamReader("dir.txt");
            string line = "";

            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    if (line.StartsWith("a"))
                    {
                        files.Add(line);
                    }
                }
            }

            sr.Close();

            try
            {
                XPathDocument document = new XPathDocument("http://www.nbp.pl/kursy/xml/" + files[files.Count - 1] + ".xml");
                XPathNavigator navigator = document.CreateNavigator();
                XPathNodeIterator iterator;
                iterator = navigator.Select("tabela_kursow");

                foreach (XPathNavigator nav in iterator)
                {
                    label12.Text = nav.SelectSingleNode("data_publikacji").Value;
                    //dataToolStripMenuItem.Text = "Data kursu: " + dataLabel.Text;
                }

                iterator = navigator.Select("tabela_kursow/pozycja");

                foreach (XPathNavigator nav in iterator)
                {
                    if (nav.SelectSingleNode("kod_waluty").Value == "USD" || nav.SelectSingleNode("kod_waluty").Value == "EUR" || nav.SelectSingleNode("kod_waluty").Value == "GBP" || nav.SelectSingleNode("kod_waluty").Value == "CHF")
                    {
                        if (nav.SelectSingleNode("kod_waluty").Value == "EUR")
                        {
                            label4.Text = nav.SelectSingleNode("kurs_sredni").Value + " PLN";
                            //euroToolStripMenuItem.Text = "Euro: " + kursEuro.Text;
                        }
                        else if (nav.SelectSingleNode("kod_waluty").Value == "USD")
                        {
                            label6.Text = nav.SelectSingleNode("kurs_sredni").Value + " PLN";
                            //dolarToolStripMenuItem.Text = "Dolar: " + kursDolar.Text;
                        }
                        else if(nav.SelectSingleNode("kod_waluty").Value == "GBP")
                        {
                            label8.Text = nav.SelectSingleNode("kurs_sredni").Value + " PLN";
                            //funtToolStripMenuItem.Text = "Funt: " + kursFunt.Text;
                        }
                        else if (nav.SelectSingleNode("kod_waluty").Value == "CHF")
                        {
                            label10.Text = nav.SelectSingleNode("kurs_sredni").Value + " PLN";
                        }
                    }
                }
            }
            catch (XPathException ex)
            {
                MessageBox.Show("Błąd przy pobieraniu kursów walut"+ex.ToString());
            }
        }
        public Form11()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void Form11_Load(object sender, EventArgs e)
        {
            polaczenie = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (polaczenie == true)
                aktualizacja_kursów();
            else
                MessageBox.Show("Brak połączenia z internetem powoduje brak możliwości pobrania kursów walut");
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            polaczenie = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (polaczenie == true)
                aktualizacja_kursów();
            else
                MessageBox.Show("Brak połączenia z internetem powoduje brak możliwości pobrania kursów walut");
        }
    }
}
