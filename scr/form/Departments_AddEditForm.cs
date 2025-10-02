using DiplomaProgram_RPZ.scr.core;

namespace DiplomaProgram_RPZ.scr.form
{
    public partial class Departments_AddEditForm : Form
    {
        DBT_Departments Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_DepartmentName;
        TextBox textBox_Location;

        public Departments_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Departments_AddEditForm(DBT_Departments obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            textBox_DepartmentName.Text = obj.DepartmentName.ToString();
            textBox_Location.Text = obj.Location.ToString();
        }

        private void Init()
        {
            this.Text = "Департамент";
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
                RowCount = 2
            };

            Label label_DepartmentName = new Label();
            SetLabel(ref label_DepartmentName, "Название");
            tableLayout.Controls.Add(label_DepartmentName, 0, 0);
            textBox_DepartmentName = new TextBox();
            textBox_DepartmentName.Dock = DockStyle.Fill;
            textBox_DepartmentName.MaxLength = 254;
            tableLayout.Controls.Add(textBox_DepartmentName, 1, 0);

            Label label_Location = new Label();
            SetLabel(ref label_Location, "Расположение");
            tableLayout.Controls.Add(label_Location, 0, 1);
            textBox_Location = new TextBox();
            textBox_Location.Dock = DockStyle.Fill;
            textBox_Location.MaxLength = 254;
            tableLayout.Controls.Add(textBox_Location, 1, 1);

            this.Controls.Add(tableLayout);
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
            if (string.IsNullOrWhiteSpace(textBox_DepartmentName.Text)) { MessageBox.Show("Поле Название имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Departments.Create(
                    new DBT_Departments()
                    {
                        DepartmentName = textBox_DepartmentName.Text,
                        Location = textBox_Location.Text
                    }
                );
            }
            else
            {
                res = DBT_Departments.Edit(
                    new DBT_Departments()
                    {
                        DepartmentID = Object.DepartmentID,
                        DepartmentName = textBox_DepartmentName.Text,
                        Location = textBox_Location.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
