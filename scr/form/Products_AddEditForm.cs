using DiplomaProgram_RPZ.scr.core;

namespace DiplomaProgram_RPZ.scr.form
{
    public partial class Products_AddEditForm : Form
    {
        DBT_Products Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        TextBox textBox_ProductName;
        TextBox textBox_ProductCode;
        DateTimePicker dateTimePicker_ProductionStartDate;
        TextBox textBox_Description;

        public Products_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public Products_AddEditForm(DBT_Products obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            textBox_ProductName.Text = obj.ProductName.ToString();
            textBox_ProductCode.Text = obj.ProductCode.ToString();
            dateTimePicker_ProductionStartDate.Value = (DateTime) obj.ProductionStartDate;
            textBox_Description.Text = obj.Description.ToString();
        }

        private void Init()
        {
            this.Text = "";
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

            Label label_ProductName = new Label();
            SetLabel(ref label_ProductName, "Наименование");
            tableLayout.Controls.Add(label_ProductName, 0, 0);
            textBox_ProductName = new TextBox();
            textBox_ProductName.Dock = DockStyle.Fill;
            textBox_ProductName.MaxLength = 254;
            tableLayout.Controls.Add(textBox_ProductName, 1, 0);

            Label label_ProductCode = new Label();
            SetLabel(ref label_ProductCode, "Код");
            tableLayout.Controls.Add(label_ProductCode, 0, 1);
            textBox_ProductCode = new TextBox();
            textBox_ProductCode.Dock = DockStyle.Fill;
            textBox_ProductCode.MaxLength = 254;
            tableLayout.Controls.Add(textBox_ProductCode, 1, 1);

            Label label_ProductionStartDate = new Label();
            SetLabel(ref label_ProductionStartDate, "Дата начала производства");
            tableLayout.Controls.Add(label_ProductionStartDate, 0, 2);
            dateTimePicker_ProductionStartDate = new DateTimePicker();
            dateTimePicker_ProductionStartDate.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_ProductionStartDate, 1, 2);
            dateTimePicker_ProductionStartDate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_ProductionStartDate.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            Label label_Description = new Label();
            SetLabel(ref label_Description, "Описание");
            tableLayout.Controls.Add(label_Description, 0, 3);
            textBox_Description = new TextBox();
            textBox_Description.Dock = DockStyle.Fill;
            textBox_Description.MaxLength = 254;
            tableLayout.Controls.Add(textBox_Description, 1, 3);

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
            if (string.IsNullOrWhiteSpace(textBox_ProductName.Text)) { MessageBox.Show("Поле Наименование имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_ProductCode.Text)) { MessageBox.Show("Поле Код имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_Products.Create(
                    new DBT_Products()
                    {
                        ProductName = textBox_ProductName.Text,
                        ProductCode = textBox_ProductCode.Text,
                        ProductionStartDate = dateTimePicker_ProductionStartDate.Value,
                        Description = textBox_Description.Text
                    }
                );
            }
            else
            {
                res = DBT_Products.Edit(
                    new DBT_Products()
                    {
                        ProductID = Object.ProductID,
                        ProductName = textBox_ProductName.Text,
                        ProductCode = textBox_ProductCode.Text,
                        ProductionStartDate = dateTimePicker_ProductionStartDate.Value,
                        Description = textBox_Description.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
