using DiplomaProgram_RPZ.scr.core;
using DiplomaProgram_RPZ.scr.form;
using Microsoft.Data.SqlClient;

namespace DiplomaProgram_RPZ
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT COUNT(*) FROM Auth WHERE Login = @login AND PasswordHash = @password;";
                        query.Parameters.AddWithValue("@login", textBox_login.Text);
                        query.Parameters.AddWithValue("@password", textBox_password.Text);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.GetInt32(0) == 0)
                                {
                                    MessageBox.Show("Ошибка");
                                    return;
                                }
                            }
                        }
                    }

                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT Employees.EmployeeID FROM Employees WHERE Employees.EmployeeID = (SELECT Auth.EmployeeID FROM Auth WHERE Login = @login AND PasswordHash = @password);";
                        query.Parameters.AddWithValue("@login", textBox_login.Text);
                        query.Parameters.AddWithValue("@password", textBox_password.Text);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.IsDBNull(0))
                                {
                                    MessageBox.Show("Ошибка");
                                    return;
                                }
                                else
                                {
                                    Core.ThisEmployee = DBT_Employees.GetById(reader.GetInt32(0));
                                    if (Core.ThisEmployee == null)
                                    {
                                        MessageBox.Show("Ошибка");
                                        return;
                                    }
                                    else
                                    {
                                        this.Hide();
                                        new MainForm().ShowDialog();
                                        this.Show();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
                return;
            }
        }
    }
}
