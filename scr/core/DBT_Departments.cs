using Microsoft.Data.SqlClient;

namespace DiplomaProgram_RPZ.scr.core
{
    public class DBT_Departments
    {
        public int DepartmentID;
        public string DepartmentName;
        public string? Location;


        public static List<DBT_Departments> GetAll()
        {
            var objs = new List<DBT_Departments>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Departments";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Departments();

                                obj.DepartmentID = reader.GetInt32(0);
                                obj.DepartmentName = reader.GetString(1);
                                if (reader.IsDBNull(2)) obj.Location = string.Empty;
                                else obj.Location = reader.GetString(2);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }
        public static DBT_Departments GetById(int id)
        {
            var obj = new DBT_Departments();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Departments WHERE DepartmentID = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.DepartmentID = reader.GetInt32(0);
                                obj.DepartmentName = reader.GetString(1);
                                if (reader.IsDBNull(2)) obj.Location = string.Empty;
                                else obj.Location = reader.GetString(2);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Departments obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Departments VALUES (@DepartmentName, @Location);";
                        query.Parameters.AddWithValue("@DepartmentName", obj.DepartmentName);
                        query.Parameters.AddWithValue("@Location", obj.Location);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

        public static int Edit(DBT_Departments obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Departments SET DepartmentName = @DepartmentName, Location = @Location WHERE DepartmentID = @id;";
                        query.Parameters.AddWithValue("@DepartmentName", obj.DepartmentName);
                        query.Parameters.AddWithValue("@Location", obj.Location);
                        query.Parameters.AddWithValue("@id", obj.DepartmentID);
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
                        query.CommandText = "DELETE FROM Departments WHERE DepartmentID = @id;";
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
