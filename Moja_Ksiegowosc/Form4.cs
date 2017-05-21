using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.IO;
using System.Data.SQLite;

namespace Moja_Ksiegowosc
{
    public partial class Form4 : Form
    {
        #region Zmienne

        public bool rozpoczecie;
        private float i;
        private float j;
        private string path;
        private string sql;
        private string zalogowany;
        private string nazwa_bazy;
        private string path_1;
        private string magazyn_domyslny;
        private string magazyn_opis;
        private string magazyn_typ;
        List<string> mag_domyslny = new List<string>();
        List<string> mag_opis = new List<string>();
        List<string> mag_typ = new List<string>();
        private string domyslna_jednostka;
        List<string> dom_jednostka = new List<string>();
        private bool d_o_1= false;
        private string opis;
        
        

        #endregion
        public Form4()
        {
            InitializeComponent();
            
        }


        private void button7_Click(object sender, EventArgs e)
        {

            #region Otrzeżenia o brakujących wpisach i sprawdzenie numerów NIP REGON


            if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Podaj Pełną Nazwę Firmy");
                textBox2.Focus();
                textBox2.BackColor = Color.Yellow;
                return;
            }
            if (textBox3.Text.Length != 10 && comboBox1.SelectedIndex==23)
            {
                    MessageBox.Show("Podałeś nieprawidłowej długości NIP");
                    textBox3.Focus();
                    textBox3.Text = string.Empty;
                    textBox3.BackColor = Color.Yellow;
                    return;
               
            }
            else
            {
                i = (textBox3.Text[0] * 6) + (textBox3.Text[1] * 5) + (textBox3.Text[2] * 7) + (textBox3.Text[3] * 2) + (textBox3.Text[4] * 3) + (textBox3.Text[5] * 4) + (textBox3.Text[6] * 5) + (textBox3.Text[7] * 6) + (textBox3.Text[8] * 7);
                j = i % 11;
                if (j == 10)
                {
                    MessageBox.Show("Podałeś nieprawidłowy Nr NIP");
                    textBox3.Focus();
                    textBox3.Text = string.Empty;
                    textBox3.BackColor = Color.Yellow;
                    return;
                    
                }
            }
            if (textBox4.Text.Length != 7 && textBox4.Text.Length!=14)
            {

                MessageBox.Show("Podałeś nieprawidłowej długości numer REGON");
                textBox4.Focus();
                textBox4.Text = string.Empty;
                textBox4.BackColor = Color.Yellow;
                return;
            }
            else
            {
                
            }
            if (comboBox2.SelectedIndex == 0)
            {
                MessageBox.Show("Wybierz Urząd Skarbowy");
                comboBox2.Focus();
                comboBox2.BackColor = Color.Yellow;
                return;
            }

#endregion


            #region Zapis do bazy danych 

            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");

