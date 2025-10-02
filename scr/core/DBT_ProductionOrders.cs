using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProgram_RPZ.scr.core
{
    public class DBT_ProductionOrders
    {
        public int OrderID;
        public int ProductID;
        public DateTime OrderDate;
        public DateTime? PlannedCompletionDate;
        public DateTime? ActualCompletionDate;
        public int Quantity;
        public string Status;


        public static List<DBT_ProductionOrders> GetAll()
        {
            var objs = new List<DBT_ProductionOrders>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM ProductionOrders";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_ProductionOrders();

                                obj.OrderID = reader.GetInt32(0);
                                obj.ProductID = reader.GetInt32(1);
                                obj.OrderDate = DateTime.Parse(reader.GetValue(2).ToString());
                                if (reader.IsDBNull(3)) obj.PlannedCompletionDate = null;
                                else obj.PlannedCompletionDate = DateTime.Parse(reader.GetValue(3).ToString());
                                if (reader.IsDBNull(4)) obj.ActualCompletionDate = null;
                                else obj.ActualCompletionDate = DateTime.Parse(reader.GetValue(4).ToString());
                                obj.Quantity = reader.GetInt32(5);
                                obj.Status = reader.GetString(6);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }
        public static DBT_ProductionOrders GetById(int id)
        {
            var obj = new DBT_ProductionOrders();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM ProductionOrders WHERE OrderID = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.OrderID = reader.GetInt32(0);
                                obj.ProductID = reader.GetInt32(1);
                                obj.OrderDate = DateTime.Parse(reader.GetValue(2).ToString());
                                if (reader.IsDBNull(3)) obj.PlannedCompletionDate = null;
                                else obj.PlannedCompletionDate = DateTime.Parse(reader.GetValue(3).ToString());
                                if (reader.IsDBNull(4)) obj.ActualCompletionDate = null;
                                else obj.ActualCompletionDate = DateTime.Parse(reader.GetValue(4).ToString());
                                obj.Quantity = reader.GetInt32(5);
                                obj.Status = reader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_ProductionOrders obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO ProductionOrders VALUES (@ProductID, @OrderDate, @PlannedCompletionDate, @ActualCompletionDate, @Quantity, @Status);";
                        query.Parameters.AddWithValue("@ProductID", obj.ProductID);
                        query.Parameters.AddWithValue("@OrderDate", obj.OrderDate);
                        query.Parameters.AddWithValue("@PlannedCompletionDate", obj.PlannedCompletionDate);
                        query.Parameters.AddWithValue("@ActualCompletionDate", obj.ActualCompletionDate);
                        query.Parameters.AddWithValue("@Quantity", obj.Quantity);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

        public static int Edit(DBT_ProductionOrders obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE ProductionOrders SET ProductID = @ProductID, OrderDate = @OrderDate, PlannedCompletionDate = @PlannedCompletionDate, ActualCompletionDate = @ActualCompletionDate, Quantity = @Quantity, Status = @Status WHERE OrderID = @id;";
                        query.Parameters.AddWithValue("@ProductID", obj.ProductID);
                        query.Parameters.AddWithValue("@OrderDate", obj.OrderDate);
                        query.Parameters.AddWithValue("@PlannedCompletionDate", obj.PlannedCompletionDate);
                        query.Parameters.AddWithValue("@ActualCompletionDate", obj.ActualCompletionDate);
                        query.Parameters.AddWithValue("@Quantity", obj.Quantity);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        query.Parameters.AddWithValue("@id", obj.OrderID);
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
                        query.CommandText = "DELETE FROM ProductionOrders WHERE OrderID = @id;";
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
