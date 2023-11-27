namespace AdministratorWinApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0084)
            {
                if ((Cursor.Position.X - Location.X) > (this.Width - 7) && (Cursor.Position.Y - Location.Y) > (this.Height - 7))
                {
                    m.Result = (IntPtr)17;
                }
                else if ((Cursor.Position.X - Location.X) < 7)
                {
                    m.Result = (IntPtr)10;
                }
                else if ((Cursor.Position.X - Location.X) > (this.Width - 7))
                {
                    m.Result = (IntPtr)11;
                }
                else if ((Cursor.Position.Y - Location.Y) < 7)
                {
                    m.Result = (IntPtr)12;
                }
                else if ((Cursor.Position.Y - Location.Y) > (this.Height - 7))
                {
                    m.Result = (IntPtr)15;
                }

                else
                {
                    m.Result = (IntPtr)2;
                }
                return;
            }
            base.WndProc(ref m);
        }

        private void panelButton_Click(object sender, EventArgs e)
        {
            switch (((Panel)(sender)).Name)
            {
                case "panelCloseButton":
                    Application.Exit();
                    break;
                case "panelMaxButton":
                    WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
                    break;
                case "panelMinButton":
                    WindowState = FormWindowState.Minimized;
                    break;
            }
        }
    }
}