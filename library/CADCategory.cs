using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace library
{
    public class CADCategory
    {
        private string constring;

        public CADCategory()
        {
            constring = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
        }

        public bool Read(ENCategory en)
        {
            SqlConnection conn = new SqlConnection(constring);
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Categories] WHERE id = @id", conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = en.Id;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    en.Id = Convert.ToInt32(reader["id"]);
                    en.Name = reader["name"].ToString();
                    reader.Close();
                    conn.Close();
                    return true;
                }

                reader.Close();
                conn.Close();
                return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Category operation has failed. Error: {0}", ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public List<ENCategory> ReadAll()
        {
            List<ENCategory> categories = new List<ENCategory>();
            SqlConnection conn = new SqlConnection(constring);

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Categories] ORDER BY id", conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ENCategory category = new ENCategory();
                    category.Id = Convert.ToInt32(reader["id"]);
                    category.Name = reader["name"].ToString();
                    categories.Add(category);
                }

                reader.Close();
                conn.Close();
                return categories;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Category operation has failed. Error: {0}", ex.Message);
                return categories;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}