using Projekt_P.widoki.Główne;
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

namespace Projekt_P.widoki
{
    /// <summary>
    /// Logika interakcji dla klasy LekarzeWindow.xaml
    /// </summary>
    public partial class LekarzeWindow : Window
    {   


        /// <summary>
        /// Konstruktor klasy LekarzeWindow 
        /// </summary>
        /// <remarks>
        /// Wywołuje metodę GetLekarzeList()
        /// </remarks>
        public LekarzeWindow()
        {
            InitializeComponent();
            GetLekarzeList();
        }



        /// <summary>
        /// Metoda pobierająca dane z tabeli Doctor i wprowadzająca do GridLekarz.
        /// </summary>
        private void GetLekarzeList()
        {
            SqlConnection con = new SqlConnection();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Doctor";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Doctor");
            da.Fill(dt);
            GridLekarz.ItemsSource = dt.DefaultView;
        }



        ///<summary>
        ///Metoda otwierająca interfejs MainWindow po kliknięciu Wroc_Click
        /// </summary>
        /// <param name="sender">	obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param> <param name="e"></param>
        private void Wroc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }




        /// <summary>
        /// Metoda otwierająca interfejs AddLekarzWindow po kliknięciu DodajLekarz_Click
        /// </summary>
        /// <param name="sender">	obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void DodajLekarz_Click(object sender, RoutedEventArgs e)
        {
            AddLekarzWindow objAddLekarzWindow = new AddLekarzWindow();
            this.Visibility = Visibility.Hidden;
            objAddLekarzWindow.Show();
        }



        ///<summary>
        ///Metoda służąca usuwaniu rekordu z bazy Doctor o id wprowadzonych do usunBox
        ///</summary>
        ///<remarks>
        ///Metoda sprawdza czy w usunBox znajduje się tekst, jeżeli nie, informuje o tym.
        ///Po usunięciu rekordu wywołuje funkcję GetLekarzeList()
        ///</remarks>
        /// <param name="sender">	obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void UsunLekarz_Click(object sender, RoutedEventArgs e)
        {
            if (usunBox.Text == "")
            {
                MessageBox.Show("Wprowadź id lekarza, którego chcesz usunąć z bazy");
                usunBox.Focus();
            }
            else {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from Doctor where IdLekarza=' " + this.usunBox.Text + "'; ";
                cmd.Connection = con;

                try
                {
                    con.Open();
                    SqlDataReader da = cmd.ExecuteReader();
                    MessageBox.Show("Usunięto lekarza z bazy"); 
                    while (da.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                usunBox.Text = "";
                GetLekarzeList();
            }
        }
    }
}