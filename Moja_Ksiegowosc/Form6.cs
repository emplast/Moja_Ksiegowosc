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
    public partial class Form6 : Form
    {
        private string sql, path, a;
        List<object> list = new List<object>();
        private bool y;

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

            
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            
            
            
            
            if (System.IO.File.Exists(path))
            {


                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Select * from Uzytkownicy";
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

                    }
                    reader.Close();
                    comboBox1.DataSource = list;
                    sql = "Select * from Zalogowany";
                    cmd = new SQLiteCommand(sql, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        
                                
                          a = reader["Nazwa"].ToString();
                          textBox1.Text = reader["Uzytkownik"].ToString();
                          textBox2.Text = reader["Haslo"].ToString();
                                
                         

                       
                    }
                    reader.Close();
                    int i;
                    i = comboBox1.FindStringExact(a);
                    comboBox1.SelectedIndex = i;
                    if (a == null)
                    {
                        DialogResult result = new DialogResult();
                        result = MessageBox.Show("Nie została utworzona baza działalności utwórz ją z menu 'Nowa'\r Jeśli chcesz przejść do innego menu wciśnij 'OK'", "Informacja", MessageBoxButtons.OKCancel);
                        if (result == DialogResult.OK)
                        {
                            this.Close();
                            Form2 form2 = new Form2();
                            form2.ShowDialog();
                        }
                        else
                        {
                            this.Close();
                            Form1 form1 = new Form1();
                            form1.Visible = true;
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
            }
            else
            {

                DialogResult result;
                result = MessageBox.Show("Nie została utworzona baza działalności utwórz ją z menu 'Nowa'\r Jeśli chcesz przejść do innego menu wciśnij 'OK'", "Informacja", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    this.Visible = false;
                    Form2 form2 = new Form2();
                    form2.ShowDialog();
                    this.Close();
                }
                else
                {
                    Close();
                }
                if (result == DialogResult.Cancel)
                {
                    Close();
                    Form1 form1 = new Form1();
                    form1.ShowDialog();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");



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
            if (y != true)
            {
                MessageBox.Show("Podałeś złe dane logowania");
                textBox1.Text = string.Empty;
                textBox1.BackColor = Color.Yellow;
                textBox2.Focus();
                textBox2.Text = string.Empty;

            }
            else
            {
                con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Delete from Zalogowany";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into Zalogowany (Nazwa,Uzytkownik,Haslo) Values ('" + comboBox1.SelectedValue + "','" + textBox1.Text + "','" + textBox2.Text + "')";
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
                this.Visible = false;
                Form5 form5 = new Form5();

                form5.ShowDialog();


                Form6 form6 = new Form6();
                form6.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form1 form1 = new Form1();
            form1.Visible=true;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
        }
    }
}
