using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
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
    /// Logika interakcji dla klasy UmowWizyteWindow.xaml
    /// </summary>
    public partial class UmowWizyteWindow : Window
    {   
        ///<summary>
        ///Konstruktor klasy UmowWizyteWindow
        ///</summary>
        ///<remarks>
        ///wywołuje metody fill1() i fill2()
        /// </remarks>
        public UmowWizyteWindow()
        {
            InitializeComponent();
            fill1();
            fill2();

        }
        ///<summary> 
        ///Metoda otwierająca interfejs WizytyWindow po kliknięciu Wroc_Click
        ///</summary>
        ///<param name="sender"> obiekt wywołujący zdarzenie </param>
        ///<param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Wroc_Click(object sender, RoutedEventArgs e)
        {
            WizytyWindow objWizytyWindow = new WizytyWindow();
            this.Visibility = Visibility.Hidden;
            objWizytyWindow.Show();
        }

        /// <summary>
        /// Metoda pobierająca z tabeli Pacjent Imię i Nazwisko i umieszczająca je w PacjentComboBox.
        /// </summary>
        void fill1()
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select Imie +' '+ Nazwisko as Nazwisko from Pacjent";
            cmd.Connection = con;
            SqlDataReader da = cmd.ExecuteReader();

            while (da.Read())
            {

                PacjentComboBox.Items.Add(da["Nazwisko"].ToString());

            }
            da.Close();


        }

        /// <summary>
        /// Metoda pobierająca z tabeli Doctor Imie i Nazwisko i umieszczająca je w LekarzComboBox.
        /// </summary>
        
        void fill2()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select Imie +' '+ Nazwisko as Nazwisko from Doctor";
            cmd.Connection = con;
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                LekarzComboBox.Items.Add(da["Nazwisko"].ToString());

            }

            da.Close();

        }

        /// <summary>
        /// Metoda licząca wizyty konkretnego dnia u danego lekarza.
        /// </summary>
        /// <returns>
        /// Zwraca liczbę wizyt umówionych konkretnego dnia do konkretnego lekarza.
        /// </returns>
        public int Count()
        {


            var date = Convert.ToDateTime(DatePicker.Text).ToString("yyyy-MM-dd");
            int count = 0;


            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select count(*) from [Visit] where Data='" + date + "' AND Lekarz= @Lekarz";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Lekarz", LekarzComboBox.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable Results = new DataTable();
            da.Fill(Results);


            if (Results.Rows.Count > 0)
            {
                count = int.Parse(Results.Rows[0][0].ToString());
            }


            return count;


        }
        /// <summary>
        /// Metoda zapisująca dane o wizycie do bazy do tabeli Visit.
        /// </summary>
        /// <remarks>
        /// Metoda wywołuje metodę Count.
        /// Maksymalna liczba wizyt do danego lekarza w jednym dniu to pięć. 
        /// </remarks>
        ///<param name="sender"> obiekt wywołujący zdarzenie </param>
        ///<param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Umow_Click(object sender, RoutedEventArgs e)
        {
            if (PacjentComboBox.Text == "")
            {
                MessageBox.Show("Wybierz pacjenta");

            }
            else if (LekarzComboBox.Text == "")
            {
                MessageBox.Show("Wybierz lekarza");

            }
            else if (DatePicker.Text == "")
            {
                MessageBox.Show("Wprowadź datę");
                DatePicker.Focus();
            }
            else
            {
                int count = Count();
                if (count >= 5)
                {
                    MessageBox.Show("Brak miejsc na wybrany dzień do lekarza '" + LekarzComboBox.Text + "' Proszę wybrać inny dzień.");
                }


                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "Insert into Visit (Pacjent, Lekarz, Data) values (@Pacjent, @Lekarz, @Data)";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Pacjent", PacjentComboBox.Text);
                    cmd.Parameters.AddWithValue("@Lekarz", LekarzComboBox.Text);
                    cmd.Parameters.AddWithValue("@Data", DatePicker.SelectedDate);


                    cmd.Connection = con;
                    con.Open();
                    int a = cmd.ExecuteNonQuery();

                    if (a == 1)
                    {
                        MessageBox.Show("Dane dodane prawidłowo");

                    }



                }
            }


        }
    }
}

