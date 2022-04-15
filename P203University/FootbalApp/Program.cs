using FootbalApp.Data;
using FootbalApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FootbalApp
{
    internal class Program
    {
        static string _conString = @"Server=DESKTOP-BU4GQ1K\SQLEXPRESS;Database=Football;Trusted_Connection=TRUE";
        static void Main(string[] args)
        {
            StadionData stadionData = new StadionData();

            //Console.WriteLine("Axtardigin stadionun id deyeri:");
            //string idStr = Console.ReadLine();
            //int id;

            //while (!int.TryParse(idStr, out id))
            //{
            //    Console.WriteLine("Id deyerini dogru daxil edin!");
            //    Console.WriteLine("Axtardigin stadionun id deyeri:");
            //    idStr = Console.ReadLine();
            //}

            //Stadion std =  stadionData.GetById(id);

            //if (std != null)
            //{
            //    Console.WriteLine($"Name: {std.Name} - Price: {std.HourPrice}");
            //}
            //else
            //{
            //    Console.WriteLine($"{id} id deyerli Stadion yoxdur!");
            //}

            Stadion stadion = new Stadion()
            {
                Name = "STD3",
                HourPrice = 30,
                Capacity = 50
            };

            stadionData.Insert(stadion);
        }


        static void InserStadium()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                Console.WriteLine("Name:");
                string name = Console.ReadLine();

                Console.WriteLine("Price:");
                string price = Console.ReadLine();

                Console.WriteLine("Capacity:");
                string capacity = Console.ReadLine();

                string query = $"INSERT INTO Stadions (Name,HourPrice,Capacity) VALUES ('{name}',{price},{capacity})";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            
        }

        static void ShowStadions()
        {
            using(SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                string query = "SELECT * FROM Stadions";

                SqlCommand cmd = new SqlCommand(query, con);

                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine($"Id : {dr.GetInt32(0)} - Name: {dr.GetString(1)} - Price: {dr["HourPrice"]} - Capacity: {dr.GetInt32(3)}");
                    }
                }
            }
        }

        static List<Stadion> GetAllStadions()
        {
            List<Stadion> stadions = new List<Stadion>();

            using(SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                string query = "SELECT * FROM Stadions";

                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Stadion stadion = new Stadion
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            HourPrice = dr.GetDecimal(2),
                            Capacity = dr.GetInt32(3)
                        };

                        stadions.Add(stadion);
                    }
                }
                return stadions;
            }
        }
    }
}
