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
    public partial class Form7 : Form
    {
        private string path;
        private string sql;
        private string zalogowany;
        private string nazwa_bazy;
        List<object> list = new List<object>();


        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
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

                MessageBox.Show("Nie zaostała utworzona żadna firma");
                this.Visible = false;
                Form1 form1 = new Form1();
                form1.ShowDialog();
                this.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            DialogResult result = MessageBox.Show("Uwaga usuwasz Dziłalność z rejestru", "Usuwanie działalności", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                this.Visible = false;
                form1.ShowDialog();
                this.Close();
            }
            else
            {
                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Delete from Uzytkownicy where Nazwa='" + comboBox1.SelectedValue + "'";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Delete from Zalogowany ";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    if (comboBox1.SelectedValue != null)
                    {
                        zalogowany = comboBox1.SelectedValue.ToString();
                        nazwa_bazy = "Dane_Firmy_" + zalogowany + "";
                        sql = "Drop table if exists '" + nazwa_bazy + "'";
                        cmd = new SQLiteCommand(sql, con);
                        cmd.ExecuteNonQuery();
                        
                    }
                    else
                    {
                                              
                        MessageBox.Show("Brak Firmy do usunięcia");
                        this.Visible = false;
                        form1.ShowDialog();
                        this.Close();
                        return;
                                   
                       
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
                MessageBox.Show("Usunełeś działalność");
                this.Visible = false;
                form1.ShowDialog();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form1 form1 = new Form1();
            form1.Visible=true;
            this.Close();
        }
    }
}
