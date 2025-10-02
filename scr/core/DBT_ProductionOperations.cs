using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProgram_RPZ.scr.core
{
    public class DBT_ProductionOperations
    {
        public int OperationID;
        public int OrderID;
        public string OperationName;
        public int EmployeeID;
        public int DepartmentID;
        public DateTime? PlannedStartDate;
        public DateTime? ActualStartDate;
        public DateTime? ActualEndDate;
        public string Status;


        public static List<DBT_ProductionOperations> GetAll()
        {
            var objs = new List<DBT_ProductionOperations>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM ProductionOperations";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_ProductionOperations();

                                obj.OperationID = reader.GetInt32(0);
                                obj.OrderID = reader.GetInt32(1);
                                obj.OperationName = reader.GetString(2);
                                obj.EmployeeID = reader.GetInt32(3);
                                obj.DepartmentID = reader.GetInt32(4);
                                if (reader.IsDBNull(5)) obj.PlannedStartDate = null;
                                else obj.PlannedStartDate = DateTime.Parse(reader.GetValue(5).ToString());
                                if (reader.IsDBNull(6)) obj.ActualStartDate = null;
                                else obj.ActualStartDate = DateTime.Parse(reader.GetValue(6).ToString());
                                if (reader.IsDBNull(7)) obj.ActualEndDate = null;
                                else obj.ActualEndDate = DateTime.Parse(reader.GetValue(7).ToString());
                                obj.Status = reader.GetString(8);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }
        public static DBT_ProductionOperations GetById(int id)
        {
            var obj = new DBT_ProductionOperations();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM ProductionOperations WHERE OperationID = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.OperationID = reader.GetInt32(0);
                                obj.OrderID = reader.GetInt32(1);
                                obj.OperationName = reader.GetString(2);
                                obj.EmployeeID = reader.GetInt32(3);
                                obj.DepartmentID = reader.GetInt32(4);
                                if (reader.IsDBNull(5)) obj.PlannedStartDate = null;
                                else obj.PlannedStartDate = DateTime.Parse(reader.GetValue(5).ToString());
                                if (reader.IsDBNull(6)) obj.ActualStartDate = null;
                                else obj.ActualStartDate = DateTime.Parse(reader.GetValue(6).ToString());
                                if (reader.IsDBNull(7)) obj.ActualEndDate = null;
                                else obj.ActualEndDate = DateTime.Parse(reader.GetValue(7).ToString());
                                obj.Status = reader.GetString(8);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_ProductionOperations obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO ProductionOperations VALUES (@OrderID, @OperationName, @EmployeeID, @DepartmentID, @PlannedStartDate, @ActualStartDate, @ActualEndDate, @Status);";
                        query.Parameters.AddWithValue("@OrderID", obj.OrderID);
                        query.Parameters.AddWithValue("@OperationName", obj.OperationName);
                        query.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                        query.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
                        query.Parameters.AddWithValue("@PlannedStartDate", obj.PlannedStartDate);
                        query.Parameters.AddWithValue("@ActualStartDate", obj.ActualStartDate);
                        query.Parameters.AddWithValue("@ActualEndDate", obj.ActualEndDate);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

        public static int Edit(DBT_ProductionOperations obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE ProductionOperations SET OrderID = @OrderID, OperationName = @OperationName, EmployeeID = @EmployeeID, DepartmentID = @DepartmentID, PlannedStartDate = @PlannedStartDate, ActualStartDate = @ActualStartDate, ActualEndDate = @ActualEndDate, Status = @Status WHERE OperationID = @id;";
                        query.Parameters.AddWithValue("@OrderID", obj.OrderID);
                        query.Parameters.AddWithValue("@OperationName", obj.OperationName);
                        query.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                        query.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
                        query.Parameters.AddWithValue("@PlannedStartDate", obj.PlannedStartDate);
                        query.Parameters.AddWithValue("@ActualStartDate", obj.ActualStartDate);
                        query.Parameters.AddWithValue("@ActualEndDate", obj.ActualEndDate);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        query.Parameters.AddWithValue("@id", obj.OperationID);
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
                        query.CommandText = "DELETE FROM ProductionOperations WHERE OperationID = @id;";
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
