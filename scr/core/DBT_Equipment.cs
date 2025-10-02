using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProgram_RPZ.scr.core
{
    public class DBT_Equipment
    {
        public int EquipmentID;
        public string EquipmentName;
        public string? SerialNumber;
        public DateTime? InstallationDate;
        public int DepartmentID;


        public static List<DBT_Equipment> GetAll()
        {
            var objs = new List<DBT_Equipment>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Equipment";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Equipment();

                                obj.EquipmentID = reader.GetInt32(0);
                                obj.EquipmentName = reader.GetString(1);
                                if (reader.IsDBNull(2)) obj.SerialNumber = string.Empty;
                                else obj.SerialNumber = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.InstallationDate = null;
                                else obj.InstallationDate = DateTime.Parse(reader.GetValue(3).ToString());
                                obj.DepartmentID = reader.GetInt32(4);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }
        public static DBT_Equipment GetById(int id)
        {
            var obj = new DBT_Equipment();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Equipment WHERE EquipmentID = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.EquipmentID = reader.GetInt32(0);
                                obj.EquipmentName = reader.GetString(1);
                                if (reader.IsDBNull(2)) obj.SerialNumber = string.Empty;
                                else obj.SerialNumber = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.InstallationDate = null;
                                else obj.InstallationDate = DateTime.Parse(reader.GetValue(3).ToString());
                                obj.DepartmentID = reader.GetInt32(4);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Equipment obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Equipment VALUES (@EquipmentName, @SerialNumber, @InstallationDate, @DepartmentID);";
                        query.Parameters.AddWithValue("@EquipmentName", obj.EquipmentName);
                        query.Parameters.AddWithValue("@SerialNumber", obj.SerialNumber);
                        query.Parameters.AddWithValue("@InstallationDate", obj.InstallationDate);
                        query.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

        public static int Edit(DBT_Equipment obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Equipment SET EquipmentName = @EquipmentName, SerialNumber = @SerialNumber, InstallationDate = @InstallationDate, DepartmentID = @DepartmentID WHERE EquipmentID = @id;";
                        query.Parameters.AddWithValue("@EquipmentName", obj.EquipmentName);
                        query.Parameters.AddWithValue("@SerialNumber", obj.SerialNumber);
                        query.Parameters.AddWithValue("@InstallationDate", obj.InstallationDate);
                        query.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
                        query.Parameters.AddWithValue("@id", obj.EquipmentID);
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
                        query.CommandText = "DELETE FROM Equipment WHERE EquipmentID = @id;";
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
