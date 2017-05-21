using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moja_Ksiegowosc
{
    public partial class Form12 : Form
    {
        private string path;
        private string sql;
        List<string> tabele = new List<string>();
        private string miesiac;
        private string zalogowany;

        public Form12()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = DateTime.Now.Year;
            numericUpDown1.Minimum = DateTime.Now.Year - 1;
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            if (System.IO.File.Exists(path))
            {

                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3");
                try
                {
                    con.Open();
                    sql = "Select* from Zalogowany ";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        zalogowany = reader["Nazwa"].ToString();
                    }
                    reader.Close();
                    sql = "Pragma table_info(rok_miesiac_ksiegowy_"+zalogowany+")";
                    cmd = new SQLiteCommand(sql, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        tabele.Add(reader.ToString());
                    }
                    reader.Close();
                    if (tabele.Count == 0)
                    {
                        numericUpDown1.Value = DateTime.Now.Year;


                    }
                    else
                    {
                        sql = "Select * from rok_miesiac_ksiegowy_"+zalogowany+"";
                        cmd = new SQLiteCommand(sql, con);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {

                            numericUpDown1.Value = Convert.ToDecimal(reader["rok"]);
                            miesiac = reader["miesiac"].ToString();
                        }


                    }
                    reader.Close();



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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            if (System.IO.File.Exists(path))
            {

                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3");
                try
                {
                    con.Open();
                    sql = "Select* from Zalogowany ";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        zalogowany = reader["Nazwa"].ToString();
                    }
                    reader.Close();
                    sql = "Delete from rok_miesiac_ksiegowy_"+zalogowany+"";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into rok_miesiac_ksiegowy_"+zalogowany+" (miesiac,rok) values('"+miesiac+"','" + numericUpDown1.Value + "')";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                Close();
            }
        }
    }
}
