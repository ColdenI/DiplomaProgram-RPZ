using DiplomaProgram_RPZ.scr.core;
using Microsoft.Data.SqlClient;

namespace DiplomaProgram_RPZ.scr.form
{
    public partial class ProductionOperations_ViewForm : Form
    {
        DataGridView dataGridView;
        TextBox textBox_search;
        Button button_edit;
        Button button_update;
        Button button_create;
        Button button_remove;

        public ProductionOperations_ViewForm()
        {
            InitializeComponent();

            this.Load += ProductionOperations_ViewForm_Load;
            this.Disposed += ProductionOperations_ViewForm_Disposed;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Text = "Производственные операции";

            button_create = new Button()
            {
                Text = "Добавить",
                Height = 30,
                Dock = DockStyle.Bottom
            };
            button_edit = new Button()
            {
                Text = "Изменить",
                Height = 30,
                Dock = DockStyle.Bottom
            };
            button_remove = new Button()
            {
                Text = "Удалить",
                Height = 30,
                Dock = DockStyle.Bottom
            };
            button_update = new Button()
            {
                Text = "Обновить",
                Height = 30,
                Dock = DockStyle.Bottom
            };
            textBox_search = new TextBox()
            {
                Dock = DockStyle.Top
            };
            dataGridView = new DataGridView()
            {
                Dock = DockStyle.Fill
            };

            button_update.Click += Button_update_Click;
            button_remove.Click += Button_remove_Click;
            button_create.Click += Button_create_Click;
            button_edit.Click += Button_edit_Click;
            textBox_search.TextChanged += TextBox_search_TextChanged;

            this.Controls.Add(button_create);
            this.Controls.Add(button_edit);
            this.Controls.Add(button_remove);
            this.Controls.Add(button_update);
            this.Controls.Add(textBox_search);
            this.Controls.Add(dataGridView);
        }

        private void ProductionOperations_ViewForm_Disposed(object? sender, EventArgs e)
        {
            this.Load -= ProductionOperations_ViewForm_Load;
            this.Disposed -= ProductionOperations_ViewForm_Disposed;
            button_update.Click -= Button_update_Click;
            button_remove.Click -= Button_remove_Click;
            button_create.Click -= Button_create_Click;
            button_edit.Click -= Button_edit_Click;
            textBox_search.TextChanged -= TextBox_search_TextChanged;
        }

        private void Button_edit_Click(object? sender, EventArgs e)
        {
            new ProductionOperations_AddEditForm(DBT_ProductionOperations.GetById((int)dataGridView.CurrentCell.OwningRow.Cells[0].Value)).ShowDialog();
            UpdateTable();
        }

        private void Button_create_Click(object? sender, EventArgs e)
        {
            new ProductionOperations_AddEditForm().ShowDialog();
            UpdateTable();
        }

        private void Button_remove_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить запись?\r\nОтменить будет невозможно!\r\n", "Удалить", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            int res = DBT_ProductionOperations.Remove((int)dataGridView.CurrentCell.OwningRow.Cells[0].Value);
            if (res == -1) MessageBox.Show("Ошибка удаления!");
            else if (res == 0) MessageBox.Show("Успешно удалено!");
            UpdateTable();
        }

        private void TextBox_search_TextChanged(object? sender, EventArgs e) => UpdateTable();
        private void Button_update_Click(object? sender, EventArgs e) => UpdateTable();
        private void ProductionOperations_ViewForm_Load(object sender, EventArgs e) => UpdateTable();

        private void UpdateTable()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.BringToFront();
            dataGridView.ReadOnly = true;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView.Columns.Add("OperationID", "ID");
            dataGridView.Columns.Add("OrderID", "Заказ");
            dataGridView.Columns.Add("OperationName", "Наименование");
            dataGridView.Columns.Add("EmployeeID", "Сотрудник");
            dataGridView.Columns.Add("DepartmentID", "Департамент");
            dataGridView.Columns.Add("PlannedStartDate", "Запланированная дата начала");
            dataGridView.Columns.Add("ActualStartDate", "Фактическая начальная дата");
            dataGridView.Columns.Add("ActualEndDate", "Фактическая дата окончания");
            dataGridView.Columns.Add("Status", "Статус");

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
                            string search = textBox_search.Text.ToLower();
                            if (!string.IsNullOrWhiteSpace(search))
                                if (
                                    !reader.GetValue(0).ToString().Contains(search) &&
                                    !reader.GetValue(1).ToString().Contains(search) &&
                                    !reader.GetValue(2).ToString().Contains(search) &&
                                    !reader.GetValue(3).ToString().Contains(search) &&
                                    !reader.GetValue(4).ToString().Contains(search) &&
                                    !reader.GetValue(5).ToString().Contains(search) &&
                                    !reader.GetValue(6).ToString().Contains(search) &&
                                    !reader.GetValue(7).ToString().Contains(search) &&
                                    !reader.GetValue(8).ToString().Contains(search)
                                ) continue;

                            var index = dataGridView.Rows.Add();
                            dataGridView.Rows[index].Cells[0].Value = reader.GetInt32(0);
                            dataGridView.Rows[index].Cells[1].Value = "№" + DBT_ProductionOrders.GetById(reader.GetInt32(1)).OrderID;
                            dataGridView.Rows[index].Cells[2].Value = reader.GetString(2);
                            dataGridView.Rows[index].Cells[3].Value = DBT_Employees.GetById(reader.GetInt32(3)).FullName;
                            dataGridView.Rows[index].Cells[4].Value = DBT_Departments.GetById(reader.GetInt32(4)).DepartmentName;
                            if (reader.IsDBNull(5)) dataGridView.Rows[index].Cells[5].Value = "-";
                            else dataGridView.Rows[index].Cells[5].Value = DateTime.Parse(reader.GetValue(5).ToString());
                            if (reader.IsDBNull(6)) dataGridView.Rows[index].Cells[6].Value = "-";
                            else dataGridView.Rows[index].Cells[6].Value = DateTime.Parse(reader.GetValue(6).ToString());
                            if (reader.IsDBNull(7)) dataGridView.Rows[index].Cells[7].Value = "-";
                            else dataGridView.Rows[index].Cells[7].Value = DateTime.Parse(reader.GetValue(7).ToString());
                            dataGridView.Rows[index].Cells[8].Value = reader.GetString(8);

                        }
                    }
                }
            }
        }
    }


}
