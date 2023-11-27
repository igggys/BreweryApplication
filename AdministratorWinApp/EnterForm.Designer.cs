namespace AdministratorWinApp
{
    partial class EnterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterForm));
            labelEnterButton = new Label();
            labelCancelButton = new Label();
            textBoxUserName = new TextBox();
            textBoxPassword = new TextBox();
            SuspendLayout();
            // 
            // labelEnterButton
            // 
            labelEnterButton.BackColor = Color.Transparent;
            labelEnterButton.Cursor = Cursors.Hand;
            labelEnterButton.Location = new Point(286, 351);
            labelEnterButton.Name = "labelEnterButton";
            labelEnterButton.Size = new Size(131, 34);
            labelEnterButton.TabIndex = 0;
            labelEnterButton.Click += labelButton_Click;
            // 
            // labelCancelButton
            // 
            labelCancelButton.BackColor = Color.Transparent;
            labelCancelButton.Cursor = Cursors.Hand;
            labelCancelButton.Location = new Point(442, 351);
            labelCancelButton.Name = "labelCancelButton";
            labelCancelButton.Size = new Size(131, 34);
            labelCancelButton.TabIndex = 1;
            labelCancelButton.Click += labelButton_Click;
            // 
            // textBoxUserName
            // 
            textBoxUserName.BorderStyle = BorderStyle.None;
            textBoxUserName.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxUserName.ForeColor = Color.Black;
            textBoxUserName.Location = new Point(78, 200);
            textBoxUserName.MaxLength = 50;
            textBoxUserName.Name = "textBoxUserName";
            textBoxUserName.Size = new Size(312, 20);
            textBoxUserName.TabIndex = 2;
            // 
            // textBoxPassword
            // 
            textBoxPassword.BorderStyle = BorderStyle.None;
            textBoxPassword.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxPassword.ForeColor = Color.Black;
            textBoxPassword.Location = new Point(78, 256);
            textBoxPassword.MaxLength = 50;
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(312, 20);
            textBoxPassword.TabIndex = 3;
            textBoxPassword.UseSystemPasswordChar = true;
            // 
            // EnterForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.bewery;
            ClientSize = new Size(601, 396);
            Controls.Add(textBoxPassword);
            Controls.Add(textBoxUserName);
            Controls.Add(labelCancelButton);
            Controls.Add(labelEnterButton);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(601, 396);
            MinimizeBox = false;
            MinimumSize = new Size(601, 396);
            Name = "EnterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EnterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelEnterButton;
        private Label labelCancelButton;
        private TextBox textBoxUserName;
        private TextBox textBoxPassword;
    }
}