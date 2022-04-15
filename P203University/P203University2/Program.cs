using System;
using System.Data.SqlClient;

namespace P203University2
{
    internal class Program
    {
        static string _conStr = @"Server=DESKTOP-BU4GQ1K\SQLEXPRESS;Database=P203University;Trusted_Connection=TRUE";
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(_conStr);

            string ans;

            bool check = true;
            do
            {
                Console.WriteLine("1. Group elave et");
                Console.WriteLine("2. Grouplara bax");
                Console.WriteLine("0. Programdan cix");


                ans = Console.ReadLine();

                switch (ans)
                {
                    case "1":
                        InsertGroup();
                        break;
                    case "2":
                        ShowGroups();
                        break;
                    case "0":
                        check = false;
                        break;
                    default:
                        Console.WriteLine("Dogru secim edin!");
                        break;
                }

            } while (check);
        }

        static void InsertGroup()
        {
            //@"Server=DESKTOP-BU4GQ1K\SQLEXPRESS;Database=P203University;Trusted_Connection=TRUE";

            Console.WriteLine("No dyerini daxil edin:");
            string no = Console.ReadLine();

            Console.WriteLine("StudentLimit deyerini daxil edin:");
            string stdLimit = Console.ReadLine();

            using(SqlConnection con = new SqlConnection(_conStr))
            {
                con.Open();
                string query = $"INSERT INTO Groups (No,StudentLimit) VALUES ('{no}',{stdLimit})";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();
            }
        }

        static void ShowGroups()
        {
            using(SqlConnection con = new SqlConnection(_conStr))
            {
                con.Open();
                string query = "SELECT * FROM Groups";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine($"{dr["Id"]} - {dr.GetString(1)} - {dr.GetInt32(2)}");
                }

                dr.Close();
            }
        }

    }
}
