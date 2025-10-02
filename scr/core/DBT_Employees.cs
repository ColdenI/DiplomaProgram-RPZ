using Microsoft.Data.SqlClient;

namespace DiplomaProgram_RPZ.scr.core
{
    public class DBT_Employees
    {
        public int EmployeeID;
        public string FullName;
        public string? Position;
        public DateTime? HireDate;
        public int DepartmentID;
        public int RoleID;


        public static List<DBT_Employees> GetAll()
        {
            var objs = new List<DBT_Employees>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Employees";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Employees();

                                obj.EmployeeID = reader.GetInt32(0);
                                obj.FullName = reader.GetString(1);
                                if (reader.IsDBNull(2)) obj.Position = string.Empty;
                                else obj.Position = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.HireDate = null;
                                else obj.HireDate = DateTime.Parse(reader.GetValue(3).ToString());
                                obj.DepartmentID = reader.GetInt32(4);
                                obj.RoleID = reader.GetInt32(5);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }
        public static DBT_Employees GetById(int id)
        {
            var obj = new DBT_Employees();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Employees WHERE EmployeeID = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.EmployeeID = reader.GetInt32(0);
                                obj.FullName = reader.GetString(1);
                                if (reader.IsDBNull(2)) obj.Position = string.Empty;
                                else obj.Position = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.HireDate = null;
                                else obj.HireDate = DateTime.Parse(reader.GetValue(3).ToString());
                                obj.DepartmentID = reader.GetInt32(4);
                                obj.RoleID = reader.GetInt32(5);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Employees obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Employees VALUES (@FullName, @Position, @HireDate, @DepartmentID, @RoleID);";
                        query.Parameters.AddWithValue("@FullName", obj.FullName);
                        query.Parameters.AddWithValue("@Position", obj.Position);
                        query.Parameters.AddWithValue("@HireDate", obj.HireDate);
                        query.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
                        query.Parameters.AddWithValue("@RoleID", obj.RoleID);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            int _id = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT MAX(EmployeeID) FROM Employees;";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _id = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch { return -1; }

            return _id;
        }

        public static int Edit(DBT_Employees obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Employees SET FullName = @FullName, Position = @Position, HireDate = @HireDate, DepartmentID = @DepartmentID, RoleID = @RoleID WHERE EmployeeID = @id;";
                        query.Parameters.AddWithValue("@FullName", obj.FullName);
                        query.Parameters.AddWithValue("@Position", obj.Position);
                        query.Parameters.AddWithValue("@HireDate", obj.HireDate);
                        query.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
                        query.Parameters.AddWithValue("@RoleID", obj.RoleID);
                        query.Parameters.AddWithValue("@id", obj.EmployeeID);
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
                        query.CommandText = "DELETE FROM Employees WHERE EmployeeID = @id;";
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
