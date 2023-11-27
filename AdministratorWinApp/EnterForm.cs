using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdministratorWinApp
{
    public partial class EnterForm : Form
    {
        public EnterForm()
        {
            InitializeComponent();
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0084)
            {
                m.Result = (IntPtr)2;
                return;
            }
            base.WndProc(ref m);
        }

        private void labelButton_Click(object sender, EventArgs e)
        {
            switch (((Label)(sender)).Name)
            {
                case "labelEnterButton":
                    break;
                case "labelCancelButton":
                    Application.Exit();
                    break;
            }
        }
    }
}
