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
using System.Windows.Shapes;

namespace Projekt_P.widoki.Główne
{
    /// <summary>
    /// Logika interakcji dla klasy LabSkierWindow.xaml
    /// </summary>
    public partial class LabSkierWindow : Window
    {  
        
        
        /// <summary>
    /// Konstruktor klasy LabSkierWrindow
    /// </summary>
        public LabSkierWindow()
        {
            InitializeComponent();
        }




        ///<summary>
        ///Metoda otwierająca interfejs LabWindow po kliknięciu Wroc_Click
        ///</summary>
        /// <param name="sender">	obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        private void Wroc_Click(object sender, RoutedEventArgs e)
        {
            LabWindow objLabWindow = new LabWindow();
            this.Visibility = Visibility.Hidden;
            objLabWindow.Show();
        }




        /// <summary>
        /// Metoda otwierająca odpowiedni interfejs w zależności od zaznaczonego checkboxa.
        /// </summary>
        /// <remarks>
        /// Metoda sprawdza czy zaznaczony jest checkbox oraz czy nie są zaznaczone oba.
        /// Otwiera okno ZlecBadanieTak dla zaznaczonego checkboxu Tak lub ZlecBadanieWindow dla zaznaczonego checkboxa Nie.
        /// </remarks>
        /// <param name="sender">	obiekt wywołujący zdarzenie </param>
        /// <param name="e"> parametr przysyłający dane do zainicjalizowania zdarzeń w metodzie </param>
        
        private void Umow_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxNie.IsChecked != true && checkBoxTak.IsChecked != true)
            {
                MessageBox.Show("Zaznacz opcję");

            }
            else if (checkBoxTak.IsChecked == true && checkBoxNie.IsChecked == true)
            {
                MessageBox.Show("Wybierz tylko jedną opcję");
            }
            else 
            {
                if (checkBoxTak.IsChecked == true)
                {
                    ZlecBadanieTak objZlecBadanieTak = new ZlecBadanieTak();
                    this.Visibility = Visibility.Hidden;
                    objZlecBadanieTak.Show();
                }

                else if (checkBoxNie.IsChecked == true)
                {
                    ZlecBadanieWindow objZlecBadanieWindow = new ZlecBadanieWindow();
                    this.Visibility = Visibility.Hidden;
                    objZlecBadanieWindow.Show();
                }
               
            }
        }

       
    }
}
