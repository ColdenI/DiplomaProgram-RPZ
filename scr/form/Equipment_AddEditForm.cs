using DiplomaProgram_RPZ.scr.core;

namespace DiplomaProgram_RPZ.scr.form
{
    public partial class Equipment_AddEditForm : Form
    {
        DBT_Equipment Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_EquipmentName;
        TextBox textBox_SerialNumber;
        DateTimePicker dateTimePicker_InstallationDate;
        ComboBox comboBox_DepartmentID;

        List<DBT_Departments> Departments = new List<DBT_Departments>();

        public Equipment_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Equipment_AddEditForm(DBT_Equipment obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            textBox_EquipmentName.Text = obj.EquipmentName.ToString();
            textBox_SerialNumber.Text = obj.SerialNumber.ToString();
            dateTimePicker_InstallationDate.Value = (DateTime)obj.InstallationDate;
            comboBox_DepartmentID.SelectedIndex = indexOf_Departments(obj.DepartmentID);
        }

        private void Init()
        {
            this.Text = "Оборудование";
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
                RowCount = 4
            };

            Label label_EquipmentName = new Label();
            SetLabel(ref label_EquipmentName, "Наименование");
            tableLayout.Controls.Add(label_EquipmentName, 0, 0);
            textBox_EquipmentName = new TextBox();
            textBox_EquipmentName.Dock = DockStyle.Fill;
            textBox_EquipmentName.MaxLength = 254;
            tableLayout.Controls.Add(textBox_EquipmentName, 1, 0);

            Label label_SerialNumber = new Label();
            SetLabel(ref label_SerialNumber, "Серийный номер");
            tableLayout.Controls.Add(label_SerialNumber, 0, 1);
            textBox_SerialNumber = new TextBox();
            textBox_SerialNumber.Dock = DockStyle.Fill;
            textBox_SerialNumber.MaxLength = 254;
            tableLayout.Controls.Add(textBox_SerialNumber, 1, 1);

            Label label_InstallationDate = new Label();
            SetLabel(ref label_InstallationDate, "Дата установки");
            tableLayout.Controls.Add(label_InstallationDate, 0, 2);
            dateTimePicker_InstallationDate = new DateTimePicker();
            dateTimePicker_InstallationDate.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_InstallationDate, 1, 2);
            dateTimePicker_InstallationDate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_InstallationDate.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            Label label_DepartmentID = new Label();
            SetLabel(ref label_DepartmentID, "Департамент");
            tableLayout.Controls.Add(label_DepartmentID, 0, 3);
            comboBox_DepartmentID = new ComboBox();
            comboBox_DepartmentID.Dock = DockStyle.Fill;
            comboBox_DepartmentID.MaxLength = 254;
            tableLayout.Controls.Add(comboBox_DepartmentID, 1, 3);
            LoadComboBox_Departments();

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
            if (string.IsNullOrWhiteSpace(textBox_EquipmentName.Text)) { MessageBox.Show("Поле Наименование имеет некорректное значение!"); return; }
            if (comboBox_DepartmentID.SelectedIndex == -1) { MessageBox.Show("Поле Департамент имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Equipment.Create(
                    new DBT_Equipment()
                    {
                        EquipmentName = textBox_EquipmentName.Text,
                        SerialNumber = textBox_SerialNumber.Text,
                        InstallationDate = dateTimePicker_InstallationDate.Value,
                        DepartmentID = Departments[comboBox_DepartmentID.SelectedIndex].DepartmentID
                    }
                );
            }
            else
            {
                res = DBT_Equipment.Edit(
                    new DBT_Equipment()
                    {
                        EquipmentID = Object.EquipmentID,
                        EquipmentName = textBox_EquipmentName.Text,
                        SerialNumber = textBox_SerialNumber.Text,
                        InstallationDate = dateTimePicker_InstallationDate.Value,
                        DepartmentID = Departments[comboBox_DepartmentID.SelectedIndex].DepartmentID
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
