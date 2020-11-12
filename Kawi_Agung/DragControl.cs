using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Kawi_Agung
{
	class DragControl : Component
	{
        private Control handleControl;

        public Control SelectControl
        {
            get
            {
                return this.handleControl;
            }
            set 
            {
                this.handleControl = value;
                this.handleControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_Form_MouseDown);
            }
        }


        //public const int WM_NCLBUTTONDOWN = 0xA1;
        //public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Drag_Form_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                //SendMessage(, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

                SendMessage(this.SelectControl.FindForm().Handle, 161, 2, 0);
            }
        }
    }
}
