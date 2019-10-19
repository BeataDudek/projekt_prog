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
    /// Logika interakcji dla klasy LabWindow.xaml
    /// </summary>
    public partial class LabWindow : Window
    {   


        /// <summary>
        /// Konstruktor klasy LabWindow 
        /// </summary>
        /// <remarks>
        /// Konstruktor wywołuje metody GetLabList() oraz GetBadanieList()
        /// </remarks>
        public LabWindow()
        {
            InitializeComponent();
            GetLabList();
            GetBadanieList();
        }



        /// <summary>
        /// Metoda otwierająca interfejs MainWindow po kliknięciu Wroc_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wroc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();

        }

        /// <summary>
        /// Metoda otwierająca interfejs AddBadanie po kliknięciu DodajBadanie_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DodajBadanie_Click(object sender, RoutedEventArgs e)
        {
            AddBadanie objAddBadanie = new AddBadanie();
            this.Visibility = Visibility.Hidden;
            objAddBadanie.Show();

        }





        /// <summary>
        /// Metoda otwierająca interfejs LabSkierWindow po kliknięciu UmowBadanie_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UmowBadanie_Click(object sender, RoutedEventArgs e)
        {
            LabSkierWindow objLabSkierWindow = new LabSkierWindow();
            this.Visibility = Visibility.Hidden;
            objLabSkierWindow.Show();

        }




        /// <summary>
        /// Metoda pobierająca dane z bazy z tabeli Lab i umieszczająca je w GridLab
        /// </summary>
        public void GetLabList()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Lab";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Lab");
            da.Fill(dt);
            GridLab.ItemsSource = dt.DefaultView;
        }







        /// <summary>
        /// Metoda pobierająca dane z tabeli Badania i umieszczająca je w GridBadanie.
        /// </summary>
        public void GetBadanieList()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Badanie";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Badanie");
            da.Fill(dt);
            GridBadanie.ItemsSource = dt.DefaultView;
        }




        /// <summary>
        /// Metoda odpowiadająca za usunięcie rekordu z tabeli Lab, którego id wprowadzone zostało do usunBadanieBox.
        /// </summary>
        /// <remarks>
        /// Metoda sprawdza czy w usunBadanieBox jest coś wpisane. Jesli nie, to informuje.
        /// Po usunięciu z bazy wywołuje metodę GetLabList()
        /// </remarks>
        /// <param name="sender">	obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void UsunBadanie_Click(object sender, RoutedEventArgs e)
        {
            if (usunBadanieBox.Text == "")
            {
                MessageBox.Show("Wprowadź id badania do usunięcia");
                usunBadanieBox.Focus();
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from Lab where IdBadania=' " + this.usunBadanieBox.Text + "'; ";
                cmd.Connection = con;

                try
                {
                    con.Open();
                    SqlDataReader da = cmd.ExecuteReader();
                    MessageBox.Show("Badanie usunięte z bazy");// czemu wyswietla zawsze?
                    while (da.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                usunBadanieBox.Text = "";
                GetLabList();
            }
        }



        /// <summary>
        /// Metoda odpowiadająca za usunięcie rekordu z tabeli Badanie, którego id wprowadzone zostało do odwolajBadanieBox.
        /// </summary>
        /// <remarks>
        /// Metoda sprawdza czy w odwolajBadanieBox jest coś wpisane. Jesli nie, to informuje.
        /// Po usunięciu z bazy wywołuje metodę GetBadanieList()
        /// </remarks>
        /// <param name="sender">	obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void OdwolajBadanie_Click(object sender, RoutedEventArgs e)
        {
            if (odwolajBadanieBox.Text == "")
            {
                MessageBox.Show("Wprowadź id badania, które chcesz odwołać");
                odwolajBadanieBox.Focus();
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from Badanie where IdBadanie=' " + this.odwolajBadanieBox.Text + "'; ";
                cmd.Connection = con;

                try
                {
                    con.Open();
                    SqlDataReader da = cmd.ExecuteReader();
                    MessageBox.Show("Badanie odwołane poprawnie"); 
                    while (da.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                odwolajBadanieBox.Text = "";
                GetBadanieList();
            }
        }

    }
}
