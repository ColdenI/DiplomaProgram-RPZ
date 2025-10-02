using DiplomaProgram_RPZ.scr.core;

namespace DiplomaProgram_RPZ.scr.form
{
    public partial class ProductionOrders_AddEditForm : Form
    {
        DBT_ProductionOrders Object;

        Button button_apply;
        TableLayoutPanel tableLayout;

        ComboBox comboBox_ProductID;
        DateTimePicker dateTimePicker_OrderDate;
        DateTimePicker dateTimePicker_PlannedCompletionDate;
        DateTimePicker dateTimePicker_ActualCompletionDate;
        TextBox textBox_Quantity;
        TextBox textBox_Status;

        List<DBT_Products> Products = new List<DBT_Products>();

        public ProductionOrders_AddEditForm()
        {
            InitializeComponent();
            Init();
        }
        public ProductionOrders_AddEditForm(DBT_ProductionOrders obj)
        {
            InitializeComponent();
            Object = obj;
            Init();

            comboBox_ProductID.SelectedIndex = indexOf_Products(obj.ProductID);
            dateTimePicker_OrderDate.Value = obj.OrderDate;
            dateTimePicker_PlannedCompletionDate.Value = (DateTime)obj.PlannedCompletionDate;
            dateTimePicker_ActualCompletionDate.Value = (DateTime)((obj.ActualCompletionDate == null) ? DateTime.Now : obj.ActualCompletionDate);
            textBox_Quantity.Text = obj.Quantity.ToString();
            textBox_Status.Text = obj.Status.ToString();
        }

        private void Init()
        {
            this.Text = "Заказы";
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
                RowCount = 6
            };

            Label label_ProductID = new Label();
            SetLabel(ref label_ProductID, "Продукт");
            tableLayout.Controls.Add(label_ProductID, 0, 0);
            comboBox_ProductID = new ComboBox();
            comboBox_ProductID.Dock = DockStyle.Fill;
            comboBox_ProductID.MaxLength = 254;
            tableLayout.Controls.Add(comboBox_ProductID, 1, 0);
            LoadComboBox_Products();

            Label label_OrderDate = new Label();
            SetLabel(ref label_OrderDate, "Дата заказа");
            tableLayout.Controls.Add(label_OrderDate, 0, 1);
            dateTimePicker_OrderDate = new DateTimePicker();
            dateTimePicker_OrderDate.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_OrderDate, 1, 1);
            dateTimePicker_OrderDate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_OrderDate.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            Label label_PlannedCompletionDate = new Label();
            SetLabel(ref label_PlannedCompletionDate, "Планируемая дата завершения");
            tableLayout.Controls.Add(label_PlannedCompletionDate, 0, 2);
            dateTimePicker_PlannedCompletionDate = new DateTimePicker();
            dateTimePicker_PlannedCompletionDate.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_PlannedCompletionDate, 1, 2);
            dateTimePicker_PlannedCompletionDate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_PlannedCompletionDate.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            Label label_ActualCompletionDate = new Label();
            SetLabel(ref label_ActualCompletionDate, "Фактическая дата завершения");
            tableLayout.Controls.Add(label_ActualCompletionDate, 0, 3);
            dateTimePicker_ActualCompletionDate = new DateTimePicker();
            dateTimePicker_ActualCompletionDate.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(dateTimePicker_ActualCompletionDate, 1, 3);
            dateTimePicker_ActualCompletionDate.Format = DateTimePickerFormat.Custom;
            dateTimePicker_ActualCompletionDate.CustomFormat = "yyyy.MM.dd HH:mm:ss";

            Label label_Quantity = new Label();
            SetLabel(ref label_Quantity, "Количество");
            tableLayout.Controls.Add(label_Quantity, 0, 4);
            textBox_Quantity = new TextBox();
            textBox_Quantity.Dock = DockStyle.Fill;
            textBox_Quantity.MaxLength = 254;
            tableLayout.Controls.Add(textBox_Quantity, 1, 4);

            Label label_Status = new Label();
            SetLabel(ref label_Status, "Статус");
            tableLayout.Controls.Add(label_Status, 0, 5);
            textBox_Status = new TextBox();
            textBox_Status.Dock = DockStyle.Fill;
            textBox_Status.MaxLength = 254;
            tableLayout.Controls.Add(textBox_Status, 1, 5);

            this.Controls.Add(tableLayout);
        }
        private void LoadComboBox_Products()
        {
            Products = DBT_Products.GetAll();

            comboBox_ProductID.Items.Clear();
            foreach (var i in Products)
                comboBox_ProductID.Items.Add(i.ProductName);
        }
        private int indexOf_Products(int id)
        {
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].ProductID == id) return i;
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
            if (comboBox_ProductID.SelectedIndex == -1) { MessageBox.Show("Поле Продукт имеет некорректное значение!"); return; }
            if (!int.TryParse(textBox_Quantity.Text, out int tp_Quantity)) { MessageBox.Show("Поле Количество имеет некорректное значение!"); return; }
            if (string.IsNullOrWhiteSpace(textBox_Status.Text)) { MessageBox.Show("Поле Статус имеет некорректное значение!"); return; }

            int res = 0;

            if (Object == null)
            {
                res = DBT_ProductionOrders.Create(
                    new DBT_ProductionOrders()
                    {
                        ProductID = Products[comboBox_ProductID.SelectedIndex].ProductID,
                        OrderDate = dateTimePicker_OrderDate.Value,
                        PlannedCompletionDate = dateTimePicker_PlannedCompletionDate.Value,
                        ActualCompletionDate = dateTimePicker_ActualCompletionDate.Value,
                        Quantity = int.Parse(textBox_Quantity.Text),
                        Status = textBox_Status.Text
                    }
                );
            }
            else
            {
                res = DBT_ProductionOrders.Edit(
                    new DBT_ProductionOrders()
                    {
                        OrderID = Object.OrderID,
                        ProductID = Products[comboBox_ProductID.SelectedIndex].ProductID,
                        OrderDate = dateTimePicker_OrderDate.Value,
                        PlannedCompletionDate = dateTimePicker_PlannedCompletionDate.Value,
                        ActualCompletionDate = dateTimePicker_ActualCompletionDate.Value,
                        Quantity = int.Parse(textBox_Quantity.Text),
                        Status = textBox_Status.Text
                    }
                );
            }
            if (res == -1) MessageBox.Show("Ошибка! Один из ID не ссылается на запись в БД!");
            else this.Close();
        }
    }
}