            if (File.Exists(path))
            {
                
                try
                {
                    con.Open();
                    sql = "Select * from Zalogowany";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        zalogowany = reader["Nazwa"].ToString();
                    }
                    reader.Close();
                    nazwa_bazy = "Dane_Firmy_"+zalogowany+"" ;
                    sql = "Delete from '" + nazwa_bazy + "'";
                    cmd = new SQLiteCommand(sql,con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert Into '" + nazwa_bazy + "'(Nazwa_skrocona,Nazwa_pelna,NIP_panstwo,NIP,REGON,Nr_zezwolenia,Urzad_skarbowy,Nazwa_banku,NR_konta_eban,Nr_konta,Podatnik_na_vat,Podatnik_vat_imie,Podatnik_vat_nazwisko,Podatnik_vat_nr_telefonu,numeric_1,Podatnik_unijny,Rozliczenie_kwartalne_vat_I_kwartal,Rozliczenie_vat_II_kwartal,Rozliczenie_vat_III_kwartal,Rozliczenie_vat_IV_kwartal,Metoda_kasowa_I_kwartal,Metoda_kasowa_II_kwartal,Metoda_kasowa_III_kwartal,Metoda_kasowa_IV_kwartal,Z_rozrachunkow_I_kwartal,Z_rozrachunkow_II_kwartal,Z_rozrachunkow_III_kwartal,Z_rozrachunkow_IV_kwartal,Rozliczenie_kwartalne_vat_eu_I_kwartal,Rozliczenie_kwartalne_vat_eu_II_kwartal,Rozliczenie_kwartalne_vat_eu_III_kwartal,Rozliczenie_kwartalne_vat_eu_IV_kwartal,VAT_zwolnione_netto,VAT_procent_vat,PKPiR,Nowy_wzor_pkpir,Metoda_uproszczona,Ewidencja_przychodow,Ewidencja_przychodow_I_kwartal,Ewidencja_przychodow_II_kwartal,Ewidencja_przychodow_III_kwartal,Ewidencja_przychodow_IV_kwartal,Zaokranglaj,Rodzaj_prowadzonej_dzialalnosci,numeric_2,Kraj,Wojewodztwo,Powiat,Gmina,Ulica_dane_adresowe,Nr_domu_dane_adresowe,Nr_lokalu_dane_adresowe,Miejscowosc_dane_adresowe,Kod_I_dane_adresowe,Kod_II_dane_adresowe,Poczta_dane_adresowe,Tel_fax_dane_adresowe,E_mail_dane_adresowe,Miejsce_miejscowosc_dane_adresowe,Miejsce_ulica_dane_adresowe,Miejsce_nr_domu_dane_adresowe,Miejsce_nr_lokalu_dane_adresowe,Magazyn_domyslna_jednostka,numeric_3,numeric_4,numeric_5,Magazyn_wspolna_dla_calego_roku,Magazyn_odrebna_dla_kazdego_miesiaca,Magazyn_domyslny_magazyn,Magazyn_domyslny_opis,Magazyn_domyslny_typ,Magazyn_wspolna_numeracja_faktur,Magazyn_automatyczna_numeracja_indeksow,Magazyn_zaplata_zero,Magazyn_zapobiegaj_zmianom,Magazyn_ksieguj_w_tym_samym_programie,Magazyn_ksieguj_w_innym_programie,Magazyn_nie_ksieguj,Magazyn_rozrachunki_razem_z_dokumentem,Magazyn_rozrachunki_odrebne,Magazyn_nie_wprowadzaj_rozrachunkow,Magazyn_ksieguj_dokumenty_wedlug_regol,Magazyn_wprowadzaj_rozrachunki_automatycznie,Magazyn_ksieguj_w_dacie_wystawienia,Magazyn_drukuj_rachunek_zamiast_faktura,Magazyn_w_danych_kontrahenta_drukuj_miejsce,Magazyn_w_danych_kontrahenta_drukuj_nr_zezwolenia,Magazyn_w_danych_kontrahenta_drukuj_REGON,Magazyn_w_fakturze_drukuj_indeks,Magazyn_drukuj_informacje_na_fakturze_o_wz,Magazyn_na_wz_drukuj_cene,Magazyn_drukuj_podsumowanie_ilosci,Magazyn_na_fakturach_drukuj_nazwe_skrocona_kontrahenta,Magazyn_informacja_o_rabatach,Magazyn_drukuj_date_termin_zaplaty,Magazyn_na_fakturach_drukuj_poczta,Magazyn_na_fakturze_walutowej_informacja_o_wartosci_pln,Magazyn_drukuj_stopke,Magazyn_informacja_o_niezaplaconych_fakturach,Magazyn_informacja_do_zaplaty_pozostalo,numeric_6,numeric_7,Magazyn_rodzaj_drukowanej_faktury_orginal,Magazyn_rodzaj_drukowanej_faktury_kopia,Magazyn_rodzaj_drukowanej_faktury_orginal_kopia,Magazyn_rodzaj_drukowanej_faktury_nieokreslony,Magazyn_drukuj_kwote_slownie,Magazyn_drukuj_informacje_o_rabacie_na_zamowieniach,Magazyn_na_zamowieniach_drukuj_obok_siebie_ceny_brutto_netto,Magazyn_sprzedawaj_w_cenach_brutto,Magazyn_ustal_cene_sprzedazy_1_wedlug_narzutu,Magazyn_sprzedawaj_wedlug_ostatniej_ceny_sprzedazy_kontrahenta,Magazyn_cena_sprzedazy_1,Magazyn_cena_sprzedazy_2,Magazyn_cena_sprzedazy_3,Magazyn_cena_sprzedazy_4,Magazyn_wystawiaj_faktury_bez_podpisu,Magazyn_podpis_osoby_upowaznionej,Rozliczenia_numeracja_wspolna,Rozliczenia_numeracja_odrebna,numeric_8,Rozliczenia_tytul_wezwania,Rozliczenia_naglowek_wezwania,Rozliczenia_stopka_wezwania,Rozliczenia_tytul_nota,Rozliczenia_naglowek_nota,Rozliczenia_stopka_nota,Rozliczenia_podpis,Srodki_trwale,Zus_nazwa,Zus_NIP,Zus_REGON,Zus_PESEL,Zus_d_o,Zus_paszport,Zus_seria_numer,Zus_imie,Zus_nazwisko,Zus_rok_urodzenia,Zus_miesiac_urodzenia,Zus_dzien_urodzenia,Zus_koszty_nieograniczone,Zus_koszty_do_wysokosci_placy_brutto_pomniejszone_o_zus,Zus_koszty_do_wysokosci_placy_brutto) values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.SelectedIndex.ToString() + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + comboBox2.SelectedIndex.ToString() + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + checkBox1.Checked.ToString() + "','" + textBox9.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + numericUpDown1.Value.ToString() + "','" + checkBox2.Checked.ToString() + "','" + checkBox3.Checked.ToString() + "','" + checkBox4.Checked.ToString() + "','" + checkBox5.Checked.ToString() + "','" + checkBox6.Checked.ToString() + "','" + checkBox7.Checked.ToString() + "','" + checkBox8.Checked.ToString() + "','" + checkBox9.Checked.ToString() + "','" + checkBox10.Checked.ToString() + "','" + checkBox11.Checked.ToString() + "','" + checkBox12.Checked.ToString() + "','" + checkBox13.Checked.ToString() + "','" + checkBox14.Checked.ToString() + "','" + checkBox15.Checked.ToString() + "','" + checkBox16.Checked.ToString() + "','" + checkBox17.Checked.ToString() + "','" + checkBox18.Checked.ToString() + "','" + checkBox19.Checked.ToString() + "','" + checkBox20.Checked.ToString() + "','" + checkBox21.Checked.ToString() + "','" + checkBox22.Checked.ToString() + "','" + checkBox23.Checked.ToString() + "','" + checkBox24.Checked.ToString() + "','" + checkBox25.Checked.ToString() + "','" + checkBox26.Checked.ToString() + "','" + checkBox27.Checked.ToString() + "','" + checkBox28.Checked.ToString() + "','" + checkBox29.Checked.ToString() + "','" + textBox12.Text + "','" + numericUpDown2.Value.ToString() + "','" + textBox13.Text + "','" + textBox14.Text + "','" + textBox15.Text + "','" + textBox16.Text + "','" + textBox17.Text + "','" + textBox18.Text + "','" + textBox19.Text + "','" + textBox20.Text + "','" + textBox21.Text + "','" + textBox22.Text + "','" + textBox23.Text + "','" + textBox24.Text + "','" + textBox25.Text + "','" + textBox26.Text + "','" + textBox27.Text + "','" + textBox28.Text + "','" + textBox29.Text + "','"+textBox50.Text+"','" + numericUpDown3.Value.ToString() + "','" + numericUpDown4.Value.ToString() + "','" + numericUpDown5.Value.ToString() + "','" + checkBox30.Checked.ToString() + "','" + checkBox31.Checked.ToString() + "','"+textBox48.Text+"','"+textBox49.Text+"','"+comboBox3.SelectedItem+"','" + checkBox32.Checked.ToString() + "','" + checkBox33.Checked.ToString() + "','" + checkBox34.Checked.ToString() + "','" + checkBox35.Checked.ToString() + "','" + checkBox36.Checked.ToString() + "','" + checkBox37.Checked.ToString() + "','" + checkBox38.Checked.ToString() + "','" + checkBox39.Checked.ToString() + "','" + checkBox40.Checked.ToString() + "','" + checkBox41.Checked.ToString() + "','" + checkBox42.Checked.ToString() + "','" + checkBox43.Checked.ToString() + "','" + checkBox44.Checked.ToString() + "','" + checkBox45.Checked.ToString() + "','" + checkBox46.Checked.ToString() + "','" + checkBox47.Checked.ToString() + "','" + checkBox48.Checked.ToString() + "','" + checkBox49.Checked.ToString() + "','" + checkBox50.Checked.ToString() + "','" + checkBox51.Checked.ToString() + "','" + checkBox52.Checked.ToString() + "','" + checkBox53.Checked.ToString() + "','" + checkBox54.Checked.ToString() + "','" + checkBox55.Checked.ToString() + "','" + checkBox56.Checked.ToString() + "','" + checkBox57.Checked.ToString() + "','" + checkBox58.Checked.ToString() + "','" + checkBox59.Checked.ToString() + "','" + checkBox60.Checked.ToString() + "','" + numericUpDown6.Value.ToString() + "','" + numericUpDown7.Value.ToString() + "','" + checkBox61.Checked.ToString() + "','" + checkBox62.Checked.ToString() + "','" + checkBox63.Checked.ToString() + "','" + checkBox64.Checked.ToString() + "','" + checkBox65.Checked.ToString() + "','" + checkBox66.Checked.ToString() + "','" + checkBox67.Checked.ToString() + "','" + checkBox68.Checked.ToString() + "','" + checkBox69.Checked.ToString() + "','" + checkBox70.Checked.ToString() + "','" + textBox30.Text + "','" + textBox31.Text + "','" + textBox32.Text + "','" + textBox33.Text + "','" + checkBox71.Checked.ToString() +"','"+textBox34.Text+"','"+checkBox72.Checked.ToString()+"','"+checkBox73.Checked.ToString()+"','"+numericUpDown8.Value.ToString()+"','"+textBox35.Text+"','"+richTextBox1.Text+"','"+richTextBox2.Text+"','"+textBox36.Text+"','"+richTextBox3.Text+"','"+richTextBox4.Text+"','"+textBox37.Text+"','"+checkBox74.Checked.ToString()+"','"+textBox38.Text+"','"+textBox39.Text+"','"+textBox40.Text+"','"+textBox41.Text+"','"+checkBox75.Checked.ToString()+"','"+checkBox76.Checked.ToString()+"','"+textBox42.Text+"','"+textBox43.Text+"','"+textBox44.Text+"','"+textBox45.Text+"','"+textBox46.Text+"','"+textBox47.Text+"','"+checkBox77.Checked.ToString()+"','"+checkBox78.Checked.ToString()+"','"+checkBox79.Checked.ToString()+"')";
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
               
            }
            else
            {
                MessageBox.Show("Brak Bazy Danych");
            }



            #endregion

            #region Zakładanie magazynu


            
           
            path_1 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
            if (!System.IO.File.Exists(path_1))
            {
                SQLiteConnection.CreateFile(path_1);
            }
            
            con = new SQLiteConnection("Data Source='" + path_1 + "';Version=3;");
            try
            {
                con.Open();
                sql = "Create table if not exists Magazyny (Lp INTEGER PRIMARY KEY , Symbol varchar (20),Opis varchar(20),Typ varchar(20))";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Magazyn ( Symbol varchar (20),Opis varchar(20),Typ varchar(20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Jednostki_miary (Lp Integer Primary Key,Nazwa_jednostki varchar (20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                path=System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
                SQLiteConnection com = new SQLiteConnection("Data Source='"+path+"';Version=3;");
                try
                {
                    com.Open();
                    sql="Select * from Dane_Firmy_"+zalogowany+"";
                    SQLiteCommand cmd_1 = new SQLiteCommand(sql,com);
                    SQLiteDataReader reader = cmd_1.ExecuteReader();
                    while(reader.Read())
                    {
                        magazyn_domyslny=reader["Magazyn_domyslny_magazyn"].ToString();
                        magazyn_opis=reader["Magazyn_domyslny_opis"].ToString();
                        magazyn_typ = reader["Magazyn_domyslny_typ"].ToString();
                        
                    }
                    reader.Close();
                    

                }
                catch(Exception ex_1)
                {
                    MessageBox.Show(ex_1.ToString());
                }
                finally
                {
                    com.Close();
                }
               
                    sql = "Select* from Magazyny";
                    cmd = new SQLiteCommand(sql, con);
                    SQLiteDataReader reader_1 = cmd.ExecuteReader();
                    while (reader_1.Read())
                    {
                       
                            mag_domyslny.Add(reader_1["Symbol"].ToString());
                            mag_opis.Add(reader_1["Opis"].ToString());
                            mag_typ.Add(reader_1["Typ"].ToString());
                       
                        
                        
                    }
                    reader_1.Close();
                    sql = "Delete from Magazyny";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    
                       

                    for (int i = 0; i < mag_domyslny.Count; i++)
                    {
                        if (magazyn_domyslny != mag_domyslny[i])
                        {
                            sql = "Insert into Magazyny (Symbol,Opis,Typ) values('" + mag_domyslny[i] + "','" + mag_opis[i] + "','" + mag_typ[i] + "')";
                            cmd = new SQLiteCommand(sql, con);
                            cmd.ExecuteNonQuery();
                        }
                        
                        
                    }

                    sql = "Insert into Magazyny (Symbol,Opis,Typ) values ('" + magazyn_domyslny + "','" + magazyn_opis + "','" + magazyn_typ + "')";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();

                    sql = "Delete from Magazyn";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into Magazyn (Symbol,Opis,Typ) values('" +textBox48.Text + "','" + textBox49.Text + "','" + comboBox3.SelectedItem + "')";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();

                    sql = "Select * from Jednostki_miary where Nazwa_jednostki='"+textBox50.Text+"'";
                    cmd = new SQLiteCommand(sql, con);
                    reader_1 = cmd.ExecuteReader();
                    while (reader_1.Read())
                    {
                        if (reader_1.HasRows)
                        {
                            domyslna_jednostka = reader_1["Nazwa_jednostki"].ToString();
                            d_o_1 = true;
                        }

                    }
                    if (d_o_1 == false)
                    {
                        domyslna_jednostka = textBox50.Text;
                        sql = "Insert into Jednostki_miary (Nazwa_jednostki) values ('" + domyslna_jednostka + "')";
                        cmd = new SQLiteCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        sql = "Update Jednostki_miary set Nazwa_jednostki='"+domyslna_jednostka+"'where Nazwa_jednostki='"+domyslna_jednostka+"'";
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
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
            con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Create table if not exists Magazyn_kartoteki (Symbol_magazynu varchar(20),Indeks varchar(20)  ,Symbol_SWW varchar (20),Nazwa varchar(20),Jednostka_miary varchar(20),Typ varchar(20),Stawka_VAT varchar(20),Stan_magazynowy varchar (20),Minimalny_stan varchar (20),Okres_zbywania varchar (20),Rezerwacja varchar (20),Cena_I varchar (20),Cena_II varchar(20),Cena_III varchar (20),Cena_IV varchar (20),Cena_zakupu varchar (20),Marza varchar(20),JPG varchar(20))";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Edycja_kartoteki (Symbol_magazynu varchar(20),Indeks varchar(20)  ,Symbol_SWW varchar (20),Nazwa varchar(20),Jednostka_miary varchar(20),Typ varchar(20),Stawka_VAT varchar(20),Stan_magazynowy varchar (20),Minimalny_stan varchar (20),Okres_zbywania varchar (20),Rezerwacja varchar (20),Cena_I varchar (20),Cena_II varchar(20),Cena_III varchar (20),Cena_IV varchar (20),Cena_zakupu varchar (20),Marza varchar(20),JPG varchar(20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Magazyn_" + magazyn_domyslny + "(Symbol varchar (20),Nazwa varchar(20),Cena_sprzedazy varchar(20),Dostepny_stan varchar(20),Jednostka_miary varchar(20),Ilosc_zarezerwowana varchar(20),Stawka_VAT varchar(20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Magazyn_pozycje_dokumentow_magazynowych (Lp varchar(20) ,Magazyn varchar (20),Indeks varchar (20),Cena_sprzedaży varchar (20),Rabat varchar (20), Cena_z_rabatem varchar (20),Ilość varchar(20),Jednostka_miary varchar (20),Wartość varchar (20) ,Stawka_VAT varchar (20), Nazwa varchar (20), Numer_dokumentu_magazynowego varchar (20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Magazyn_dokumenty_magazynowe (Nr_dokumentu varchar (20),Data_dokumentu varchar (20), Kontrahent varchar (20),Typ varchar (20),Nr_faktury_paragonu varchar (20),Data_faktury_paragonu varchar (20), Wartość_faktury_paragonu varchar (20),Wartość_dokumentu varchar (20),Dokument_zaksięgowany varchar (20),Forma_płatności varchar (20),Forma_transportu varchar (20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Pozycja_dokumentu (Magazyn varchar (20),Indeks varchar (20),Nazwa varchar (20),Jednostka_miary varchar (20),Typ varchar (20),Stawka_VAT varchar (20),Cena_zakupu varchar (20),Marża varchar (20) ,JPG varchar(20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Pozycje_dokumentu (Lp Integer primary key,Magazyn varchar(20),Indeks varchar(20),Nazwa varchar(20),Ilość varchar (20),Jednostka_miary varchar (20), Cena_sprzedaży varchar (20),Rabat varchar (20),Cena_z_rabatem varchar (20),Wartość varchar (20),Stawka_VAT varchar (20),Cena_zakupu varchar (20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Jednostki_miary (Lp Integer primary key,Nazwa_jednostki varchar (20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Jednostka_miary (Nazwa_jednostki varchar (20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Edycja_dokumentu_magazynowego (Magazyn varchar(20),Indeks varchar(20),Nazwa varchar(20),Ilość varchar (20),Jednostka_miary varchar (20), Cena_sprzedaży varchar (20),Rabat varchar (20),Cena_z_rabatem varchar (20),Wartość varchar (20),Stawka_VAT varchar (20),Cena_zakupu varchar (20))";
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

            #endregion
            



            if (Application.OpenForms["Form5"] == null)
            {
                this.Visible = false;
                Form5 form5 = new Form5();
                form5.ShowDialog();
            }
            else
            {
                Close();

            }  

                
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            Form1 form1 = new Form1();
            if (Application.OpenForms["Form5"]==null)
            {
                Close();
                form1.Visible = true;
            }
            else
                Close();
           

        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked == true)
            {
                checkBox24.Checked = false;
                checkBox23.Enabled = true;
                
            }
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox24.Checked == false)
            {
                checkBox21.Checked = true;
                checkBox25.Enabled = false;
                checkBox26.Enabled = false;
                checkBox27.Enabled = false;
                checkBox28.Enabled = false;

            }
            else 
            {
                checkBox21.Checked = false;
                checkBox25.Enabled = true;
                checkBox26.Enabled = true;
                checkBox27.Enabled = true;
                checkBox28.Enabled = true;
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

            comboBox1.SelectedIndex = 23;
            comboBox2.SelectedIndex = 1;
            textBox2.Text = "Emplast Piotr Majewski";
            textBox3.Text = "9161001759";
            textBox4.Text = "1212121";
            
            #region Ustawienia checkBox'ów

            if (checkBox24.Checked == true)
            {
                checkBox25.Enabled = true;
                checkBox26.Enabled = true;
                checkBox27.Enabled = true;
                checkBox28.Enabled = true;
            }
            else
            {
                checkBox25.Enabled = false;
                checkBox26.Enabled = false;
                checkBox27.Enabled = false;
                checkBox28.Enabled = false;
            }


            if (checkBox21.Checked == true)
            {
                checkBox24.Checked = false;
            }
            else
            {
                checkBox24.Checked = true;
            }
            if (checkBox1.Checked == true)
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
                checkBox19.Enabled = true;
                checkBox20.Enabled = true;
                checkBox20.Checked = true;

            }
            else
            {
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox19.Enabled = false;
                checkBox20.Enabled = false;
                checkBox20.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;
            }


           
            switch (checkBox2.Checked)
            {
                case false:
                    label78.Image = Properties.Resources.pl;
                    break;
                case true:
                    label78.Image = Properties.Resources.europeanunion;
                    break;
            }
            if (checkBox61.Checked == true)
            {
                checkBox62.Checked = false;
                checkBox63.Checked = false;
                checkBox64.Checked = false;
            }
            if (checkBox62.Checked == true)
            {
                checkBox61.Checked = false;
                checkBox63.Checked = false;
                checkBox64.Checked = false;
            }
            if (checkBox63.Checked == true)
            {
                checkBox61.Checked = false;
                checkBox62.Checked = false;
                checkBox64.Checked = false;
            }
            if (checkBox64.Checked == true)
            {
                checkBox61.Checked = false;
                checkBox62.Checked = false;
                checkBox63.Checked = false;
            }

            comboBox3.SelectedIndex = 2;
            if (rozpoczecie == true)
            {
                button8.Enabled = false;
            }
            else
            {
                button8.Enabled = true;
            }


            #endregion

           

          
           
            


            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");


            try
                {
                    con.Open();
                    sql = "Select * from Zalogowany";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        zalogowany = reader["Nazwa"].ToString();
                    }
                    reader.Close();


                    nazwa_bazy = "Dane_Firmy_" + zalogowany + "";

                    sql = "Create table if not exists'" + nazwa_bazy + "'(Lp Integer Primary Key,Nazwa_skrocona varchar(20),Nazwa_pelna varchar(20),NIP_panstwo varchar(20),NIP varchar(20),REGON varchar(20),Nr_zezwolenia varchar(20),Urzad_skarbowy varchar(20),Nazwa_banku vaechar(20),Nr_konta_eban varchar(20),Nr_konta varchar(20),Podatnik_na_vat varchar(20),Podatnik_vat_imie varchar (20),Podatnik_vat_nazwisko varchar (20),Podatnik_vat_nr_telefonu varchar (20),numeric_1 varchar (20),Podatnik_unijny varchar (20),Rozliczenie_kwartalne_vat_I_kwartal varchar(20),Rozliczenie_vat_II_kwartal varchar (20),Rozliczenie_vat_III_kwartal varchar(20),Rozliczenie_vat_IV_kwartal varchar (20),Metoda_kasowa_I_kwartal varchar(20),Metoda_kasowa_II_kwartal varchar(20),Metoda_kasowa_III_kwartal varchar(20),Metoda_kasowa_IV_kwartal varchar(20),Z_rozrachunkow_I_kwartal varchar(20),Z_rozrachunkow_II_kwartal varchar(20),Z_rozrachunkow_III_kwartal varchar(20),Z_rozrachunkow_IV_kwartal varchar (20),Rozliczenie_kwartalne_vat_eu_I_kwartal varchar(20),Rozliczenie_kwartalne_vat_eu_II_kwartal varchar(20),Rozliczenie_kwartalne_vat_eu_III_kwartal varchar(20),Rozliczenie_kwartalne_vat_eu_IV_kwartal varchar(20),VAT_zwolnione_netto varchar(20),VAT_procent_vat varchar(20),PKPiR varchar(20),Nowy_wzor_pkpir varchar(20),Metoda_uproszczona varchar(20),Ewidencja_przychodow varchar(20),Ewidencja_przychodow_I_kwartal varchar(20),Ewidencja_przychodow_II_kwartal varchar (20),Ewidencja_przychodow_III_kwartal varchar(20),Ewidencja_przychodow_IV_kwartal varchar(20),Zaokranglaj varchar(20),Rodzaj_prowadzonej_dzialalnosci varchar(20),numeric_2 varchar(20),Kraj varchar(20),Wojewodztwo varchar(20),Powiat varchar(20),Gmina varchar(20),Ulica_dane_adresowe varchar(20),Nr_domu_dane_adresowe varchar(20),Nr_lokalu_dane_adresowe varchar(20),Miejscowosc_dane_adresowe varchar(20),Kod_I_dane_adresowe varchar (20),Kod_II_dane_adresowe varchar(20),Poczta_dane_adresowe varchar(20),Tel_fax_dane_adresowe varchar(20),E_mail_dane_adresowe varchar(20),Miejsce_miejscowosc_dane_adresowe varchar (20),Miejsce_ulica_dane_adresowe varchar(20),Miejsce_nr_domu_dane_adresowe varchar (20),Miejsce_nr_lokalu_dane_adresowe varchar(20),Magazyn_domyslna_jednostka varchar (20),numeric_3 varchar(20),numeric_4 varchar(20),numeric_5 varchar(20),Magazyn_wspolna_dla_calego_roku varchar(20),Magazyn_odrebna_dla_kazdego_miesiaca varchar (20),Magazyn_domyslny_magazyn varchar (20),Magazyn_domyslny_opis varchar (20),Magazyn_domyslny_typ varchar(20),Magazyn_wspolna_numeracja_faktur varchar(20),Magazyn_automatyczna_numeracja_indeksow varchar(20),Magazyn_zaplata_zero varchar(20),Magazyn_zapobiegaj_zmianom varchar(20),Magazyn_ksieguj_w_tym_samym_programie varchar (20),Magazyn_ksieguj_w_innym_programie varchar(20),Magazyn_nie_ksieguj varchar(20),Magazyn_rozrachunki_razem_z_dokumentem varchar(20),Magazyn_rozrachunki_odrebne varchar(20),Magazyn_nie_wprowadzaj_rozrachunkow varchar(20),Magazyn_ksieguj_dokumenty_wedlug_regol varchar(20),Magazyn_wprowadzaj_rozrachunki_automatycznie varchar(20),Magazyn_ksieguj_w_dacie_wystawienia varchar(20),Magazyn_drukuj_rachunek_zamiast_faktura varchar(20),Magazyn_w_danych_kontrahenta_drukuj_miejsce varchar(20),Magazyn_w_danych_kontrahenta_drukuj_nr_zezwolenia varchar(20),Magazyn_w_danych_kontrahenta_drukuj_REGON varchar(20),Magazyn_w_fakturze_drukuj_indeks varchar(20),Magazyn_drukuj_informacje_na_fakturze_o_wz varchar(20),Magazyn_na_wz_drukuj_cene varchar(20),Magazyn_drukuj_podsumowanie_ilosci varchar(20),Magazyn_na_fakturach_drukuj_nazwe_skrocona_kontrahenta varchar(20),Magazyn_informacja_o_rabatach varchar(20),Magazyn_drukuj_date_termin_zaplaty varchar(20),Magazyn_na_fakturach_drukuj_poczta varchar(20),Magazyn_na_fakturze_walutowej_informacja_o_wartosci_pln varchar(20),Magazyn_drukuj_stopke varchar(20),Magazyn_informacja_o_niezaplaconych_fakturach varchar(20),Magazyn_informacja_do_zaplaty_pozostalo varchar (20),numeric_6 varchar(20),numeric_7 varchar(20),Magazyn_rodzaj_drukowanej_faktury_orginal varchar(20),Magazyn_rodzaj_drukowanej_faktury_kopia varchar(20),Magazyn_rodzaj_drukowanej_faktury_orginal_kopia varchar(20),Magazyn_rodzaj_drukowanej_faktury_nieokreslony varchar(20),Magazyn_drukuj_kwote_slownie varchar(20),Magazyn_drukuj_informacje_o_rabacie_na_zamowieniach varchar (20),Magazyn_na_zamowieniach_drukuj_obok_siebie_ceny_brutto_netto varchar(20),Magazyn_sprzedawaj_w_cenach_brutto varchar(20),Magazyn_ustal_cene_sprzedazy_1_wedlug_narzutu varchar (20),Magazyn_sprzedawaj_wedlug_ostatniej_ceny_sprzedazy_kontrahenta varchar(20),Magazyn_cena_sprzedazy_1 varchar(20),Magazyn_cena_sprzedazy_2 varchar(20),Magazyn_cena_sprzedazy_3 varchar(20),Magazyn_cena_sprzedazy_4 varchar (20),Magazyn_wystawiaj_faktury_bez_podpisu varchar(20),Magazyn_podpis_osoby_upowaznionej varchar (20),Rozliczenia_numeracja_wspolna varchar(20),Rozliczenia_numeracja_odrebna varchar(20),numeric_8 varchar(20),Rozliczenia_tytul_wezwania varchar(20),Rozliczenia_naglowek_wezwania varchar(20),Rozliczenia_stopka_wezwania varchar(20),Rozliczenia_tytul_nota varchar(20),Rozliczenia_naglowek_nota varchar(20),Rozliczenia_stopka_nota varchar(20),Rozliczenia_podpis varchar(20),Srodki_trwale varchar (20),Zus_nazwa varchar(20),Zus_NIP varchar (20),Zus_REGON varchar (20),Zus_PESEL varchar(20),Zus_d_o varchar(20),Zus_paszport varchar(20),Zus_seria_numer varchar(20),Zus_imie varchar(20),Zus_nazwisko varchar(20),Zus_rok_urodzenia varchar(20),Zus_miesiac_urodzenia varchar(20),Zus_dzien_urodzenia varchar (20),Zus_koszty_nieograniczone varchar(20),Zus_koszty_do_wysokosci_placy_brutto_pomniejszone_o_zus varchar(20),Zus_koszty_do_wysokosci_placy_brutto varchar(20))";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Select * from '" + nazwa_bazy + "'";
                    cmd = new SQLiteCommand(sql, con);
                    reader = cmd.ExecuteReader();

                    #region Odczyt z bazy danych


                    while (reader.Read())
                    {
                        textBox1.Text = reader["Nazwa_skrocona"].ToString();
                        textBox2.Text = reader["Nazwa_pelna"].ToString();
                        comboBox1.SelectedIndex = Convert.ToInt32(reader["NIP_panstwo"]);
                        textBox3.Text = reader["NIP"].ToString();
                        textBox4.Text = reader["REGON"].ToString();
                        textBox5.Text = reader["Nr_zezwolenia"].ToString();
                        comboBox2.SelectedIndex = Convert.ToInt32(reader["Urzad_skarbowy"]);
                        textBox6.Text = reader["Nazwa_banku"].ToString();
                        textBox7.Text = reader["Nr_konta_eban"].ToString();
                        textBox8.Text = reader["Nr_konta"].ToString();
                        if (reader["Podatnik_na_vat"].ToString() == "True")
                            checkBox1.Checked = true;
                        else
                            checkBox1.Checked = false;
                        textBox9.Text = reader["Podatnik_vat_imie"].ToString();
                        textBox10.Text = reader["Podatnik_vat_nazwisko"].ToString();
                        textBox11.Text = reader["Podatnik_vat_nr_telefonu"].ToString();
                        numericUpDown1.Value = Convert.ToInt32(reader["numeric_1"]);
                        if (reader["Podatnik_unijny"].ToString() == "True")
                            checkBox2.Checked = true;
                        else
                            checkBox2.Checked = false;
                        if (reader["Rozliczenie_kwartalne_vat_I_kwartal"].ToString() == "True")
                            checkBox3.Checked = true;
                        else
                            checkBox3.Checked = false;
                        if (reader["Rozliczenie_vat_II_kwartal"].ToString() == "True")
                            checkBox4.Checked = true;
                        else
                            checkBox4.Checked = false;
                        if (reader["Rozliczenie_vat_III_kwartal"].ToString() == "True")
                            checkBox5.Checked = true;
                        else
                            checkBox5.Checked = false;
                        if (reader["Rozliczenie_vat_IV_kwartal"].ToString() == "True")
                            checkBox6.Checked = true;
                        else
                            checkBox6.Checked = false;
                        if (reader["Metoda_kasowa_I_kwartal"].ToString() == "True")
                            checkBox7.Checked = true;
                        else
                            checkBox7.Checked = false;
                        if (reader["Metoda_kasowa_II_kwartal"].ToString() == "True")
                            checkBox8.Checked = true;
                        else
                            checkBox8.Checked = false;
                        if (reader["Metoda_kasowa_III_kwartal"].ToString() == "True")
                            checkBox9.Checked = true;
                        else
                            checkBox9.Checked = false;
                        if (reader["Metoda_kasowa_IV_kwartal"].ToString() == "True")
                            checkBox10.Checked = true;
                        else
                            checkBox10.Checked = false;
                        if (reader["Z_rozrachunkow_I_kwartal"].ToString() == "True")
                            checkBox11.Checked = true;
                        else
                            checkBox11.Checked = false;
                        if (reader["Z_rozrachunkow_II_kwartal"].ToString() == "True")
                            checkBox12.Checked = true;
                        else
                            checkBox12.Checked = false;
                        if (reader["Z_rozrachunkow_III_kwartal"].ToString() == "True")
                            checkBox13.Checked = true;
                        else
                            checkBox13.Checked = false;
                        if (reader["Z_rozrachunkow_IV_kwartal"].ToString() == "True")
                            checkBox14.Checked = true;
                        else
                            checkBox14.Checked = false;
                        if (reader["Rozliczenie_kwartalne_vat_eu_I_kwartal"].ToString() == "True")
                            checkBox15.Checked = true;
                        else
                            checkBox15.Checked = false;
                        if (reader["Rozliczenie_kwartalne_vat_eu_II_kwartal"].ToString() == "True")
                            checkBox16.Checked = true;
                        else
                            checkBox16.Checked = false;
                        if (reader["Rozliczenie_kwartalne_vat_eu_III_kwartal"].ToString() == "True")
                            checkBox17.Checked = true;
                        else
                            checkBox17.Checked = false;
                        if (reader["Rozliczenie_kwartalne_vat_eu_IV_kwartal"].ToString() == "True")
                            checkBox18.Checked = true;
                        else
                            checkBox18.Checked = false;
                        if (reader["VAT_zwolnione_netto"].ToString() == "True")
                            checkBox19.Checked = true;
                        else
                            checkBox19.Checked = false;
                        if (reader["VAT_procent_vat"].ToString() == "True")
                            checkBox20.Checked = true;
                        else
                        {
                            checkBox20.Checked = false;
                        }
                        if (reader["PKPiR"].ToString() == "True")
                            checkBox21.Checked = true;
                        else
                            checkBox21.Checked = false;
                        if (reader["Nowy_wzor_pkpir"].ToString() == "True")
                            checkBox22.Checked = true;
                        else
                            checkBox22.Checked = false;
                        if (reader["Metoda_uproszczona"].ToString() == "True")
                            checkBox23.Checked = true;
                        else
                            checkBox23.Checked = false;
                        if (reader["Ewidencja_przychodow"].ToString() == "True")
                            checkBox24.Checked = true;
                        else
                            checkBox24.Checked = false;
                        if (reader["Ewidencja_przychodow_I_kwartal"].ToString() == "True")
                            checkBox25.Checked = true;
                        else
                            checkBox25.Checked = false;
                        if (reader["Ewidencja_przychodow_II_kwartal"].ToString() == "True")
                            checkBox26.Checked = true;
                        else
                            checkBox26.Checked = false;
                        if (reader["Ewidencja_przychodow_III_kwartal"].ToString() == "True")
                            checkBox27.Checked = true;
                        else
                            checkBox27.Checked = false;
                        if (reader["Ewidencja_przychodow_IV_kwartal"].ToString() == "True")
                            checkBox28.Checked = true;
                        else
                            checkBox28.Checked = false;
                        if (reader["Zaokranglaj"].ToString() == "True")
                            checkBox29.Checked = true;
                        else
                            checkBox29.Checked = false;
                        textBox12.Text = reader["Rodzaj_prowadzonej_dzialalnosci"].ToString();
                        numericUpDown2.Value = Convert.ToInt32(reader["numeric_2"]);
                        textBox13.Text = reader["Kraj"].ToString();
                        textBox14.Text = reader["Wojewodztwo"].ToString();
                        textBox15.Text = reader["Powiat"].ToString();
                        textBox16.Text = reader["Gmina"].ToString();
                        textBox17.Text = reader["Ulica_dane_adresowe"].ToString();
                        textBox18.Text = reader["Nr_domu_dane_adresowe"].ToString();
                        textBox19.Text = reader["Nr_lokalu_dane_adresowe"].ToString();
                        textBox20.Text = reader["Miejscowosc_dane_adresowe"].ToString();
                        textBox21.Text = reader["Kod_I_dane_adresowe"].ToString();
                        textBox22.Text = reader["Kod_II_dane_adresowe"].ToString();
                        textBox23.Text = reader["Poczta_dane_adresowe"].ToString();
                        textBox24.Text = reader["Tel_fax_dane_adresowe"].ToString();
                        textBox25.Text = reader["E_mail_dane_adresowe"].ToString();
                        textBox26.Text = reader["Miejsce_miejscowosc_dane_adresowe"].ToString();
                        textBox27.Text = reader["Miejsce_ulica_dane_adresowe"].ToString();
                        textBox28.Text = reader["Miejsce_nr_domu_dane_adresowe"].ToString();
                        textBox29.Text = reader["Miejsce_nr_lokalu_dane_adresowe"].ToString();
                        textBox50.Text = reader["Magazyn_domyslna_jednostka"].ToString();
                        numericUpDown3.Value = Convert.ToInt32(reader["numeric_3"]);
                        numericUpDown4.Value = Convert.ToInt32(reader["numeric_4"]);
                        numericUpDown5.Value = Convert.ToInt32(reader["numeric_5"]);
                        if (reader["Magazyn_wspolna_dla_calego_roku"].ToString() == "True")
                            checkBox30.Checked = true;
                        else
                            checkBox30.Checked = false;
                        if (reader["Magazyn_odrebna_dla_kazdego_miesiaca"].ToString() == "True")
                            checkBox31.Checked = true;
                        else
                            checkBox31.Checked = false;
                        if (reader["Magazyn_wspolna_numeracja_faktur"].ToString() == "True")
                            checkBox32.Checked = true;
                        else
                            checkBox32.Checked = false;
                        textBox48.Text = reader["Magazyn_domyslny_magazyn"].ToString();
                        textBox49.Text = reader["Magazyn_domyslny_opis"].ToString();
                        
                        int i;
                        i = comboBox3.FindStringExact(reader["Magazyn_domyslny_typ"].ToString());
                        comboBox3.SelectedIndex = i;
                        if (reader["Magazyn_automatyczna_numeracja_indeksow"].ToString() == "True")
                            checkBox33.Checked = true;
                        else
                            checkBox33.Checked = false;
                        if (reader["Magazyn_zaplata_zero"].ToString() == "True")
                            checkBox34.Checked = true;
                        else
                            checkBox34.Checked = false;
                        if (reader["Magazyn_zapobiegaj_zmianom"].ToString() == "True")
                            checkBox35.Checked = true;
                        else
                            checkBox35.Checked = false;
                        if (reader["Magazyn_ksieguj_w_tym_samym_programie"].ToString() == "True")
                            checkBox36.Checked = true;
                        else
                            checkBox36.Checked = false;
                        if (reader["Magazyn_ksieguj_w_innym_programie"].ToString() == "True")
                            checkBox37.Checked = true;
                        else
                            checkBox37.Checked = false;
                        if (reader["Magazyn_nie_ksieguj"].ToString() == "True")
                            checkBox38.Checked = true;
                        else
                            checkBox38.Checked = false;
                        if (reader["Magazyn_rozrachunki_razem_z_dokumentem"].ToString() == "True")
                            checkBox39.Checked = true;
                        else
                            checkBox39.Checked = false;
                        if (reader["Magazyn_rozrachunki_odrebne"].ToString() == "True")
                            checkBox40.Checked = true;
                        else
                            checkBox40.Checked = false;
                        if (reader["Magazyn_nie_wprowadzaj_rozrachunkow"].ToString() == "True")
                            checkBox41.Checked = true;
                        else
                            checkBox41.Checked = false;
                        if (reader["Magazyn_ksieguj_dokumenty_wedlug_regol"].ToString() == "True")
                            checkBox42.Checked = true;
                        else
                            checkBox42.Checked = false;
                        if (reader["Magazyn_wprowadzaj_rozrachunki_automatycznie"].ToString() == "True")
                            checkBox43.Checked = true;
                        else
                            checkBox43.Checked = false;
                        if (reader["Magazyn_ksieguj_w_dacie_wystawienia"].ToString() == "True")
                            checkBox44.Checked = true;
                        else
                            checkBox44.Checked = false;
                        if (reader["Magazyn_drukuj_rachunek_zamiast_faktura"].ToString() == "True")
                            checkBox45.Checked = true;
                        else
                            checkBox45.Checked = false;
                        if (reader["Magazyn_w_danych_kontrahenta_drukuj_miejsce"].ToString() == "True")
                            checkBox46.Checked = true;
                        else
                            checkBox46.Checked = false;
                        if (reader["Magazyn_w_danych_kontrahenta_drukuj_nr_zezwolenia"].ToString() == "True")
                            checkBox47.Checked = true;
                        else
                            checkBox47.Checked = false;
                        if (reader["Magazyn_w_danych_kontrahenta_drukuj_REGON"].ToString() == "True")
                            checkBox48.Checked = true;
                        else
                            checkBox48.Checked = false;
                        if (reader["Magazyn_w_fakturze_drukuj_indeks"].ToString() == "True")
                            checkBox49.Checked = true;
                        else
                            checkBox49.Checked = false;
                        if (reader["Magazyn_drukuj_informacje_na_fakturze_o_wz"].ToString() == "True")
                            checkBox50.Checked = true;
                        else
                            checkBox50.Checked = false;
                        if (reader["Magazyn_na_wz_drukuj_cene"].ToString() == "True")
                            checkBox51.Checked = true;
                        else
                            checkBox51.Checked = false;
                        if (reader["Magazyn_drukuj_podsumowanie_ilosci"].ToString() == "True")
                            checkBox52.Checked = true;
                        else
                            checkBox52.Checked = false;
                        if (reader["Magazyn_na_fakturach_drukuj_nazwe_skrocona_kontrahenta"].ToString() == "True")
                            checkBox53.Checked = true;
                        else
                            checkBox53.Checked = false;
                        if (reader["Magazyn_informacja_o_rabatach"].ToString() == "True")
                            checkBox54.Checked = true;
                        else
                            checkBox54.Checked = false;
                        if (reader["Magazyn_drukuj_date_termin_zaplaty"].ToString() == "True")
                            checkBox55.Checked = true;
                        else
                            checkBox55.Checked = false;
                        if (reader["Magazyn_na_fakturach_drukuj_poczta"].ToString() == "True")
                            checkBox56.Checked = true;
                        else
                            checkBox56.Checked = false;
                        if (reader["Magazyn_na_fakturze_walutowej_informacja_o_wartosci_pln"].ToString() == "True")
                            checkBox57.Checked = true;
                        else
                            checkBox57.Checked = false;
                        if (reader["Magazyn_drukuj_stopke"].ToString() == "True")
                            checkBox58.Checked = true;
                        else
                            checkBox58.Checked = false;
                        if (reader["Magazyn_informacja_o_niezaplaconych_fakturach"].ToString() == "True")
                            checkBox59.Checked = true;
                        else
                            checkBox59.Checked = false;
                        if (reader["Magazyn_informacja_do_zaplaty_pozostalo"].ToString() == "True")
                            checkBox60.Checked = true;
                        else
                            checkBox60.Checked = false;
                        numericUpDown6.Value = Convert.ToInt32(reader["numeric_6"]);
                        numericUpDown7.Value = Convert.ToInt32(reader["numeric_7"]);
                        if (reader["Magazyn_rodzaj_drukowanej_faktury_orginal"].ToString() == "True")
                            checkBox61.Checked = true;
                        else
                            checkBox61.Checked = false;
                        if (reader["Magazyn_rodzaj_drukowanej_faktury_kopia"].ToString() == "True")
                            checkBox62.Checked = true;
                        else
                            checkBox62.Checked = false;
                        if (reader["Magazyn_rodzaj_drukowanej_faktury_orginal_kopia"].ToString() == "True")
                            checkBox63.Checked = true;
                        else
                            checkBox63.Checked = false;
                        if (reader["Magazyn_rodzaj_drukowanej_faktury_nieokreslony"].ToString() == "True")
                            checkBox64.Checked = true;
                        else
                            checkBox64.Checked = false;
                        if (reader["Magazyn_drukuj_kwote_slownie"].ToString() == "True")
                            checkBox65.Checked = true;
                        else
                            checkBox65.Checked = false;
                        if (reader["Magazyn_drukuj_informacje_o_rabacie_na_zamowieniach"].ToString() == "True")
                            checkBox66.Checked = true;
                        else
                            checkBox66.Checked = false;
                        if (reader["Magazyn_na_zamowieniach_drukuj_obok_siebie_ceny_brutto_netto"].ToString() == "True")
                            checkBox67.Checked = true;
                        else
                            checkBox67.Checked = false;
                        if (reader["Magazyn_sprzedawaj_w_cenach_brutto"].ToString() == "True")
                            checkBox68.Checked = true;
                        else
                            checkBox68.Checked = false;
                        if (reader["Magazyn_ustal_cene_sprzedazy_1_wedlug_narzutu"].ToString() == "True")
                            checkBox69.Checked = true;
                        else
                            checkBox69.Checked = false;
                        if (reader["Magazyn_sprzedawaj_wedlug_ostatniej_ceny_sprzedazy_kontrahenta"].ToString() == "True")
                            checkBox70.Checked = true;
                        else
                            checkBox70.Checked = false;
                        textBox30.Text = reader["Magazyn_cena_sprzedazy_1"].ToString();
                        textBox31.Text = reader["Magazyn_cena_sprzedazy_2"].ToString();
                        textBox32.Text = reader["Magazyn_cena_sprzedazy_3"].ToString();
                        textBox33.Text = reader["Magazyn_cena_sprzedazy_4"].ToString();
                        if (reader["Magazyn_wystawiaj_faktury_bez_podpisu"].ToString() == "True")
                            checkBox71.Checked = true;
                        else
                            checkBox71.Checked = false;
                        textBox34.Text = reader["Magazyn_podpis_osoby_upowaznionej"].ToString();
                        if (reader["Rozliczenia_numeracja_wspolna"].ToString() == "True")
                            checkBox72.Checked = true;
                        else
                            checkBox72.Checked = false;
                        if (reader["Rozliczenia_numeracja_odrebna"].ToString() == "True")
                            checkBox73.Checked = true;
                        else
                            checkBox73.Checked = false;
                        numericUpDown8.Value = Convert.ToInt32(reader["numeric_8"]);
                        textBox35.Text = reader["Rozliczenia_tytul_wezwania"].ToString();
                        richTextBox1.Text = reader["Rozliczenia_naglowek_wezwania"].ToString();
                        richTextBox2.Text = reader["Rozliczenia_stopka_wezwania"].ToString();
                        textBox36.Text = reader["Rozliczenia_tytul_nota"].ToString();
                        richTextBox3.Text = reader["Rozliczenia_naglowek_nota"].ToString();
                        richTextBox4.Text = reader["Rozliczenia_stopka_nota"].ToString();
                        textBox37.Text = reader["Rozliczenia_podpis"].ToString();
                        if (reader["Srodki_trwale"].ToString() == "True")
                            checkBox74.Checked = true;
                        else
                            checkBox74.Checked = false;
                        textBox38.Text = reader["Zus_nazwa"].ToString();
                        textBox39.Text = reader["Zus_NIP"].ToString();
                        textBox40.Text = reader["Zus_REGON"].ToString();
                        textBox41.Text = reader["Zus_PESEL"].ToString();
                        if (reader["Zus_d_o"].ToString() == "True")
                            checkBox75.Checked = true;
                        else
                            checkBox75.Checked = false;
                        if (reader["Zus_paszport"].ToString() == "True")
                            checkBox76.Checked = true;
                        else
                            checkBox76.Checked = false;
                        textBox42.Text = reader["Zus_seria_numer"].ToString();
                        textBox43.Text = reader["Zus_imie"].ToString();
                        textBox44.Text = reader["Zus_nazwisko"].ToString();
                        textBox45.Text = reader["Zus_rok_urodzenia"].ToString();
                        textBox46.Text = reader["Zus_miesiac_urodzenia"].ToString();
                        textBox47.Text = reader["Zus_dzien_urodzenia"].ToString();
                        if (reader["Zus_koszty_nieograniczone"].ToString() == "True")
                            checkBox77.Checked = true;
                        else
                            checkBox77.Checked = false;
                        if (reader["Zus_koszty_do_wysokosci_placy_brutto_pomniejszone_o_zus"].ToString() == "True")
                            checkBox78.Checked = true;
                        else
                            checkBox78.Checked = false;
                        if (reader["Zus_koszty_do_wysokosci_placy_brutto"].ToString() == "True")
                            checkBox79.Checked = true;
                        else
                            checkBox79.Checked = false;

                    }


                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }

            path_1 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
            if (!System.IO.File.Exists(path_1))
            {
                SQLiteConnection.CreateFile(path_1);
            }

             con = new SQLiteConnection("Data Source='" + path_1 + "';Version=3;");
            try
            {
                con.Open();
                sql = "Create table if not exists Magazyny (Lp INTEGER PRIMARY KEY , Symbol varchar (20),Opis varchar(20),Typ varchar(20))";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Magazyn ( Symbol varchar (20),Opis varchar(20),Typ varchar(20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Create table if not exists Jednostki_miary (Lp Integer Primary Key,Nazwa_jednostki varchar (20))";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }

            AutoCompletText();



        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = Properties.Resources.Wezwanie_do_zaplaty_naglowek;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = Properties.Resources.Wezwanie_do_zaplaty_stopka;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = Properties.Resources.Nota_odsetkowa_naglowek;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox4.Text = Properties.Resources.Nota_odsetkowa_stopka;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            #region Ustawienia Flag


            switch (comboBox1.SelectedIndex)
            {
                case 1:
                label77.Image=Properties.Resources.at;
                break;
                case 2:
                label77.Image = Properties.Resources.be;
                break;
                case 3:
                label77.Image = Properties.Resources.bg;
                break;
                case 4:
                label77.Image = Properties.Resources.cy;
                break;
                case 5:
                label77.Image = Properties.Resources.cz;
                break;
                case 6:
                label77.Image = Properties.Resources.de;
                break;
                case 7:
                label77.Image = Properties.Resources.dk;
                break;
                case 8:
                label77.Image = Properties.Resources.ee;
                break;
                case 9:
                label77.Image = Properties.Resources.es;
                break;
                case 10:
                label77.Image = Properties.Resources.europeanunion;
                break;
                case 11:
                label77.Image = Properties.Resources.fi;
                break;
                case 12:
                label77.Image = Properties.Resources.fr;
                break;
                case 13:
                label77.Image = Properties.Resources.gb;
                break;
                case 14:
                label77.Image = Properties.Resources.hu;
                break;
                case 15:
                label77.Image = Properties.Resources.hr;
                break;
                case 16:
                label77.Image = Properties.Resources.ie;
                break;
                case 17:
                label77.Image = Properties.Resources.it;
                break;
                case 18:
                label77.Image = Properties.Resources.lt;
                break;
                case 19:
                label77.Image = Properties.Resources.lu;
                break;
                case 20:
                label77.Image=Properties.Resources.lv;
                break;
                case 21:
                label77.Image = Properties.Resources.mt;
                break;
                case 22:
                label77.Image = Properties.Resources.nl;
                break;
                case 23:
                label77.Image = Properties.Resources.pl;
                break;
                case 24:
                label77.Image = Properties.Resources.pt;
                break;
                case 25:
                label77.Image = Properties.Resources.ro;
                break;
                case 26:
                label77.Image = Properties.Resources.se;
                break;
                case 27:
                label77.Image = Properties.Resources.sk;
                break;
                case 28:
                label77.Image = Properties.Resources.si;
                break;

            #endregion




            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkBox2.Checked)
            {
                case false:
                label78.Image = Properties.Resources.pl;
                checkBox15.Enabled = false;
                checkBox16.Enabled = false;
                checkBox17.Enabled = false;
                checkBox18.Enabled = false;
                checkBox15.Checked = false;
                checkBox16.Checked = false;
                checkBox17.Checked = false;
                checkBox18.Checked = false;
                break;
                case true:
                label78.Image = Properties.Resources.europeanunion;
                checkBox15.Enabled = true;
                checkBox16.Enabled = true;
                checkBox17.Enabled = true;
                checkBox18.Enabled = true;
                break;
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
                checkBox19.Enabled = true;
                checkBox20.Enabled = true;
                checkBox20.Checked = true;

            }
            else
            {
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox19.Enabled = false;
                checkBox20.Enabled = false;
                checkBox20.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox7.Enabled = true;
            }
            else
            {
                checkBox7.Enabled = false;
                checkBox7.Checked = false;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                checkBox11.Enabled = true;
            }
            else
            {
                checkBox11.Enabled = false;
                checkBox11.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                checkBox8.Enabled = true;
            }
            else
            {
                checkBox8.Enabled = false;
                checkBox8.Checked = false;
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                checkBox12.Enabled = true;
            }
            else
            {
                checkBox12.Enabled = false;
                checkBox12.Checked = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                checkBox9.Enabled = true;
            }
            else
            {
                checkBox9.Enabled = false;
                checkBox9.Checked = false;
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked == true)
            {
                checkBox13.Enabled = true;

            }
            else
            {
                checkBox13.Enabled = false;
                checkBox13.Checked = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                checkBox10.Enabled = true;
            }
            else
            {
                checkBox10.Enabled = false;
                checkBox10.Checked = false;
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked == true)
            {
                checkBox14.Enabled = true;
            }
            else
            {
                checkBox14.Enabled = false;
                checkBox14.Checked=false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.ShowDialog();
        }

        private void checkBox75_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox75.Checked == true)
            {
                checkBox76.Checked = false;
            }
            else
            {
                checkBox76.Checked = true;
            }
        }

        private void checkBox76_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox76.Checked == true)
            {
                checkBox75.Checked = false;
            }
            else
            {
                checkBox75.Checked = true;
            }
        }

        private void checkBox77_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox77.Checked == true)
            {
                checkBox78.Checked = false;
                checkBox79.Checked = false;
            }
                        

            
        }

        private void checkBox78_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox78.Checked == true)
            {
                checkBox77.Checked = false;
                checkBox79.Checked = false;
            }
        }

