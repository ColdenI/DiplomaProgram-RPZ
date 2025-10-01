﻿using Microsoft.Data.SqlClient;

namespace DiplomaProgram_RPZ.scr.core
{
    public static class SQL
    {
        public static string _sqlConnectStr = "Server=localhost;Database=RamenInstrumentPlant;Trusted_Connection=True;TrustServerCertificate=True;";
        public static SqlConnection SqlConnect = new SqlConnection(_sqlConnectStr);

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
