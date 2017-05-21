using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moja_Ksiegowosc
{
    public partial class Form3 : Form
    {
        public string a, path, sql;
        private int count_1;
        private bool b;
        private bool polaczenie;
        List<string> tabele = new List<string>();
        private string zalogowany;
        private int year;
        public bool rozpoczecie;
        
        




        public Form3()
        {
            InitializeComponent();
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
            #region Ustawienia Okna

            numericUpDown1.Maximum = DateTime.Now.Year;
            numericUpDown1.Minimum = DateTime.Now.Year - 1;


            if (numericUpDown1.Value == 2016)
            {
                this.Size = new System.Drawing.Size(500, 500);
                textBox1.Visible = true;
                textBox1.Visible = false;
                textBox10.Visible = false;
                textBox11.Visible = false;
                textBox12.Visible = false;
                textBox13.Visible = false;
                textBox14.Visible = false;
                textBox15.Visible = false;
                textBox16.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                groupBox2.Size = new Size(460, 110);
                groupBox2.Location = new Point(12, 75);
                groupBox3.Location = new Point(12, 200);
                button1.Location = new Point(40, 380);
                button2.Location = new Point(175, 380);
                button3.Location = new Point(340, 380);
            }
            else if (numericUpDown1.Value == 2017)
            {
                this.Size = new System.Drawing.Size(500, 560);
                textBox1.Visible = true;
                textBox10.Visible = true;
                textBox11.Visible = true;
                textBox12.Visible = true;
                textBox13.Visible = true;
                textBox14.Visible = true;
                textBox15.Visible = true;
                textBox16.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                groupBox2.Size = new Size(460, 170);
                groupBox2.Location = new Point(12, 75);
                groupBox3.Location = new Point(12, 260);
                button1.Location = new Point(40, 440);
                button2.Location = new Point(175, 440);
                button3.Location = new Point(340, 440);
            }

            if (rozpoczecie == true)
            {
                button3.Enabled = false;

            }
            else
            {
                button3.Enabled = true;
            }
            Form2 form2 = new Form2();
            form2.rozpoczecie = rozpoczecie;




            #endregion

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
                            numericUpDown1.Value = DateTime.Now.Year-1;
                            numericUpDown2.Value = DateTime.Now.Month;
                            
                        }
                        else
                        {
                            sql = "Select * from rok_miesiac_ksiegowy_"+zalogowany+"";
                            cmd = new SQLiteCommand(sql, con);
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                numericUpDown2.Value = Convert.ToDecimal(reader["miesiac"]);
                                numericUpDown1.Value = Convert.ToDecimal(reader["rok"]);
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
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Stawki_podatkowe.sqlite");
            if (System.IO.File.Exists(path))
            {


                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Stawki_podatkowe.sqlite");
                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Select* from podatek_dochodowy where Lp=1";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox2.Text = reader["Od"].ToString();
                        textBox3.Text = reader["Do"].ToString();
                        textBox4.Text = reader["Stawka"].ToString();
                        textBox5.Text = reader["Kwota_wolna_od_podatku"].ToString();
                    }
                    reader.Close();
                    sql = "Select * from podatek_dochodowy where Lp=2";
                    cmd = new SQLiteCommand(sql, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox6.Text = reader["Od"].ToString();
                        textBox7.Text = reader["Do"].ToString();
                        textBox8.Text = reader["Stawka"].ToString();
                        textBox9.Text = reader["Kwota_wolna_od_podatku"].ToString();

                    }
                    reader.Close();
                    sql = "Select * from stawki_ryczaltu";
                    cmd = new SQLiteCommand(sql, con);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);
                    cmd.ExecuteNonQuery();
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.Columns[0].Width = 30;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

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
                b = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3");
            try
            {
                con.Open();
                sql="Select* from Zalogowany ";
                SQLiteCommand cmd = new SQLiteCommand(sql,con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    zalogowany = reader["Nazwa"].ToString();
                }
                reader.Close();
                sql = "Create table if not exists rok_miesiac_ksiegowy_"+zalogowany+" (Lp INTEGER PRIMARY KEY,miesiac int,rok int)";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Delete from rok_miesiac_ksiegowy_"+zalogowany+"";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Insert into rok_miesiac_ksiegowy_"+zalogowany+" (miesiac , rok ) values('" + numericUpDown2.Value.ToString() + "','" + numericUpDown1.Value.ToString() + "')";
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

            if(Application.OpenForms["Form5"]==null)
            {
                this.Visible = false;
                Form4 form4 = new Form4();
                form4.rozpoczecie = rozpoczecie;
                form4.ShowDialog();
                this.Close();
            }
            else
            {
                Close();
            }
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            year = Convert.ToInt32(numericUpDown1.Value);

            polaczenie = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

            if (polaczenie == true)
            {



                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Stawki_podatkowe_" + numericUpDown1.Value + "_rok.sqlite");
                wsdl.Piotr_WS ws = new wsdl.Piotr_WS();
                count_1 = ws.wiersz_1(year);
                if (year == 2016)
                {


                    if (!System.IO.File.Exists(path))
                    {
                        
                        textBox2.Text = string.Empty;
                        textBox3.Text = string.Empty;
                        textBox4.Text = string.Empty;
                        textBox5.Text = string.Empty;
                        textBox6.Text = string.Empty;
                        textBox7.Text = string.Empty;
                        textBox8.Text = string.Empty;
                        textBox9.Text = string.Empty;
                        
                        dataGridView1.Columns.Clear();

                        toolStripProgressBar2.Value = 50;
                        toolStripLabel1.Text = "Status pobierania z serwera WWW";


                        textBox2.Text = ws._od(1, 1,year);
                        textBox3.Text = ws._od(1, 2, year);
                        textBox4.Text = ws._od(1, 3, year);
                        textBox5.Text = ws._od(1, 4, year);
                        textBox6.Text = ws._od(2, 1, year);
                        textBox7.Text = ws._od(2, 2, year);
                        textBox8.Text = ws._od(2, 3, year);
                        textBox9.Text = ws._od(2, 4, year);





                        dataGridView1.Columns.Add("Lp", "Lp");
                        dataGridView1.Columns.Add("Opis", "Opis");
                        dataGridView1.Columns.Add("Stawka", "Stawka");
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.Columns[0].Width = 30;
                        dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                        dataGridView1.RowCount = count_1;
                        int i = 0;
                        toolStripProgressBar2.Value = 100;
                        do
                        {


                            dataGridView1.Rows[0].Cells[i].Value = ws.ryczalt(1, i, year);
                            dataGridView1.Rows[1].Cells[i].Value = ws.ryczalt(2, i, year);
                            dataGridView1.Rows[2].Cells[i].Value = ws.ryczalt(3, i, year);
                            dataGridView1.Rows[3].Cells[i].Value = ws.ryczalt(4, i, year);
                            dataGridView1.Rows[4].Cells[i].Value = ws.ryczalt(5, i, year);
                            dataGridView1.Rows[5].Cells[i].Value = ws.ryczalt(6, i, year);
                            i++;
                        } while (i < 3);

                        SQLiteConnection.CreateFile(path);
                        SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                        try
                        {
                            con.Open();
                            sql = "Create table if not exists podatek_dochodowy (Lp INTEGER PRIMARY KEY , Od varchar(20),Do varchar(20),Stawka varchar(20),Kwota_wolna_od_podatku varchar(20))";
                            SQLiteCommand cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Create table if not exists stawki_ryczaltu (Lp INTEGER PRIMARY KEY ,Opis varchar(20),Stawka varchar(20))";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values('" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            for (i = 0; i < dataGridView1.RowCount; i++)
                            {
                                sql = "Insert into stawki_ryczaltu(Opis,Stawka) values('" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "')";
                                cmd = new SQLiteCommand(sql, con);
                                cmd.ExecuteNonQuery();
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
                        toolStripProgressBar2.Value = 100;
                        toolStripProgressBar1.Value = 0;
                    }
                    else
                    {


                        
                        textBox2.Text = string.Empty;
                        textBox3.Text = string.Empty;
                        textBox4.Text = string.Empty;
                        textBox5.Text = string.Empty;
                        textBox6.Text = string.Empty;
                        textBox7.Text = string.Empty;
                        textBox8.Text = string.Empty;
                        textBox9.Text = string.Empty;
                        dataGridView1.Columns.Clear();


                        toolStripProgressBar2.Value = 50;
                        toolStripLabel1.Text = "Status pobierania z serwera WWW";
                        textBox2.Text = ws._od(1, 1, year);
                        textBox3.Text = ws._od(1, 2, year);
                        textBox4.Text = ws._od(1, 3, year);
                        textBox5.Text = ws._od(1, 4, year);
                        textBox6.Text = ws._od(2, 1, year);
                        textBox7.Text = ws._od(2, 2, year);
                        textBox8.Text = ws._od(2, 3, year);
                        textBox9.Text = ws._od(2, 4, year);
                        dataGridView1.Columns.Add("Lp", "Lp");
                        dataGridView1.Columns.Add("Opis", "Opis");
                        dataGridView1.Columns.Add("Stawka", "Stawka");
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.Columns[0].Width = 30;
                        dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                        if (b == true)
                            dataGridView1.RowCount = count_1;

                        int i = 0;
                        toolStripProgressBar2.Value = 100;
                        do
                        {


                            dataGridView1.Rows[0].Cells[i].Value = ws.ryczalt(1, i, year);
                            dataGridView1.Rows[1].Cells[i].Value = ws.ryczalt(2, i, year);
                            dataGridView1.Rows[2].Cells[i].Value = ws.ryczalt(3, i, year);
                            dataGridView1.Rows[3].Cells[i].Value = ws.ryczalt(4, i, year);
                            dataGridView1.Rows[4].Cells[i].Value = ws.ryczalt(5, i, year);
                            dataGridView1.Rows[5].Cells[i].Value = ws.ryczalt(6, i, year);
                            i++;
                        } while (i < 3);


                        SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                        try
                        {
                            con.Open();
                            sql = "Create table if not exists podatek_dochodowy (Lp INTEGER PRIMARY KEY , Od varchar(20),Do varchar(20),Stawka varchar(20),Kwota_wolna_od_podatku varchar(20))";
                            SQLiteCommand cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Create table if not exists stawki_ryczaltu (Lp INTEGER PRIMARY KEY ,Opis varchar(20),Stawka varchar(20))";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Delete from podatek_dochodowy";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Delete from stawki_ryczaltu";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values('" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            for (i = 0; i < dataGridView1.RowCount; i++)
                            {
                                sql = "Insert into stawki_ryczaltu(Opis,Stawka) values('" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "')";
                                cmd = new SQLiteCommand(sql, con);
                                cmd.ExecuteNonQuery();
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
                        toolStripProgressBar1.Value = 100;
                        toolStripProgressBar1.Value = 0;
                    }


                }
                if (year == 2017)
                {


                    if (!System.IO.File.Exists(path))
                    {
                        textBox1.Text = string.Empty;
                        textBox2.Text = string.Empty;
                        textBox3.Text = string.Empty;
                        textBox4.Text = string.Empty;
                        textBox5.Text = string.Empty;
                        textBox6.Text = string.Empty;
                        textBox7.Text = string.Empty;
                        textBox8.Text = string.Empty;
                        textBox9.Text = string.Empty;
                        textBox10.Text = string.Empty;
                        textBox11.Text = string.Empty;
                        textBox12.Text = string.Empty;
                        textBox13.Text = string.Empty;
                        textBox14.Text = string.Empty;
                        textBox15.Text = string.Empty;
                        textBox16.Text = string.Empty;
                        textBox17.Text = string.Empty;
                        textBox18.Text = string.Empty;
                        textBox19.Text = string.Empty;
                        textBox20.Text = string.Empty;
                        dataGridView1.Columns.Clear();

                        toolStripProgressBar2.Value = 50;
                        toolStripLabel1.Text = "Status pobierania z serwera WWW";


                        textBox2.Text = ws._od(1, 1, year);
                        textBox3.Text = ws._od(1, 2, year);
                        textBox4.Text = ws._od(1, 3, year);
                        textBox5.Text = ws._od(1, 4, year);
                        textBox6.Text = ws._od(2, 1, year);
                        textBox7.Text = ws._od(2, 2, year);
                        textBox8.Text = ws._od(2, 3, year);
                        textBox9.Text = ws._od(2, 4, year);
                        textBox1.Text = ws._od(3, 1, year);
                        textBox10.Text = ws._od(3, 2, year);
                        textBox11.Text = ws._od(3, 3, year);
                        textBox12.Text = ws._od(3, 4, year);
                        textBox13.Text = ws._od(4, 1, year);
                        textBox14.Text = ws._od(4, 2, year);
                        textBox15.Text = ws._od(4, 3, year);
                        textBox16.Text = ws._od(4, 4, year);
                        textBox17.Text = ws._od(5, 1, year);
                        textBox18.Text = ws._od(5, 2, year);
                        textBox19.Text = ws._od(5, 3, year);
                        textBox20.Text = ws._od(5, 4, year);




                        dataGridView1.Columns.Add("Lp", "Lp");
                        dataGridView1.Columns.Add("Opis", "Opis");
                        dataGridView1.Columns.Add("Stawka", "Stawka");
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.Columns[0].Width = 30;
                        dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                        dataGridView1.RowCount = count_1;
                        int i = 0;
                        toolStripProgressBar2.Value = 100;
                        do
                        {


                            dataGridView1.Rows[0].Cells[i].Value = ws.ryczalt(1, i, year);
                            dataGridView1.Rows[1].Cells[i].Value = ws.ryczalt(2, i, year);
                            dataGridView1.Rows[2].Cells[i].Value = ws.ryczalt(3, i, year);
                            dataGridView1.Rows[3].Cells[i].Value = ws.ryczalt(4, i, year);
                            dataGridView1.Rows[4].Cells[i].Value = ws.ryczalt(5, i, year);
                            dataGridView1.Rows[5].Cells[i].Value = ws.ryczalt(6, i, year);
                            i++;
                        } while (i < 3);

                        SQLiteConnection.CreateFile(path);
                        SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                        try
                        {
                            con.Open();
                            sql = "Create table if not exists podatek_dochodowy (Lp INTEGER PRIMARY KEY , Od varchar(20),Do varchar(20),Stawka varchar(20),Kwota_wolna_od_podatku varchar(20))";
                            SQLiteCommand cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Create table if not exists stawki_ryczaltu (Lp INTEGER PRIMARY KEY ,Opis varchar(20),Stawka varchar(20))";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values('" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values ('" + textBox1.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + textBox12.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values ('" + textBox13.Text + "','" + textBox14.Text + "','" + textBox15.Text + "','" + textBox16.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values ('" + textBox17.Text + "','" + textBox18.Text + "','" + textBox19.Text + "','" + textBox20.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            for (i = 0; i < dataGridView1.RowCount; i++)
                            {
                                sql = "Insert into stawki_ryczaltu(Opis,Stawka) values('" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "')";
                                cmd = new SQLiteCommand(sql, con);
                                cmd.ExecuteNonQuery();
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
                        toolStripProgressBar2.Value = 100;
                        toolStripProgressBar1.Value = 0;
                    }
                    else
                    {


                        textBox1.Text = string.Empty;
                        textBox2.Text = string.Empty;
                        textBox3.Text = string.Empty;
                        textBox4.Text = string.Empty;
                        textBox5.Text = string.Empty;
                        textBox6.Text = string.Empty;
                        textBox7.Text = string.Empty;
                        textBox8.Text = string.Empty;
                        textBox9.Text = string.Empty;
                        textBox10.Text = string.Empty;
                        textBox11.Text = string.Empty;
                        textBox12.Text = string.Empty;
                        textBox13.Text = string.Empty;
                        textBox14.Text = string.Empty;
                        textBox15.Text = string.Empty;
                        textBox16.Text = string.Empty;
                        textBox17.Text = string.Empty;
                        textBox18.Text = string.Empty;
                        textBox19.Text = string.Empty;
                        textBox20.Text = string.Empty;
                        dataGridView1.Columns.Clear();


                        toolStripProgressBar2.Value = 50;
                        toolStripLabel1.Text = "Status pobierania z serwera WWW";
                        textBox2.Text = ws._od(1, 1, year);
                        textBox3.Text = ws._od(1, 2, year);
                        textBox4.Text = ws._od(1, 3, year);
                        textBox5.Text = ws._od(1, 4, year);
                        textBox6.Text = ws._od(2, 1, year);
                        textBox7.Text = ws._od(2, 2, year);
                        textBox8.Text = ws._od(2, 3, year);
                        textBox9.Text = ws._od(2, 4, year);
                        textBox1.Text = ws._od(3, 1, year);
                        textBox10.Text = ws._od(3, 2, year);
                        textBox11.Text = ws._od(3, 3, year);
                        textBox12.Text = ws._od(3, 4, year);
                        textBox13.Text = ws._od(4, 1, year);
                        textBox14.Text = ws._od(4, 2, year);
                        textBox15.Text = ws._od(4, 3, year);
                        textBox16.Text = ws._od(4, 4, year);
                        textBox17.Text = ws._od(5, 1, year);
                        textBox18.Text = ws._od(5, 2, year);
                        textBox19.Text = ws._od(5, 3, year);
                        textBox20.Text = ws._od(5, 4, year);






                        dataGridView1.Columns.Add("Lp", "Lp");
                        dataGridView1.Columns.Add("Opis", "Opis");
                        dataGridView1.Columns.Add("Stawka", "Stawka");
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.Columns[0].Width = 30;
                        dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                        if (b == true)
                            dataGridView1.RowCount = count_1;

                        int i = 0;
                        toolStripProgressBar2.Value = 100;
                        do
                        {


                            dataGridView1.Rows[0].Cells[i].Value = ws.ryczalt(1, i, year);
                            dataGridView1.Rows[1].Cells[i].Value = ws.ryczalt(2, i, year);
                            dataGridView1.Rows[2].Cells[i].Value = ws.ryczalt(3, i, year);
                            dataGridView1.Rows[3].Cells[i].Value = ws.ryczalt(4, i, year);
                            dataGridView1.Rows[4].Cells[i].Value = ws.ryczalt(5, i, year);
                            dataGridView1.Rows[5].Cells[i].Value = ws.ryczalt(6, i, year);
                            i++;
                        } while (i < 3);


                        SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                        try
                        {
                            con.Open();
                            sql = "Create table if not exists podatek_dochodowy (Lp INTEGER PRIMARY KEY , Od varchar(20),Do varchar(20),Stawka varchar(20),Kwota_wolna_od_podatku varchar(20))";
                            SQLiteCommand cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Create table if not exists stawki_ryczaltu (Lp INTEGER PRIMARY KEY ,Opis varchar(20),Stawka varchar(20))";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Delete from podatek_dochodowy";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Delete from stawki_ryczaltu";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values('" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values ('" + textBox1.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + textBox12.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values ('" + textBox13.Text + "','" + textBox14.Text + "','" + textBox15.Text + "','" + textBox16.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            sql = "Insert into podatek_dochodowy (Od,Do,Stawka,Kwota_wolna_od_podatku) values ('" + textBox17.Text + "','" + textBox18.Text + "','" + textBox19.Text + "','" + textBox20.Text + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            for (i = 0; i < dataGridView1.RowCount; i++)
                            {
                                sql = "Insert into stawki_ryczaltu(Opis,Stawka) values('" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "')";
                                cmd = new SQLiteCommand(sql, con);
                                cmd.ExecuteNonQuery();
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
                        toolStripProgressBar1.Value = 100;
                        toolStripProgressBar1.Value = 0;
                    }


                }
                
            }
            else
            {
                MessageBox.Show("Brak połączenia z siecią internet");
            }
            
            
   }
      

        private void button3_Click(object sender, EventArgs e)
        {
            //if (Application.OpenForms["Form5"] == null)
            //{
            //    Close();
            //    Form1 form1 = new Form1();
            //    form1.Visible = true;
            //}
            //else
            //{
            //    Close();
            //}
            if (rozpoczecie == true)
            {
                Form4 form4 = new Form4();
                form4.ShowDialog();
                Close();
            }
            else
            {
                Close();
            }

            
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value == 0)
                numericUpDown2.Value = 12;
            else if (numericUpDown2.Value == 13)
                numericUpDown2.Value = 1;
                
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 1999)
                numericUpDown1.Value = 2100;
            else if (numericUpDown1.Value == 2101)
                numericUpDown1.Value = 2000;
            if (numericUpDown1.Value == 2016)
            {
                this.Size = new System.Drawing.Size(500, 500);
                textBox1.Visible = true;
                textBox1.Visible = false;
                textBox10.Visible = false;
                textBox11.Visible = false;
                textBox12.Visible = false;
                textBox13.Visible = false;
                textBox14.Visible = false;
                textBox15.Visible = false;
                textBox16.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                groupBox2.Size = new Size(460, 115);
                groupBox2.Location = new Point(12, 75);
                groupBox3.Location = new Point(12, 200);
                button1.Location = new Point(40, 380);
                button2.Location = new Point(175, 380);
                button3.Location = new Point(340, 380);
            }
            else if (numericUpDown1.Value == 2017)
            {
                this.Size = new System.Drawing.Size(500, 560);
                textBox1.Visible = true;
                textBox10.Visible = true;
                textBox11.Visible = true;
                textBox12.Visible = true;
                textBox13.Visible = true;
                textBox14.Visible = true;
                textBox15.Visible = true;
                textBox16.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                groupBox2.Size = new Size(460, 170);
                groupBox2.Location = new Point(12, 75);
                groupBox3.Location = new Point(12, 260);
                button1.Location = new Point(40, 440);
                button2.Location = new Point(175, 440);
                button3.Location = new Point(340, 440);
            }
        }
    }
}
