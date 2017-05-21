using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moja_Ksiegowosc
{
    public partial class Form2 : Form
    {
        #region Zmienne

        public string a, path, sql;
        private string folder;
        List<object> list = new List<object>();
        private bool y;
        private string filePath;
        private string[] identyfikator;
        public bool rozpoczecie;

        #endregion


        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Stella";
            textBox2.Text = "Piotr";
            textBox3.Text = "piotr123";
            if (checkBox1.Checked == true)
            {
                comboBox1.Visible = true;
                button1.Visible = true;

            }
            else
            {
                comboBox1.Visible = false;
                button1.Visible = false;
            }
            if (rozpoczecie == true)
            {
                button3.Enabled = false;

            }
            else
            {
                button3.Enabled= true;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
            Form1 form1 = new Form1();
            form1.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox1.Visible = true;
                button1.Visible = true;
                textBox1.Visible = false;
                comboBox2.Visible = true;

            }
            else
            {
                comboBox1.Visible = false;
                button1.Visible = false;
                textBox1.Visible = true;
                comboBox2.Visible = false;
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            SQLiteConnection con = new SQLiteConnection("Data Source='" + openFileDialog1.FileName + "';Version=3;");
            try
            {
                con.Open();
                sql = "Select * from Uzytkownicy";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!comboBox2.Items.Contains(reader["Nazwa"]))
                    {
                        comboBox2.Items.Add(reader["Nazwa"]);
                        comboBox2.ValueMember = reader["Nazwa"].ToString();
                        list.Add(reader["Nazwa"]);


                    }

                }
                reader.Close();
                comboBox2.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                con.Close();
            }
            comboBox1.Items.Add(openFileDialog1.FileName);
            int i;
            i = comboBox1.FindStringExact(openFileDialog1.FileName);
            comboBox1.SelectedIndex = i;
        }
        public static void zmiana_ikony(string path, string iconPath, string folderToolTip)
        {
            /* Remove any existing desktop.ini */
            if (File.Exists(path + @"desktop.ini")) File.Delete(path + @"desktop.ini");

            /* Write the desktop.ini */
            StreamWriter sw = File.CreateText(path + @"desktop.ini");
            sw.WriteLine("[.ShellClassInfo]");
            sw.WriteLine("InfoTip=" + folderToolTip);
            sw.WriteLine("IconFile=" + iconPath);
            sw.WriteLine("IconIndex=0");
            sw.Close();
            sw.Dispose();

            /* Set the desktop.ini to be hidden */
            File.SetAttributes(path + @"desktop.ini", File.GetAttributes(path + @"desktop.ini") | FileAttributes.Hidden);

            /* Set the path to system */
            File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.System);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            folder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum");
            SQLiteConnection con;
            filePath = @"c:\users\empla\documents\visual studio 2013\Projects\Moja_Ksiegowosc\Moja_Ksiegowosc\Resources\SQLite_icon.ico";

            Directory.CreateDirectory(folder);

            zmiana_ikony(folder + @"\", filePath, "Archiwum");
            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);

            }

            if (checkBox1.Checked == false)
            {
                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Podaj Identyfikator firmy");
                    textBox1.Focus();
                    textBox1.BackColor = Color.Yellow;
                    return;
                }
                identyfikator = textBox1.Text.Split();
               
                    if (identyfikator.Count()> 1)
                    {
                        MessageBox.Show("Musisz podać identyfikator firmy bez znaku spacji");
                        textBox1.Focus();
                        textBox1.Text = string.Empty;
                        textBox1.BackColor = Color.Yellow;
                        return;
                    }

                



                if (textBox2.Text == string.Empty)
                {
                    MessageBox.Show("Podaj nazwę użytkownika");
                    textBox2.Focus();
                    textBox2.BackColor = Color.Yellow;
                    return;
                }
                if (textBox3.Text == string.Empty)
                {
                    MessageBox.Show("Podaj hasło");
                    textBox3.Focus();
                    textBox3.BackColor = Color.Yellow;
                    return;
                }

               
                con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Create table if not exists Uzytkownicy (Nazwa varchar(20),Uzytkownik varchar(20),Haslo varchar(20),Sciezka varchar (20))";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Select * from Uzytkownicy where Nazwa='" + textBox1.Text + "'";
                    cmd = new SQLiteCommand(sql, con);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (textBox1.Text == reader["Nazwa"].ToString())
                        {

                            textBox1.Text = string.Empty;
                            textBox1.Focus();
                            textBox1.BackColor = Color.Yellow;
                            MessageBox.Show("Podałeś istniejący identyfikator firmy podaj inny");
                            reader.Close();
                            return;
                        }


                    }
                    sql = "Insert into Uzytkownicy (Nazwa,Uzytkownik,Haslo,Sciezka) Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + path + "')";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Create table if not exists Zalogowany (Nazwa varchar(20),Uzytkownik varchar(20),Haslo varchar(20))";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Delete from Zalogowany";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into Zalogowany(Nazwa ,Uzytkownik,Haslo) Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
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
                Form3 form3 = new Form3();
                rozpoczecie = true;
                form3.rozpoczecie = rozpoczecie;
                form3.ShowDialog();
                this.Close();


            }
            else
            {
                if (textBox2.Text == string.Empty)
                {
                    MessageBox.Show("Podaj Nazwe użytkownika");
                    textBox2.Focus();
                    textBox2.BackColor = Color.Yellow;
                    return;
                }
                if (textBox3.Text == string.Empty)
                {
                    MessageBox.Show("Podaj Hasło");
                    textBox3.Focus();
                    textBox3.BackColor = Color.Yellow;
                    return;
                }
                if (!System.IO.File.Exists(path))
                {
                    SQLiteConnection.CreateFile(path);

                }
                con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Create table if not exists Uzytkownicy (Nazwa varchar(20),Uzytkownik varchar(20),Haslo varchar(20),Sciezka varchar (20))";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Create table if not exists Zalogowany (Nazwa varchar(20),Uzytkownik varchar(20),Haslo varchar(20))";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Select * from Uzytkownicy where Nazwa ='" + comboBox2.SelectedValue + "'";
                    cmd = new SQLiteCommand(sql, con);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            MessageBox.Show("Wybrałeś firme o takim samym identyfikatorze podaj inny identyfikator");
                            return;
                        }

                    }

                    sql = "Delete from Zalogowany";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into Zalogowany(Nazwa ,Uzytkownik,Haslo) Values ('" + comboBox2.SelectedValue + "','" + textBox2.Text + "','" + textBox3.Text + "')";
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



                con = new SQLiteConnection("Data Source='" + openFileDialog1.FileName + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Select * from Uzytkownicy where Nazwa='" + comboBox2.SelectedValue + "'and Uzytkownik='" + textBox2.Text + "'and Haslo='" + textBox3.Text + "'";
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
                if (y == false)
                {
                    MessageBox.Show("Podałeś nieprawidłowe dane logowanie");
                    textBox2.Text = string.Empty;
                    textBox3.Text = string.Empty;
                    textBox2.Focus();
                    textBox2.BackColor = Color.Yellow;
                    return;
                }
                else
                {
                    con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
                    try
                    {
                        con.Open();
                        sql = "Insert into Uzytkownicy(Nazwa,Uzytkownik,Haslo,Sciezka) values ('" + comboBox2.SelectedValue + "','" + textBox2.Text + "','" + textBox3.Text + "','" + openFileDialog1.FileName + "')";
                        SQLiteCommand cmd = new SQLiteCommand(sql, con);
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


               


                    Form5 form5 = new Form5();
                    form5.Show();
                    Close();
                }

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.BackColor = Color.White;
        }
    }
}
