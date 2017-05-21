using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Magazyn_podstawowy;


namespace Moja_Ksiegowosc
{
    public partial class Form5 : Form
    {
        #region Zmienne
        private int i;
        private string path;
        private string sql;
       
        #endregion

        public Form5()
        {
            InitializeComponent();
        }
        
        
        private void Form5_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
            toolStripStatusLabel3.Text = DateTime.Now.ToString();
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");

            SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Select* from Zalogowany";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    toolStripStatusLabel2.Text = reader["Nazwa"].ToString();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                con.Close();
            }



            
        }

        private void wyjścieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void zamknijWszystkieOknaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            for (i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "Form5" && Application.OpenForms[i].Name != "Form1")
                    Application.OpenForms[i].Close();
            }
            
            for (i = 5; i < oknaToolStripMenuItem.DropDownItems.Count; i++)
            {
                oknaToolStripMenuItem.DropDownItems.RemoveAt(i);
            }

        }

        private void oknaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (i = 5; i < oknaToolStripMenuItem.DropDownItems.Count; i++)
            {
                oknaToolStripMenuItem.DropDownItems.RemoveAt(i);
            }

            Form7 form7 = new Form7();
            Form6 form6 = new Form6();
            Form2 form2 = new Form2();
            Form3 form3 = new Form3();
            Form4 form4 = new Form4();
            i = 0;
            do
            {
                if (Application.OpenForms[i] != null && Application.OpenForms[i].Name != Application.OpenForms["Form1"].Name && Application.OpenForms[i].Text != form6.Text && Application.OpenForms[i].Text != form2.Text && Application.OpenForms[i].Text != form3.Text && Application.OpenForms[i].Text != form4.Text && Application.OpenForms[i].Text != form7.Text)
                    oknaToolStripMenuItem.DropDownItems.Add(Application.OpenForms[i].Text);
                i++;
            } while (i < Application.OpenForms.Count);

        }

        private void daneToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result;
            result = MessageBox.Show("Przed zmianą Danych Firmy musisz zamknąć wszystkie okna!!! \n Jeżeli chesz zamknąć wszystkie okna wcisnij OK \n Jeżeli nie wciśnij Anuluj", "Informacja", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {

                for (i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "Form5" && Application.OpenForms[i].Name != "Form1")
                        Application.OpenForms[i].Close();
                }

                for (i = 5; i < oknaToolStripMenuItem.DropDownItems.Count; i++)
                {
                    oknaToolStripMenuItem.DropDownItems.RemoveAt(i);
                }
                Form4 form4 = new Form4();
                form4.ShowDialog();

            }
            

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Przed zmianą roku i progów podatkowych musisz zamknąć wszystkie okna!!! \n Jeżeli chesz zamknąć wszystkie okna wcisnij OK \n Jeżeli nie wciśnij Anuluj", "Informacja", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {

                for (i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "Form5" && Application.OpenForms[i].Name != "Form1")
                        Application.OpenForms[i].Close();
                }

                for (i = 5; i < oknaToolStripMenuItem.DropDownItems.Count; i++)
                {
                    oknaToolStripMenuItem.DropDownItems.RemoveAt(i);
                }
                Form3 form3 = new Form3();
                form3.ShowDialog();
            }
        }

        private void Form5_Activated(object sender, EventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");

            SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Select* from Zalogowany";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    toolStripStatusLabel2.Text = reader["Nazwa"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                con.Close();
            }

            for (i = 5; i < oknaToolStripMenuItem.DropDownItems.Count; i++)
            {
                oknaToolStripMenuItem.DropDownItems.RemoveAt(i);
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToString();
        }

        private void użytkownikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Przed wylogowaniem i zmianą urzytkownika musisz zamknąć wszystkie okna!!!\nJeśli chesz zamknąć wszystkie okna wciśnij OK!!!\nJeśli nie wciśnij Anuluj", "Informacja", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                for (i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "Form5" && Application.OpenForms[i].Name != "Form1")
                        Application.OpenForms[i].Close();
                }

                for (i = 5; i < oknaToolStripMenuItem.DropDownItems.Count; i++)
                {
                    oknaToolStripMenuItem.DropDownItems.RemoveAt(i);
                }
                Form9 form9 = new Form9();
                form9.ShowDialog();
            }
            
            
            
        }

        private void firmęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Przed zmianą firmy musisz zamknąć wszystkie okna!!! \n Jeżeli chesz zamknąć wszystkie okna wcisnij OK \n Jeżeli nie wciśnij Anuluj", "Informacja", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {

                for (i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "Form5" && Application.OpenForms[i].Name != "Form1")
                        Application.OpenForms[i].Close();
                }

                for (i = 5; i < oknaToolStripMenuItem.DropDownItems.Count; i++)
                {
                    oknaToolStripMenuItem.DropDownItems.RemoveAt(i);
                }
                Form10 form10 = new Form10();
                form10.ShowDialog();
            }
        }

        private void kalkulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
           
        }

        private void tabelaKursówŚrednichNBPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11();
            if (Application.OpenForms["Form11"] == null)
            {
                
                form11.Show();
            }
            else
            {
                this.SendToBack();
                form11.BringToFront();
            }
            
        }

        private void wprowadzanieDokumentowMagazynowychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magazyn_podstawowy.Form1_mag mg_pods_form1 = new Magazyn_podstawowy.Form1_mag();
            if (Application.OpenForms["Form1_mag"] == null)
                mg_pods_form1.Show();
            else
                this.SendToBack();
            mg_pods_form1.BringToFront();
        }

        private void rokKsięgowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Przed zmianą roku księgowego musisz zamknąć wszystkie okna!!! \n Jeżeli chesz zamknąć wszystkie okna wcisnij OK \n Jeżeli nie wciśnij Anuluj", "Informacja", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {

                for (i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "Form5" && Application.OpenForms[i].Name != "Form1")
                        Application.OpenForms[i].Close();
                }

                for (i = 5; i < oknaToolStripMenuItem.DropDownItems.Count; i++)
                {
                    oknaToolStripMenuItem.DropDownItems.RemoveAt(i);
                }
                Form12 form12 = new Form12();
                form12.ShowDialog();
            }
        }

        private void miesiacKsięgowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Przed zmianą miesiąca księgowego musisz zamknąć wszystkie okna!!! \n Jeżeli chesz zamknąć wszystkie okna wcisnij OK \n Jeżeli nie wciśnij Anuluj", "Informacja", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {

                for (i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "Form5" && Application.OpenForms[i].Name != "Form1")
                        Application.OpenForms[i].Close();
                }

                for (i = 5; i < oknaToolStripMenuItem.DropDownItems.Count; i++)
                {
                    oknaToolStripMenuItem.DropDownItems.RemoveAt(i);
                }
                Form13 form13 = new Form13();
                form13.ShowDialog();
            }
        }

        private void magazynToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void domyślnyMagazynToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magazyn_podstawowy.Form4_mag form4_mag = new Form4_mag();
            if (Application.OpenForms["Form4_mag"] == null)
            {
                form4_mag.Show();
            }
            else
            {
                this.SendToBack();
                form4_mag.BringToFront();
            }
            
            
        }

        private void jednostkiMiaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7_mag form7 = new Form7_mag();
            form7.ShowDialog();
        }

        private void magazynyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4_mag form4 = new Form4_mag();
            form4.ShowDialog();
        }

        
    }
}