        private void checkBox79_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox79.Checked == true)
            {
                checkBox77.Checked = false;
                checkBox78.Checked = false;
            }
        }

        private void checkBox72_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox72.Checked == true)
            {
                checkBox73.Checked = false;
            }
        }

        private void checkBox73_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox73.Checked == true)
            {
                checkBox72.Checked = false;
            }
        }

        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox30.Checked == true)
            {
                checkBox31.Checked = false;
            }
        }

        private void checkBox31_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox31.Checked == true)
            {
                checkBox30.Checked = false;
            }
        }

        private void checkBox36_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox36.Checked == true)
            {
                checkBox37.Checked = false;
                checkBox38.Checked = false;
            }
        }

        private void checkBox37_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox37.Checked == true)
            {
                checkBox36.Checked = false;
                checkBox38.Checked = false;
            }
        }

        private void checkBox38_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox38.Checked == true)
            {
                checkBox37.Checked = false;
                checkBox36.Checked = false;
            }
        }

        private void checkBox39_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox39.Checked == true)
            {
                checkBox40.Checked = false;
                checkBox41.Checked = false;
            }
        }

        private void checkBox40_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox40.Checked == true)
            {
                checkBox39.Checked = false;
                checkBox41.Checked = false;
            }
        }

        private void checkBox41_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox41.Checked == true)
            {
                checkBox40.Checked = false;
                checkBox39.Checked = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.BackColor = Color.White;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.BackColor = Color.White;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.BackColor = Color.White;
        }

        private void checkBox61_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox61.Checked == true)
            {
                checkBox62.Checked = false;
                checkBox63.Checked = false;
                checkBox64.Checked = false;
            }
            
        }

        private void checkBox62_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox62.Checked == true)
            {
                checkBox61.Checked = false;
                checkBox63.Checked = false;
                checkBox64.Checked = false;
            }
        }

        private void checkBox63_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox63.Checked == true)
            {
                checkBox61.Checked = false;
                checkBox62.Checked = false;
                checkBox64.Checked = false;
            }
        }

        private void checkBox64_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox64.Checked == true)
            {
                checkBox61.Checked = false;
                checkBox62.Checked = false;
                checkBox63.Checked = false;
            }
        }

        private void textBox48_TextChanged(object sender, EventArgs e)
        {

           
        }

        private void textBox49_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox69_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox69.Checked == true)
            {
                checkBox68.Checked = false;
            }
            
        }

        private void checkBox68_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox68.Checked == true)
            {
                checkBox69.Checked = false;
                textBox30.ReadOnly = false;
                textBox31.ReadOnly = false;
                textBox32.ReadOnly = false;
                textBox33.ReadOnly = false;

            }
            else
            {
                
                textBox30.ReadOnly = true;
                textBox31.ReadOnly = true;
                textBox32.ReadOnly = true;
                textBox33.ReadOnly = true;
                textBox30.Text = string.Empty;
                textBox31.Text = string.Empty;
                textBox32.Text = string.Empty;
                textBox33.Text = string.Empty;

            }
            
            
        }

        private void textBox30_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        private void textBox31_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        private void textBox32_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        private void textBox33_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        private void AutoCompletText()
        {
            textBox49.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox49.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection sc = new AutoCompleteStringCollection();

            path_1 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source='" + path_1 + "';Version=3;");
            try
            {
                con.Open();
                sql = "Select * from Magazyny ";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    opis = reader["Opis"].ToString();
                    sc.Add(opis);
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
            textBox49.AutoCompleteCustomSource = sc;
        }

    }
}
