using DiplomaProgram_RPZ.scr.core;

namespace DiplomaProgram_RPZ.scr.form
{
    public partial class Employees_AddEditForm : Form
    {
        DBT_Employees Object;
        DBT_Auth Auth;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_FullName;
        TextBox textBox_Position;
        DateTimePicker dateTimePicker_HireDate;
        ComboBox comboBox_DepartmentID;
        TextBox textBox_RoleID;

        TextBox textBox_Login;
        TextBox textBox_Password;

        List<DBT_Departments> Departments = new List<DBT_Departments>();

        public Employees_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Employees_AddEditForm(DBT_Employees obj)
        {
            InitializeComponent();
            Object = obj;
            Auth = DBT_Auth.GetByEmployeeID(obj.EmployeeID);
            Init();

            textBox_FullName.Text = obj.FullName.ToString();
            textBox_Position.Text = obj.Position.ToString();
            dateTimePicker_HireDate.Value = (DateTime)obj.HireDate;
            comboBox_DepartmentID.SelectedIndex = indexOf_Departments(obj.DepartmentID);
            textBox_RoleID.Text = obj.RoleID.ToString();

            try
            {
                textBox_Login.Text = Auth.Login.ToString();
                textBox_Password.Text = Auth.PasswordHash.ToString();
            }catch { }
        }

        private void Init()
        {
            this.Text = "Сотрудники";
            this.MinimumSize = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            button_apply = new Button()
            {
                Height = 30,
                Dock = DockStyle.Bottom
            };
            button_apply.Click += Button_apply_Click;
            button_apply.Text = Object == null ? "Добавить" : "Изменить";
            this.Controls.Add(button_apply);

            tableLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 7
            };

            Label label_FullName = new Label();
            SetLabel(ref label_FullName, "ФИО");
            tableLayout.Controls.Add(label_FullName, 0, 0);
            textBox_FullName = new TextBox();
            textBox_FullName.Dock = DockStyle.Fill;
            textBox_FullName.MaxLength = 254;
            tableLayout.Controls.Add(textBox_FullName, 1, 0);

            Label label_Position = new Label();
            SetLabel(ref label_Position, "Должность");
            tableLayout.Controls.Add(label_Position, 0, 1);
            textBox_Position = new TextBox();
            textBox_Position.Dock = DockStyle.Fill;
            textBox_Position.MaxLength = 254;
            tableLayout.Controls.Add(textBox_Position, 1, 1);

            Label label_HireDate = new Label();
            SetLabel(ref label_HireDate, "Дата найма");
            tableLayout.Controls.Add(label_HireDate, 0, 2);
            dateTimePicker_HireDate = new DateTimePicker();
            dateTimePicker_HireDate.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_HireDate, 1, 2);
            dateTimePicker_HireDate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_HireDate.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            Label label_DepartmentID = new Label();
            SetLabel(ref label_DepartmentID, "Департамент");
            tableLayout.Controls.Add(label_DepartmentID, 0, 3);
            comboBox_DepartmentID = new ComboBox();
            comboBox_DepartmentID.Dock = DockStyle.Fill;
            comboBox_DepartmentID.MaxLength = 254;
            tableLayout.Controls.Add(comboBox_DepartmentID, 1, 3);
            LoadComboBox_Departments();

            Label label_RoleID = new Label();
            SetLabel(ref label_RoleID, "Роль");
            tableLayout.Controls.Add(label_RoleID, 0, 4);
            textBox_RoleID = new TextBox();
            textBox_RoleID.Dock = DockStyle.Fill;
            textBox_RoleID.MaxLength = 254;
            tableLayout.Controls.Add(textBox_RoleID, 1, 4);

            Label label_login = new Label();
            SetLabel(ref label_login, "Логин");
            tableLayout.Controls.Add(label_login, 0, 5);
            textBox_Login = new TextBox();
            textBox_Login.Dock = DockStyle.Fill;
            textBox_Login.MaxLength = 254;
            tableLayout.Controls.Add(textBox_Login, 1, 5);

            Label label_password = new Label();
            SetLabel(ref label_password, "Пароль");
            tableLayout.Controls.Add(label_password, 0, 6);
            textBox_Password = new TextBox();
            textBox_Password.Dock = DockStyle.Fill;
            textBox_Password.MaxLength = 254;
            textBox_Password.PasswordChar = '*';
            tableLayout.Controls.Add(textBox_Password, 1, 6);

            this.Controls.Add(tableLayout);
        }

        private void LoadComboBox_Departments()
        {
            Departments = DBT_Departments.GetAll();

            comboBox_DepartmentID.Items.Clear();
            foreach (var i in Departments)
                comboBox_DepartmentID.Items.Add(i.DepartmentName);
        }
        private int indexOf_Departments(int id)
        {
            for (int i = 0; i < Departments.Count; i++)
            {
                if (Departments[i].DepartmentID == id) return i;
            }
            return -1;
        }

        private void SetLabel(ref Label label, string text = "")
        {
            label.Font = new Font(Font.FontFamily, 12);
            label.TextAlign = ContentAlignment.TopLeft;
            label.Dock = DockStyle.Fill;
            label.AutoSize = false;
            label.Width = 200;
            label.Text = text;
        }

        private void Button_apply_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_FullName.Text)) { MessageBox.Show("Поле ФИО имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Login.Text)) { MessageBox.Show("Поле Логин имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Password.Text)) { MessageBox.Show("Поле Пароль имеет некорректное значение!"); return; }
            if (comboBox_DepartmentID.SelectedIndex == -1) { MessageBox.Show("Поле Департамент имеет некорректное значение!"); return; }
            if (!int.TryParse(textBox_RoleID.Text, out int tp_RoleID)) { MessageBox.Show("Поле Роль имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Employees.Create(
                    new DBT_Employees()
                    {
                        FullName = textBox_FullName.Text,
                        Position = textBox_Position.Text,
                        HireDate = dateTimePicker_HireDate.Value,
                        DepartmentID = Departments[comboBox_DepartmentID.SelectedIndex].DepartmentID,
                        RoleID = int.Parse(textBox_RoleID.Text)
                    }
                );
                if (res == -1)
                {
                    MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
                    return;
                }
                res = DBT_Auth.Create(
                    new DBT_Auth()
                    {
                        Login = textBox_Login.Text,
                        PasswordHash = textBox_Password.Text,
                        EmployeeID = res,
                        CreatedAt = DateTime.Now
                    }
                );
            }
            else
            {
                res = DBT_Employees.Edit(
                    new DBT_Employees()
                    {
                        EmployeeID = Object.EmployeeID,
                        FullName = textBox_FullName.Text,
                        Position = textBox_Position.Text,
                        HireDate = dateTimePicker_HireDate.Value,
                        DepartmentID = Departments[comboBox_DepartmentID.SelectedIndex].DepartmentID,
                        RoleID = int.Parse(textBox_RoleID.Text)
                    }
                );
                if (res == -1)
                {
                    MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
                    return;
                }
                res = DBT_Auth.Edit(
                    new DBT_Auth()
                    {
                        AuthID = Auth.AuthID,
                        Login = textBox_Login.Text,
                        PasswordHash = textBox_Password.Text,
                        EmployeeID = Object.EmployeeID,
                        CreatedAt = Auth.CreatedAt
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
