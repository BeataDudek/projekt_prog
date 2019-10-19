using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekt_P.widoki.Główne
{ 
    /// <summary>
    /// Logika interakcji dla klasy AddPacjentWindow.xaml
    /// </summary>
    public partial class AddPacjentWindow : Window
    { 
        DataSet ds = new DataSet();
        
        
       
       /// <summary>
       /// Konstruktor Klasy AddPacjentWindow. 
       /// </summary>
       /// <remarks> Narzuca maksymalną długość peselu </remarks>
        public AddPacjentWindow()
        {
            InitializeComponent();
            peselBox.MaxLength = 11;

        }




        ///<summary>
        ///Metoda otwierająca interfejs PacjenciWindow po kliknięciu na Wroc_Click
        ///</summary>
        /// <param name="sender">	obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Wroc_Click(object sender, RoutedEventArgs e)
        {
            PacjenciWindow objPacjenciWindow = new PacjenciWindow();
            this.Visibility = Visibility.Hidden;
            objPacjenciWindow.Show();
        }




        ///<summary>
        ///Metoda sprawdzająca czy długość wprowadzonego tekstu równa jest 11 znakom.
        ///Jeżeli nie, wyświetla komunikat.
        ///</summary>
        /// <param name="sender">	obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        public void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Length != 11)
            {
                MessageBox.Show("Pesel powinien zawierać 11 cyfr");
            }
        }



        ///<summary>
        ///Metoda zapisująca dane w bazie.
        ///</summary>
        ///<remarks>
        ///Metoda sprawdza przed dodaniem czy wszystkie pola są wypełnione oraz czy dany numer pesel nie istnieje już w bazie danych.
        ///Po dodaniu danych do bazy pola są puste.
        ///</remarks>
        /// <param name="sender">	obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (imieBox.Text == "")
            {
                MessageBox.Show("Wprowadź imię pacjenta");
                imieBox.Focus();
            }

            else if (nazwiskoBox.Text == "")
            {
                MessageBox.Show("Wprowadź nazwisko pacjenta");
                nazwiskoBox.Focus();
            }

            else if (peselBox.Text == "")
            {
                MessageBox.Show("Wprowadź pesel pacjenta");
                peselBox.Focus();
            }

            else if (adresBox.Text == "")
            {
                MessageBox.Show("Wprowadź adres zamieszkania pacjenta");
                adresBox.Focus();
            }
            else if (telefonBox.Text == "")
            {
                MessageBox.Show("Wprowadź numer telefonu pacjenta");
                telefonBox.Focus();
            }
        
      
                      
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = ("Select * from [Pacjent] where Pesel='" + peselBox.Text + "'");
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                int i = ds.Tables[0].Rows.Count;
                if (i > 0)
                {
                    MessageBox.Show("W bazie widnieje już pacjent o numerze pesel '" + peselBox.Text + "'");
                    ds.Clear();
                }

             
                else
                { 

                    cmd.CommandText = "Insert into [Pacjent](Imie, Nazwisko, Pesel, Adres, Telefon) values (@Imie, @Nazwisko, @Pesel, @Adres, @Telefon)";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Imie", imieBox.Text);
                    cmd.Parameters.AddWithValue("@Nazwisko", nazwiskoBox.Text);
                    cmd.Parameters.AddWithValue("@Pesel", peselBox.Text);
                    cmd.Parameters.AddWithValue("@Adres", adresBox.Text);
                    cmd.Parameters.AddWithValue("@Telefon", telefonBox.Text);
                    cmd.Connection = con;
                    int a = cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                        MessageBox.Show("Dane dodane prawidłowo");

                    }

                    imieBox.Text = "";
                    nazwiskoBox.Text = "";
                    peselBox.Text = "";
                    adresBox.Text = "";
                    telefonBox.Text = "";


                }


            }


        }
    }
}

