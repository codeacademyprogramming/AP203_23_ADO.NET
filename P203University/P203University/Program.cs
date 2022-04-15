using System;
using System.Data.SqlClient;

namespace P203University
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=DESKTOP-BU4GQ1K\SQLEXPRESS;Database=P203University;Trusted_Connection=TRUE";

            SqlConnection con = new SqlConnection(connectionString);

            string no = Console.ReadLine();
            string stdLimit = Console.ReadLine();
            con.Open();

            string query = $"INSERT INTO Groups (No,StudentLimit) VALUES('{no}',{stdLimit})";

            SqlCommand cmd = new SqlCommand(query, con);

            int result = cmd.ExecuteNonQuery();

            Console.WriteLine(result+ "rows affected");

            query = "SELECT * FROM Groups";

            cmd = new SqlCommand(query, con);

            SqlDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("Groups:");

            while (dr.Read())
            {
                Console.WriteLine($"Id: {dr["Id"]} - No: {dr.GetString(1)} - StdLimit: {dr.GetInt32(2)}");
            }
            dr.Close();

            query = "DELETE FROM Groups WHERE Id=1102";

            cmd = new SqlCommand(query, con);

            result = cmd.ExecuteNonQuery();

            Console.WriteLine(result+ " sayda data silindi!");

            con.Close();
        }
    }
}
