using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Logika interakcji dla klasy ZlecBadanieTak.xaml
    /// </summary>
    public partial class ZlecBadanieTak : Window
    {



        ///<summary>
        ///Konstruktor klasy ZlecBadanieTak
        ///</summary>
        ///<remarks>
        ///Wywołuje metody Fill_combo1 i Fill_combo3
        ///</remarks>
        public ZlecBadanieTak()
        {
            InitializeComponent();
            Fill_combo1();
            Fill_combo3();
        }





        ///<summary>
        ///Metoda otwierająca interfejs LabWindow po kliknięciu Wroc_Click.
        ///</summary>
        ///<param name="sender">obiekt wywołujący zdarzenie </param>
        ///<param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Wroc_Click(object sender, RoutedEventArgs e)
        {

            LabWindow objLabWindow = new LabWindow();
            this.Visibility = Visibility.Hidden;
            objLabWindow.Show();

        }




        ///<summary>
        ///Metoda pobierająca imię i nazwisko z tabeli Pacjent i umieszczenie w PacjentComboBox.
        ///</summary>
        void Fill_combo1()
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select Imie +''+' '+ Nazwisko as Nazwisko from Pacjent";
            cmd.Connection = con;
            SqlDataReader da = cmd.ExecuteReader();


            while (da.Read())
            {

                PacjentComboBox2.Items.Add(da["Nazwisko"].ToString());

            }
            da.Close();

        }





        ///<summary>
        ///Metoda pobierająca Nazwę z tabeli Lab i umieszczenie BadanieComboBox.
        ///</summary>
        void Fill_combo3()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select Nazwa from Lab";
            cmd.Connection = con;
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                BadanieComboBox2.Items.Add(da["Nazwa"].ToString());

            }
            da.Close();
        }



        ///<summary>
        ///Metoda zapisująca dane w tabeli Badanie. 
        ///</summary>
        ///<remarks>
        ///Metoda zapisuję dane po sprawdzeniu czy wszystkie pola są wypełnione </remarks>
        ///<param name="sender">obiekt wywołujący zdarzenie </param>
        ///<param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Umow_Click(object sender, RoutedEventArgs e)
        {
            if (PacjentComboBox2.Text == "")
            {
                MessageBox.Show("Wybierz pacjenta");
                PacjentComboBox2.Focus();

            }
            else if (BadanieComboBox2.Text == "")
            {
                MessageBox.Show("Wybierz badanie");
                BadanieComboBox2.Focus();
            }
            else if (DatePicker2.Text == "")
            {
                MessageBox.Show("Wprowadź datę");
                DatePicker2.Focus();
            }

            else
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "Insert into Badanie (Pacjent, Badanie, Data) values (@Pacjent, @Badanie, @Data)";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Pacjent", PacjentComboBox2.Text);
                cmd.Parameters.AddWithValue("@Badanie", BadanieComboBox2.Text);
                cmd.Parameters.AddWithValue("@Data", DatePicker2.SelectedDate);

                cmd.Connection = con;
                con.Open();
                int a = cmd.ExecuteNonQuery();

                if (a == 1)
                {
                    MessageBox.Show("Dane dodane prawidłowo");

                }
                con.Close();
            }

        }
    }
}








