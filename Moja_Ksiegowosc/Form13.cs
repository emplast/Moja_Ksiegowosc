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
    public partial class Form13 : Form
    {
        private string sql;
        List<string> tabele = new List<string>();
        private string path;
        private string rok;
        private string zalogowany;




        public Form13()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
                numericUpDown1.Value = 12;
            else if (numericUpDown1.Value == 13)
                numericUpDown1.Value = 1;
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

                    sql = "Delete from rok_miesiac_ksiegowy_"+zalogowany+"";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into rok_miesiac_ksiegowy_"+zalogowany+" (miesiac,rok) values('" + numericUpDown1.Value + "','" + rok + "')";
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

        private void Form13_Load(object sender, EventArgs e)
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
                        numericUpDown1.Value = DateTime.Now.Month;


                    }
                    else
                    {
                        
                        sql = "Select * from rok_miesiac_ksiegowy_"+zalogowany+"";
                        cmd = new SQLiteCommand(sql, con);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {

                            numericUpDown1.Value = Convert.ToDecimal(reader["miesiac"]);
                            rok = reader["rok"].ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
