using Projekt_P.widoki;
using Projekt_P.widoki.Główne;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_P
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ///<sumary>
        ///Konstruktor klasy MainWindow
        ///</sumary>
        public MainWindow()
        {
            InitializeComponent();
        }

        ///<summary>
        /// Metoda Pacjenci_Click otwierająca interfejs PacjenciWindow 
        /// po kliknięciu Pacjenci_Click
        ///</summary>
       
        private void Pacjenci_Click(object sender, RoutedEventArgs e)
        {
            PacjenciWindow objPacjenciWindow = new PacjenciWindow();
            this.Visibility = Visibility.Hidden;
            objPacjenciWindow.Show();
        }


        ///<summary>
        /// Metoda Lekarze_Click otwierająca interfejs LekarzeWindow po kliknięciu Lekarze_Click
        ///</summary>
        private void Lekarze_Click(object sender, RoutedEventArgs e)
        {
            LekarzeWindow objLekarzeWindow = new LekarzeWindow();
            this.Visibility = Visibility.Hidden;
            objLekarzeWindow.Show();

        }
        ///<summary>
        /// Metoda Laboratorium_Click otwierająca interfejs LaboratoriumWindow
        /// po kliknięciu Laboratorium_Click
        ///</summary>
        private void Laboratorium_Click(object sender, RoutedEventArgs e)
        {
            LabWindow objLabWindow = new LabWindow();
            this.Visibility = Visibility.Hidden;
            objLabWindow.Show();
        }

        ///<summary>
        /// Metoda Wizyty_Click otwierająca intefejs WizytyWindow
        /// po kliknięciu Wizyty_Click
        ///</summary>
        private void Wizyty_Click(object sender, RoutedEventArgs e)
        {
            WizytyWindow objWizytyWindow = new WizytyWindow();
            this.Visibility = Visibility.Hidden;
            objWizytyWindow.Show();
        }
    }
}
