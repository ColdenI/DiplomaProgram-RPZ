using DiplomaProgram_RPZ.scr.core;

namespace DiplomaProgram_RPZ.scr.form
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            label1.Text = $"Вы вошли как: {Core.ThisEmployee.FullName}";

            if(Core.ThisEmployee.RoleID == 0)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
            }
            if(Core.ThisEmployee.RoleID == 2)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Departments_ViewForm().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Employees_ViewForm().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Equipment_ViewForm().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Products_ViewForm().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new ProductionOrders_ViewForm().Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new ProductionOperations_ViewForm().Show();
        }
    }
}
