
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Data.SqlClient;
using Dapper;
using Projekt_P.widoki.Główne;

namespace Projekt_P
{
    /// <summary>
    /// Logika interakcji dla klasy PacjenciWindow.xaml
    /// </summary>
    public partial class PacjenciWindow : Window
    {
        /// <summary>
        /// Konstruktor klasy PacjenciWindow
        /// </summary>
        /// <remarks>
        /// Wywołuje metodę GetPacjenciList() 
        /// </remarks>
        public PacjenciWindow()
        {
            InitializeComponent();
            GetPacjenciList();

        }



        /// <summary>
        /// Metoda pobierająca dane z tabeli Pacjenci i umieszczająca je w GridPacjent.
        /// </summary>
        private void GetPacjenciList()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Pacjent";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Pacjent");
            da.Fill(dt);
            GridPacjent.ItemsSource = dt.DefaultView;
        }




        ///<summary>
        ///Metoda otwierająca interfejs MainWindow po kliknięciu na Wroc_Click.
        ///</summary>
        private void Wroc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }




        ///<summary>
        ///Metoda otwierająca interfejs AddPacjentWindow.
        ///</summary>
        ///<param name="sender">	obiekt wywołujący zdarzenie </param>
        ///<param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void DodajPacjent_Click(object sender, RoutedEventArgs e)
        {
            AddPacjentWindow objAddPacjentWindow = new AddPacjentWindow();
            this.Visibility = Visibility.Hidden;
            objAddPacjentWindow.Show();

        }





        /// <summary>
        /// Metoda usuwająca z tabeli Pacjent rekorud o id wprowadzonym w usunBox. 
        /// </summary>
        /// <remarks>
        /// Metoda sprawdza czy w usunBox wprowadzone jest id.
        /// Po usunięciu rekordu wywołuje metodę GetPacjenciList </remarks>
        ///<param name="sender"> obiekt wywołujący zdarzenie </param>
        ///<param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void UsunPacjenta_Click(object sender, RoutedEventArgs e)
        {
            if (usunBox.Text == "")
            {
                MessageBox.Show("Wprowadź id pacjenta, którego chcesz usunąć z bazy");
                usunBox.Focus();
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from Pacjent where IdPacjenta=' " + this.usunBox.Text + "'; ";
                cmd.Connection = con;

                try
                {
                    con.Open();
                    SqlDataReader da = cmd.ExecuteReader();
                    MessageBox.Show("Usunięto pacjenta z bazy");
                    while (da.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                usunBox.Text = "";
                GetPacjenciList();
            }
        }

    }
}
    


