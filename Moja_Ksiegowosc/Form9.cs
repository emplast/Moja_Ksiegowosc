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
    public partial class Form9 : Form
    {
        private string path;
        private bool y;
        private string sql;
        public Form9()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Podaj Nazwę  nowego użytkownika");
                textBox2.Focus();
                textBox2.BackColor = Color.Yellow;
            }
            if (textBox3.Text == string.Empty)
            {
                MessageBox.Show("Podaj Hasło nowego użytkownika");
                textBox3.Focus();
                textBox3.BackColor = Color.Yellow;
            }
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");

            SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Select* from Uzytkownicy where Nazwa ='" + textBox1.Text + "' ";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["Nazwa"].ToString() == textBox1.Text && reader["Uzytkownik"].ToString() == textBox2.Text && reader["Haslo"].ToString() == textBox3.Text)
                    {
                        MessageBox.Show("Podałeś te same dane");
                        textBox2.Text = string.Empty;
                        textBox3.Text = string.Empty;
                        textBox2.Focus();
                        textBox2.BackColor = Color.Yellow;
                        y = true;


                    }
                    else
                    {
                        y = false;
                    }
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
            if (y == false)
            {
                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");

                con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Insert into Uzytkownicy(Nazwa,Uzytkownik,Haslo,Sciezka)Values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Delete from Zalogowany";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into Zalogowany (Nazwa,Uzytkownik,Haslo) Values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.BackColor = Color.White;
        }

        private void Form9_Load(object sender, EventArgs e)
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
                    textBox1.Text = reader["Nazwa"].ToString();

                }
                reader.Close();
                sql = "Select* from Uzytkownicy where Nazwa ='" + textBox1.Text + "'";
                cmd = new SQLiteCommand(sql, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox4.Text = reader["Sciezka"].ToString();
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
    }
}
