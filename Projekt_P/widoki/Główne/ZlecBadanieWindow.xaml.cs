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
    /// Logika interakcji dla klasy ZlecBadanieWindow.xaml
    /// </summary>
    public partial class ZlecBadanieWindow : Window
    {
        
        ///<summary>
        ///Konstruktor klasy ZlecBadanieWindow
        ///</summary>
        ///<remarks>
        ///Wywołuje metody Fill_combo i Fill_combo2
        ///</remarks>
        public ZlecBadanieWindow()
        {
            InitializeComponent();
            Fill_combo();
            Fill_combo2();
          
        }





        ///<summary>
        ///Metoda otwierająca interfejs LabWindow po kliknięciu Wroc_Click.
        ///</summary>
        ///<param name="sender"> obiekt wywołujący zdarzenie </param>
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
        void Fill_combo()
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

                PacjentComboBox.Items.Add(da["Nazwisko"].ToString());

            }
            da.Close();

        }





        ///<summary>
        ///Metoda pobierająca Nazwę oraz cenę z tabeli Lab i umieszczenie BadanieComboBox.
        ///</summary>
        void Fill_combo2()
        {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "select Nazwa +''+','+ ' cena: ' + Cena + 'zł' as Cena from Lab";
                cmd.Connection = con;
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    BadanieComboBox.Items.Add(da["Cena"].ToString());

                }
                da.Close();


            }




        ///<summary>
        ///Metoda zapisująca dane w tabeli Badanie. 
        ///</summary>
        ///<remarks>
        ///Metoda zapisuję dane po sprawdzeniu czy wszystkie pola są wypełnione </remarks>
        ///<param name="sender"> obiekt wywołujący zdarzenie </param>
        ///<param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Umow_Click(object sender, RoutedEventArgs e)
        {
            if (PacjentComboBox.Text == "")
            {
                MessageBox.Show("Wybierz pacjenta");
                PacjentComboBox.Focus();

            }
            else if (BadanieComboBox.Text == "")
            {
                MessageBox.Show("Wybierz badanie");
                BadanieComboBox.Focus();
            }
            else if (DatePicker.Text == "")
            {
                MessageBox.Show("Wprowadź datę");
                DatePicker.Focus();
            }

            else
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "Insert into Badanie (Pacjent, Badanie, Data) values (@Pacjent, @Badanie, @Data)";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Pacjent", PacjentComboBox.Text);
                cmd.Parameters.AddWithValue("@Badanie", BadanieComboBox.Text);
                cmd.Parameters.AddWithValue("@Data", DatePicker.SelectedDate);

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







