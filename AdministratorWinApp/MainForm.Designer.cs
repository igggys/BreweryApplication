namespace AdministratorWinApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panelCloseButton = new Panel();
            panelMaxButton = new Panel();
            panelMinButton = new Panel();
            SuspendLayout();
            // 
            // panelCloseButton
            // 
            panelCloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelCloseButton.BackgroundImage = Properties.Resources.CloseButton;
            panelCloseButton.BackgroundImageLayout = ImageLayout.Center;
            panelCloseButton.Cursor = Cursors.Hand;
            panelCloseButton.Location = new Point(844, 6);
            panelCloseButton.Name = "panelCloseButton";
            panelCloseButton.Size = new Size(20, 20);
            panelCloseButton.TabIndex = 0;
            panelCloseButton.Click += panelButton_Click;
            // 
            // panelMaxButton
            // 
            panelMaxButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelMaxButton.BackgroundImage = Properties.Resources.MaxButton;
            panelMaxButton.BackgroundImageLayout = ImageLayout.Center;
            panelMaxButton.Cursor = Cursors.Hand;
            panelMaxButton.Location = new Point(818, 6);
            panelMaxButton.Name = "panelMaxButton";
            panelMaxButton.Size = new Size(20, 20);
            panelMaxButton.TabIndex = 1;
            panelMaxButton.Click += panelButton_Click;
            // 
            // panelMinButton
            // 
            panelMinButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelMinButton.BackgroundImage = Properties.Resources.MinButton;
            panelMinButton.BackgroundImageLayout = ImageLayout.Center;
            panelMinButton.Cursor = Cursors.Hand;
            panelMinButton.Location = new Point(792, 6);
            panelMinButton.Name = "panelMinButton";
            panelMinButton.Size = new Size(20, 20);
            panelMinButton.TabIndex = 2;
            panelMinButton.Click += panelButton_Click;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(187, 187, 187);
            ClientSize = new Size(869, 557);
            Controls.Add(panelMinButton);
            Controls.Add(panelMaxButton);
            Controls.Add(panelCloseButton);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Administrator";
            ResumeLayout(false);
        }

        #endregion

        private Panel panelCloseButton;
        private Panel panelMaxButton;
        private Panel panelMinButton;
    }
}