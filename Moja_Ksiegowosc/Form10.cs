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
    public partial class Form10 : Form
    {
        private string path;
        private string sql;
        List<object> list = new List<object>();
        List<object> list_1 = new List<object>();
        private string path_1;
        private int i;
        private bool y;
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum/Archiwum.sqlite");

            SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Select* from Uzytkownicy";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!comboBox1.Items.Contains(reader["Nazwa"]))
                    {
                        comboBox1.Items.Add(reader["Nazwa"]);
                        comboBox1.ValueMember = reader["Nazwa"].ToString();
                        list.Add(reader["Nazwa"]);
                    }
                    if (!comboBox2.Items.Contains(reader["Sciezka"]))
                    {
                        comboBox2.Items.Add(reader["Sciezka"]);
                        comboBox2.ValueMember = reader["Sciezka"].ToString();
                        list_1.Add(reader["Sciezka"]);

                    }
                    path_1 = reader["Sciezka"].ToString();
                }
                reader.Close();
                comboBox1.DataSource = list;
                comboBox2.DataSource = list_1;

                i = comboBox2.FindStringExact(path_1);
                comboBox2.SelectedIndex = i;
                sql = "Select *from Zalogowany";
                cmd = new SQLiteCommand(sql, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    i = comboBox1.FindStringExact(reader["Nazwa"].ToString());
                    comboBox1.SelectedIndex = i;
                    textBox1.Text = reader["Uzytkownik"].ToString();
                    textBox2.Text = reader["Haslo"].ToString();



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

        private void button1_Click(object sender, EventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum/Archiwum.sqlite");

            SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Select * from Uzytkownicy where Nazwa='" + comboBox1.SelectedValue + "'and Uzytkownik='" + textBox1.Text + "'and Haslo='" + textBox2.Text + "'";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        y = true;
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
            if (y != true)
            {
                MessageBox.Show("Podałeś złe dane logowania");
                textBox1.Text = string.Empty;
                textBox1.BackColor = Color.Yellow;
                textBox1.Focus();
                textBox2.Text = string.Empty;
            }
            else
            {
                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum/Archiwum.sqlite");

                con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Delete from Zalogowany";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into Zalogowany (Nazwa,Uzytkownik,Haslo)Values('" + comboBox1.SelectedValue + "','" + textBox1.Text + "','" + textBox2.Text + "')";
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
