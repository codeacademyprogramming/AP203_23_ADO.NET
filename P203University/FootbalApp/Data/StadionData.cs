using FootbalApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FootbalApp.Data
{
    internal class StadionData
    {
        public int Insert(Stadion stadion)
        {
            using (SqlConnection con = new SqlConnection(Sql.ConnectionString))
            {
                con.Open();

                string query = $"INSERT INTO Stadions (Name,HourPrice,Capacity) VALUES (@name,@price,@capacity)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("name", stadion.Name);
                cmd.Parameters.AddWithValue("price", stadion.HourPrice);
                cmd.Parameters.AddWithValue("capacity", stadion.Capacity);


                return cmd.ExecuteNonQuery();
            }
        }

        public List<Stadion> GetAll()
        {
            List<Stadion> stadions = new List<Stadion>();
            using (SqlConnection con = new SqlConnection(Sql.ConnectionString))
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
            }
            return stadions;
        }

        public Stadion GetById(string id)
        {
            Stadion std = new Stadion();
            using (SqlConnection con = new SqlConnection(Sql.ConnectionString))
            {
                con.Open();

                string query = "SELECT * FROM Stadions WHERE Id=@id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            std.Id = dr.GetInt32(0);
                            std.Name = dr.GetString(1);
                            std.HourPrice = dr.GetDecimal(2);
                            std.Capacity = dr.GetInt32(3);
                        }
                    }
                    else return null;
                   
                }
            }
            return std;
        }

        public Stadion GetByName(string name)
        {
            Stadion std = new Stadion();
            using (SqlConnection con = new SqlConnection(Sql.ConnectionString))
            {
                con.Open();

                string query = $"SELECT * FROM Stadions WHERE Name='{name}'";

                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            std.Id = dr.GetInt32(0);
                            std.Name = dr.GetString(1);
                            std.HourPrice = dr.GetDecimal(2);
                            std.Capacity = dr.GetInt32(3);
                        }
                    }
                    else return null;

                }
            }
            return std;
        }

        public bool IsExistById(int id)
        {
            using (SqlConnection con = new SqlConnection(Sql.ConnectionString))
            {
                con.Open();

                string query = "SELECT * FROM Stadions WHERE Id=" + id;

                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    return dr.HasRows;
                }
            }
        }
    }
}
