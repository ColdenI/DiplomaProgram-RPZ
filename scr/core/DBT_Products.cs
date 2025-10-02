using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProgram_RPZ.scr.core
{
    public class DBT_Products
    {
        public int ProductID;
        public string ProductName;
        public string ProductCode;
        public DateTime? ProductionStartDate;
        public string? Description;


        public static List<DBT_Products> GetAll()
        {
            var objs = new List<DBT_Products>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Products";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Products();

                                obj.ProductID = reader.GetInt32(0);
                                obj.ProductName = reader.GetString(1);
                                obj.ProductCode = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.ProductionStartDate = null;
                                else obj.ProductionStartDate = DateTime.Parse(reader.GetValue(3).ToString());
                                if (reader.IsDBNull(4)) obj.Description = string.Empty;
                                else obj.Description = reader.GetString(4);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }
        public static DBT_Products GetById(int id)
        {
            var obj = new DBT_Products();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Products WHERE ProductID = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ProductID = reader.GetInt32(0);
                                obj.ProductName = reader.GetString(1);
                                obj.ProductCode = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.ProductionStartDate = null;
                                else obj.ProductionStartDate = DateTime.Parse(reader.GetValue(3).ToString());
                                if (reader.IsDBNull(4)) obj.Description = string.Empty;
                                else obj.Description = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Products obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Products VALUES (@ProductName, @ProductCode, @ProductionStartDate, @Description);";
                        query.Parameters.AddWithValue("@ProductName", obj.ProductName);
                        query.Parameters.AddWithValue("@ProductCode", obj.ProductCode);
                        query.Parameters.AddWithValue("@ProductionStartDate", obj.ProductionStartDate);
                        query.Parameters.AddWithValue("@Description", obj.Description);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

        public static int Edit(DBT_Products obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Products SET ProductName = @ProductName, ProductCode = @ProductCode, ProductionStartDate = @ProductionStartDate, Description = @Description WHERE ProductID = @id;";
                        query.Parameters.AddWithValue("@ProductName", obj.ProductName);
                        query.Parameters.AddWithValue("@ProductCode", obj.ProductCode);
                        query.Parameters.AddWithValue("@ProductionStartDate", obj.ProductionStartDate);
                        query.Parameters.AddWithValue("@Description", obj.Description);
                        query.Parameters.AddWithValue("@id", obj.ProductID);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

        public static int Remove(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "DELETE FROM Products WHERE ProductID = @id;";
                        query.Parameters.AddWithValue("@id", id);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

    }
}
