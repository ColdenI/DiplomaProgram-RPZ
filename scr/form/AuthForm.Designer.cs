namespace DiplomaProgram_RPZ
{
    partial class AuthForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            textBox_login = new TextBox();
            textBox_password = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 13.7454548F);
            label1.Location = new Point(85, 18);
            label1.Name = "label1";
            label1.Size = new Size(76, 29);
            label1.TabIndex = 0;
            label1.Text = "Логин";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Comic Sans MS", 13.7454548F);
            label2.Location = new Point(85, 111);
            label2.Name = "label2";
            label2.Size = new Size(90, 29);
            label2.TabIndex = 1;
            label2.Text = "Пароль";
            // 
            // textBox_login
            // 
            textBox_login.Font = new Font("Comic Sans MS", 13.7454548F);
            textBox_login.Location = new Point(12, 59);
            textBox_login.Name = "textBox_login";
            textBox_login.Size = new Size(239, 37);
            textBox_login.TabIndex = 2;
            // 
            // textBox_password
            // 
            textBox_password.Font = new Font("Comic Sans MS", 13.7454548F);
            textBox_password.Location = new Point(12, 148);
            textBox_password.Name = "textBox_password";
            textBox_password.Size = new Size(239, 37);
            textBox_password.TabIndex = 3;
            // 
            // button1
            // 
            button1.Font = new Font("Comic Sans MS", 13.7454548F);
            button1.Location = new Point(72, 214);
            button1.Name = "button1";
            button1.Size = new Size(124, 48);
            button1.TabIndex = 4;
            button1.Text = "Войти";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(274, 281);
            Controls.Add(button1);
            Controls.Add(textBox_password);
            Controls.Add(textBox_login);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "AuthForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Авторизация";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox_login;
        private TextBox textBox_password;
        private Button button1;
    }
}
