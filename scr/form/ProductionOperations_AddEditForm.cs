using DiplomaProgram_RPZ.scr.core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiplomaProgram_RPZ.scr.form
{
    public partial class ProductionOperations_AddEditForm : Form
    {
        DBT_ProductionOperations Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        ComboBox comboBox_OrderID;
        TextBox textBox_OperationName;
        ComboBox comboBox_EmployeeID;
        ComboBox comboBox_DepartmentID;
        DateTimePicker dateTimePicker_PlannedStartDate;
        DateTimePicker dateTimePicker_ActualStartDate;
        DateTimePicker dateTimePicker_ActualEndDate;
        TextBox textBox_Status;

        List<DBT_Departments> Departments = new List<DBT_Departments>();
        List<DBT_Employees> Employees = new List<DBT_Employees>();
        List<DBT_ProductionOrders> Orders = new List<DBT_ProductionOrders>();

        public ProductionOperations_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public ProductionOperations_AddEditForm(DBT_ProductionOperations obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            comboBox_OrderID.SelectedIndex = indexOf_Orders(obj.OrderID);
            textBox_OperationName.Text = obj.OperationName.ToString();
            comboBox_EmployeeID.SelectedIndex = indexOf_Employees(obj.EmployeeID);
            comboBox_DepartmentID.SelectedIndex = indexOf_Departments(obj.DepartmentID);
            dateTimePicker_PlannedStartDate.Value = (DateTime)((obj.PlannedStartDate == null) ? DateTime.Now : obj.PlannedStartDate);
            dateTimePicker_ActualStartDate.Value = (DateTime)((obj.ActualStartDate == null) ? DateTime.Now : obj.ActualStartDate);
            dateTimePicker_ActualEndDate.Value = (DateTime)((obj.ActualEndDate == null) ? DateTime.Now : obj.ActualEndDate);
            textBox_Status.Text = obj.Status.ToString();
        }

        private void Init()
        {
            this.Text = "Производственные операции";
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
                RowCount = 8
            };

            Label label_OrderID = new Label();
            SetLabel(ref label_OrderID, "Заказ");
            tableLayout.Controls.Add(label_OrderID, 0, 0);
            comboBox_OrderID = new ComboBox();
            comboBox_OrderID.Dock = DockStyle.Fill;
            comboBox_OrderID.MaxLength = 254;
            tableLayout.Controls.Add(comboBox_OrderID, 1, 0);

            Label label_OperationName = new Label();
            SetLabel(ref label_OperationName, "Наименование");
            tableLayout.Controls.Add(label_OperationName, 0, 1);
            textBox_OperationName = new TextBox();
            textBox_OperationName.Dock = DockStyle.Fill;
            textBox_OperationName.MaxLength = 254;
            tableLayout.Controls.Add(textBox_OperationName, 1, 1);

            Label label_EmployeeID = new Label();
            SetLabel(ref label_EmployeeID, "Сотрудник");
            tableLayout.Controls.Add(label_EmployeeID, 0, 2);
            comboBox_EmployeeID = new ComboBox();
            comboBox_EmployeeID.Dock = DockStyle.Fill;
            comboBox_EmployeeID.MaxLength = 254;
            tableLayout.Controls.Add(comboBox_EmployeeID, 1, 2);

            Label label_DepartmentID = new Label();
            SetLabel(ref label_DepartmentID, "Департамент");
            tableLayout.Controls.Add(label_DepartmentID, 0, 3);
            comboBox_DepartmentID = new ComboBox();
            comboBox_DepartmentID.Dock = DockStyle.Fill;
            comboBox_DepartmentID.MaxLength = 254;
            tableLayout.Controls.Add(comboBox_DepartmentID, 1, 3);
            LoadComboBox_Departments();
            LoadComboBox_Employees();
            LoadComboBox_Orders();

            Label label_PlannedStartDate = new Label();
            SetLabel(ref label_PlannedStartDate, "Запланированная дата начала");
            tableLayout.Controls.Add(label_PlannedStartDate, 0, 4);
            dateTimePicker_PlannedStartDate = new DateTimePicker();
            dateTimePicker_PlannedStartDate.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_PlannedStartDate, 1, 4);
            dateTimePicker_PlannedStartDate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_PlannedStartDate.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            Label label_ActualStartDate = new Label();
            SetLabel(ref label_ActualStartDate, "Фактическая начальная дата");
            tableLayout.Controls.Add(label_ActualStartDate, 0, 5);
            dateTimePicker_ActualStartDate = new DateTimePicker();
            dateTimePicker_ActualStartDate.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_ActualStartDate, 1, 5);
            dateTimePicker_ActualStartDate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_ActualStartDate.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            Label label_ActualEndDate = new Label();
            SetLabel(ref label_ActualEndDate, "Фактическая дата окончания");
            tableLayout.Controls.Add(label_ActualEndDate, 0, 6);
            dateTimePicker_ActualEndDate = new DateTimePicker();
            dateTimePicker_ActualEndDate.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_ActualEndDate, 1, 6);
            dateTimePicker_ActualEndDate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_ActualEndDate.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            Label label_Status = new Label();
            SetLabel(ref label_Status, "Статус");
            tableLayout.Controls.Add(label_Status, 0, 7);
            textBox_Status = new TextBox();
            textBox_Status.Dock = DockStyle.Fill;
            textBox_Status.MaxLength = 254;
            tableLayout.Controls.Add(textBox_Status, 1, 7);

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
        private void LoadComboBox_Orders()
        {
            Orders = DBT_ProductionOrders.GetAll();

            comboBox_OrderID.Items.Clear();
            foreach (var i in Orders)
                comboBox_OrderID.Items.Add($"№{i.OrderID}");
        }
        private int indexOf_Orders(int id)
        {
            for (int i = 0; i < Orders.Count; i++)
            {
                if (Orders[i].OrderID == id) return i;
            }
            return -1;
        }
        private void LoadComboBox_Employees()
        {
            Employees = DBT_Employees.GetAll();

            comboBox_EmployeeID.Items.Clear();
            foreach (var i in Employees)
                comboBox_EmployeeID.Items.Add(i.FullName);
        }
        private int indexOf_Employees(int id)
        {
            for (int i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].EmployeeID == id) return i;
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
            if (comboBox_OrderID.SelectedIndex == -1) { MessageBox.Show("Поле Заказ имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_OperationName.Text)) { MessageBox.Show("Поле Наименование имеет некорректное значение!"); return; }
            if (comboBox_EmployeeID.SelectedIndex == -1) { MessageBox.Show("Поле Сотрудник имеет некорректное значение!"); return; }
            if (comboBox_DepartmentID.SelectedIndex == -1) { MessageBox.Show("Поле Департамент имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Status.Text)) { MessageBox.Show("Поле Статус имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_ProductionOperations.Create(
                    new DBT_ProductionOperations()
                    {
                        OrderID = Orders[comboBox_OrderID.SelectedIndex].OrderID,
                        OperationName = textBox_OperationName.Text,
                        EmployeeID = Employees[comboBox_EmployeeID.SelectedIndex].EmployeeID,
                        DepartmentID = Departments[comboBox_DepartmentID.SelectedIndex].DepartmentID,
                        PlannedStartDate = dateTimePicker_PlannedStartDate.Value,
                        ActualStartDate = dateTimePicker_ActualStartDate.Value,
                        ActualEndDate = dateTimePicker_ActualEndDate.Value,
                        Status = textBox_Status.Text
                    }
                );
            }
            else
            {
                res = DBT_ProductionOperations.Edit(
                    new DBT_ProductionOperations()
                    {
                        OperationID = Object.OperationID,
                        OrderID = Orders[comboBox_OrderID.SelectedIndex].OrderID,
                        OperationName = textBox_OperationName.Text,
                        EmployeeID = Employees[comboBox_EmployeeID.SelectedIndex].EmployeeID,
                        DepartmentID = Departments[comboBox_DepartmentID.SelectedIndex].DepartmentID,
                        PlannedStartDate = dateTimePicker_PlannedStartDate.Value,
                        ActualStartDate = dateTimePicker_ActualStartDate.Value,
                        ActualEndDate = dateTimePicker_ActualEndDate.Value,
                        Status = textBox_Status.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
