using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace library
{
    public class CADProduct
    {
        private string constring;

        public CADProduct()
        {
            try
            {
                constring = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
            }
            catch (Exception ex)
            {
                // Fallback en caso de error
                constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
                Console.WriteLine("Error getting connection string: " + ex.Message);
            }
        }

        public bool Create(ENProduct en)
        {
            SqlConnection conn = new SqlConnection(constring);
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Products] (name, code, amount, price, category, creationDate) " +
                                                "VALUES (@name, @code, @amount, @price, @category, @creationDate)", conn);

                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = en.Name;
                cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = en.Code;
                cmd.Parameters.Add("@amount", SqlDbType.Int).Value = en.Amount;
                cmd.Parameters.Add("@price", SqlDbType.Float).Value = en.Price;
                cmd.Parameters.Add("@category", SqlDbType.Int).Value = en.Category;
                cmd.Parameters.Add("@creationDate", SqlDbType.DateTime).Value = en.CreationDate;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
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

        public bool Update(ENProduct en)
        {
            SqlConnection conn = new SqlConnection(constring);
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Products] SET name = @name, amount = @amount, " +
                                                "price = @price, category = @category, creationDate = @creationDate " +
                                                "WHERE code = @code", conn);

                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = en.Name;
                cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = en.Code;
                cmd.Parameters.Add("@amount", SqlDbType.Int).Value = en.Amount;
                cmd.Parameters.Add("@price", SqlDbType.Float).Value = en.Price;
                cmd.Parameters.Add("@category", SqlDbType.Int).Value = en.Category;
                cmd.Parameters.Add("@creationDate", SqlDbType.DateTime).Value = en.CreationDate;

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
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

        public bool Delete(ENProduct en)
        {
            SqlConnection conn = new SqlConnection(constring);
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Products] WHERE code = @code", conn);
                cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = en.Code;

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
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

        public bool Read(ENProduct en)
        {
            SqlConnection conn = new SqlConnection(constring);
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Products] WHERE code = @code", conn);
                cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = en.Code;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    en.Name = reader["name"].ToString();
                    en.Code = reader["code"].ToString();
                    en.Amount = Convert.ToInt32(reader["amount"]);
                    en.Price = Convert.ToSingle(reader["price"]);
                    en.Category = Convert.ToInt32(reader["category"]);
                    en.CreationDate = Convert.ToDateTime(reader["creationDate"]);
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
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
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

        public bool ReadFirst(ENProduct en)
        {
            SqlConnection conn = new SqlConnection(constring);
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM [dbo].[Products] ORDER BY id", conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    en.Name = reader["name"].ToString();
                    en.Code = reader["code"].ToString();
                    en.Amount = Convert.ToInt32(reader["amount"]);
                    en.Price = Convert.ToSingle(reader["price"]);
                    en.Category = Convert.ToInt32(reader["category"]);
                    en.CreationDate = Convert.ToDateTime(reader["creationDate"]);
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
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
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

        public bool ReadNext(ENProduct en)
        {
            SqlConnection conn = new SqlConnection(constring);
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM [dbo].[Products] WHERE id > " +
                                               "(SELECT id FROM [dbo].[Products] WHERE code = @code) ORDER BY id", conn);
                cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = en.Code;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    en.Name = reader["name"].ToString();
                    en.Code = reader["code"].ToString();
                    en.Amount = Convert.ToInt32(reader["amount"]);
                    en.Price = Convert.ToSingle(reader["price"]);
                    en.Category = Convert.ToInt32(reader["category"]);
                    en.CreationDate = Convert.ToDateTime(reader["creationDate"]);
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
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
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

        public bool ReadPrev(ENProduct en)
        {
            SqlConnection conn = new SqlConnection(constring);
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM [dbo].[Products] WHERE id < " +
                                               "(SELECT id FROM [dbo].[Products] WHERE code = @code) ORDER BY id DESC", conn);
                cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = en.Code;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    en.Name = reader["name"].ToString();
                    en.Code = reader["code"].ToString();
                    en.Amount = Convert.ToInt32(reader["amount"]);
                    en.Price = Convert.ToSingle(reader["price"]);
                    en.Category = Convert.ToInt32(reader["category"]);
                    en.CreationDate = Convert.ToDateTime(reader["creationDate"]);
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
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
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
    }
}