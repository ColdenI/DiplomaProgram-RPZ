using Microsoft.Data.SqlClient;

namespace DiplomaProgram_RPZ.scr.core
{
    public class DBT_Auth
    {
        public int AuthID;
        public string Login;
        public string PasswordHash;
        public int EmployeeID;
        public DateTime? CreatedAt;


        public static List<DBT_Auth> GetAll()
        {
            var objs = new List<DBT_Auth>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Auth";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Auth();

                                obj.AuthID = reader.GetInt32(0);
                                obj.Login = reader.GetString(1);
                                obj.PasswordHash = reader.GetString(2);
                                obj.EmployeeID = reader.GetInt32(3);
                                if (reader.IsDBNull(4)) obj.CreatedAt = null;
                                else obj.CreatedAt = DateTime.Parse(reader.GetValue(4).ToString());

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }
        public static DBT_Auth GetByEmployeeID(int id)
        {
            var obj = new DBT_Auth();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Auth WHERE EmployeeID = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.AuthID = reader.GetInt32(0);
                                obj.Login = reader.GetString(1);
                                obj.PasswordHash = reader.GetString(2);
                                obj.EmployeeID = reader.GetInt32(3);
                                if (reader.IsDBNull(4)) obj.CreatedAt = null;
                                else obj.CreatedAt = DateTime.Parse(reader.GetValue(4).ToString());
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Auth obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Auth VALUES (@Login, @PasswordHash, @EmployeeID, @CreatedAt);";
                        query.Parameters.AddWithValue("@Login", obj.Login);
                        query.Parameters.AddWithValue("@PasswordHash", obj.PasswordHash);
                        query.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                        query.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

        public static int Edit(DBT_Auth obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Auth SET Login = @Login, PasswordHash = @PasswordHash, EmployeeID = @EmployeeID, CreatedAt = @CreatedAt WHERE AuthID = @id;";
                        query.Parameters.AddWithValue("@Login", obj.Login);
                        query.Parameters.AddWithValue("@PasswordHash", obj.PasswordHash);
                        query.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                        query.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);
                        query.Parameters.AddWithValue("@id", obj.AuthID);
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
                        query.CommandText = "DELETE FROM Auth WHERE AuthID = @id;";
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
