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
    /// Logika interakcji dla klasy WizytyWindow.xaml
    /// </summary>
    public partial class WizytyWindow : Window
    {


        /// <summary>
        /// Konsktruktor klasy WizytyWindow
        /// </summary>
        /// <remarks>
        /// Wywołuję metodę GetWizytyList()
        /// </remarks>
        public WizytyWindow()
        {
            InitializeComponent();
            GetWizytyList();
            
        }

      
        ///<summary>
        ///Metoda pobierające dane z tabeli Visit do GridWizyta.
        ///</summary>
        private void GetWizytyList()
        {
            SqlConnection con = new SqlConnection();

        
            con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Visit";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Visit");
            da.Fill(dt);
            GridWizyta.ItemsSource = dt.DefaultView;
        }



        ///<summary>
        ///Metoda otwierająca interfejs MainWindow po kliknięciu Wroc_Click
        ///</summary>
        ///<param name="sender"> obiekt wywołujący zdarzenie </param>
        ///<param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Wroc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }




        /// <summary>
        /// Metoda otwierająca interfejs UmowWizyteWindow po kliknięciu DodajWizyta_Click
        /// </summary>
        /// <param name="sender"> obiekt wywołujący zdarzenie </param>
        ///<param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void DodajWizyta_Click(object sender, RoutedEventArgs e)
        {
            UmowWizyteWindow objUmowWizyteWindow = new UmowWizyteWindow();
            this.Visibility = Visibility.Hidden;
            objUmowWizyteWindow.Show();

        }



        ///<summary>
        ///Metoda usuwająca z tabeli Visit rekord, którego id zostało wpisane w usunBox.
        ///</summary>
        ///<remarks>
        ///Informacja jeżeli usunBox jest pusty</remarks>
        /// <param name="sender"> obiekt wywołujący zdarzenie </param>
        ///<param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void UsunWizyta_Click(object sender, RoutedEventArgs e)
        {

            if (usunBox.Text == "")
            {
                MessageBox.Show("Wprowadź id wizyty, którą chcesz odwołać");
                usunBox.Focus();
            }

            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from Visit where IdWizyty=' " + this.usunBox.Text + "'; ";
                cmd.Connection = con;

                
                try
                {
                    con.Open();
                    SqlDataReader da = cmd.ExecuteReader();
                    MessageBox.Show("Wizyta odwołana"); 
                    while (da.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            usunBox.Text = "";
            GetWizytyList();
           

        }
          
        
        }
    }


