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
    /// Logika interakcji dla klasy AddBadanie.xaml
    /// </summary>
    public partial class AddBadanie : Window
    {
        DataSet ds = new DataSet();

        /// <summary>
        /// Konstruktor klasy AddBadanie
        /// </summary>
        public AddBadanie()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Metoda Submit_Click przesyłająca dane do bazy po kliknięciu Submit_Click 
        /// </summary>
        /// <remarks>
        /// Metoda zawiera walidacje danych. Obowiązkowe jest wypełnienie pola nazwy badanie imieBox oraz ceny badania cenaBox.
        /// Sprawdzenie czy w bazie nie występuję już dane badanie
        /// </remarks>
        /// <param name="sender">	obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (imieBox.Text == "")
            {
                MessageBox.Show("Wprowadź nazwę badania");
                imieBox.Focus();
            }

            else if (cenaBox.Text == "")
            {
                MessageBox.Show("Wprowadź cenę badania");
                cenaBox.Focus();

            }

            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["masterDB"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = ("Select * from [Lab] where Nazwa='" + imieBox.Text + "'");
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                int i = ds.Tables[0].Rows.Count;
                if (i > 0)
                {
                    MessageBox.Show("W bazie widnieje już badanie '" + imieBox.Text + "'");
                    ds.Clear();
                }

                else

                {

                  

                    cmd.CommandText = "Insert into [Lab](Nazwa, Cena) values (@Nazwa, @Cena)";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Nazwa", imieBox.Text);
                    cmd.Parameters.AddWithValue("@Cena", cenaBox.Text);


                    cmd.Connection = con;
                    int a = cmd.ExecuteNonQuery();

                    if (a == 1)
                    {
                        MessageBox.Show("Dane dodane prawidłowo");

                    }

                    imieBox.Text = "";
                    cenaBox.Text = "";


                }
            }
        }


        /// <summary>
        /// Metoda otwierająca intefejs LabWindow po kliknięciu Wroc_Click. 
        /// </summary>
        /// <param name="sender"> obiekt wywołujący zdarzenie </param>
        /// <param name="e"> pparametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Wroc_Click(object sender, RoutedEventArgs e)
        {
            LabWindow objLabWindow = new LabWindow();
            this.Visibility = Visibility.Hidden;
            objLabWindow.Show();
        }
    }
}
