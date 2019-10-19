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
    /// Logika interakcji dla klasy AddLekarzWindow.xaml
    /// </summary>
    public partial class AddLekarzWindow : Window
    {


        /// <summary>
        /// Konstruktor klasy AddLekarzWindow
        /// </summary>
        public AddLekarzWindow()
        {
            InitializeComponent();
        }




        ///<summary>
        /// Metoda otwierająca interfejs LekarzWindow po kliknięciu Wroc_Click. 
        ///</summary>
        private void Wroc_Click(object sender, RoutedEventArgs e)
        {
            LekarzeWindow objLekarzWindow = new LekarzeWindow();
            this.Visibility = Visibility.Hidden;
            objLekarzWindow.Show();
        }




        ///<summary>
        /// Metoda przesyłająca dane do bazy po kliknięciu Submit_Click.
        ///</summary>>
        ///<remarks>
        ///Metoda sprawdza czy wszystkie pola są wypełnione przed dodaniem.
        ///Po dodaniu textboxy zostają wyczyszczone. 
        ///</remarks>
        /// <param name="sender"> obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (imieBox.Text == "")
            {
                MessageBox.Show("Wprowadź imie lekarza");
                imieBox.Focus();
            }
            else if (nazwiskoBox.Text == "")
            {
                MessageBox.Show("Wprowadź nazwisko lekarza");
                nazwiskoBox.Focus();
            }
            else if (specBox.Text == "")
            {
                MessageBox.Show("Wprowadź specjalizację");
                specBox.Focus();
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Insert into [Doctor](Imie, Nazwisko, Specjalizacja) values (@Imie, @Nazwisko, @Specjalizacja)";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Imie", imieBox.Text);
                cmd.Parameters.AddWithValue("@Nazwisko", nazwiskoBox.Text);
                cmd.Parameters.AddWithValue("@Specjalizacja", specBox.Text);

                cmd.Connection = con;
                int a = cmd.ExecuteNonQuery();

                if (a == 1)
                {
                    MessageBox.Show("Dane dodane prawidłowo");

                }

                imieBox.Text = "";
                nazwiskoBox.Text = "";
                specBox.Text = "";

            }
        }
    }
}
