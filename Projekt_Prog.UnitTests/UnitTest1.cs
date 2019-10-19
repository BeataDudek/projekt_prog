using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Projekt_P.widoki.Główne;


namespace Projekt_Prog.UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void AddPacjent_AddistniejącyPacjent_True()
        {
            string pesel = "62090612366";
            string zwrotka;


            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VDD1OQNQ;Initial Catalog=master;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Pesel FROM Pacjent WHERE Pesel='" + pesel + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            zwrotka = (string)cmd.ExecuteScalar();


            Assert.AreEqual(zwrotka, pesel);
        }







        [TestMethod]
        public void AddBadanie_AddistniejaceBadanie_True()
        {
            string nazwa = "Morfologia";
            string zwrotka;



            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VDD1OQNQ;Initial Catalog=master;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Nazwa FROM [Lab] WHERE Nazwa ='" + nazwa + "'", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            zwrotka = (string)cmd.ExecuteScalar();



            Assert.AreEqual(nazwa, zwrotka);
        }







        [TestMethod]

        public void Pesel_LiczbaZnakow_True()
        {
            string pesel = "96090711231";
            int a = pesel.Length;

            Assert.AreEqual(a, 11);
        }


        [TestMethod]

        public void LiczbaRekordow_True()
        {
            string data = "2019-10-22";
            string lekarz = "Katarzyna Bromka";
            int sprawdzenie;
            bool wynik;

            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VDD1OQNQ;Initial Catalog=master;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Visit WHERE Data ='" + data + "'AND Lekarz='" + lekarz + "'", con);
            cmd.CommandType = CommandType.Text;
            sprawdzenie = (int)cmd.ExecuteScalar();

            if (sprawdzenie <= 5)
            {
                wynik = true;
            }
            else wynik = false;

            Assert.IsTrue(wynik);

        }

        
    }
}


