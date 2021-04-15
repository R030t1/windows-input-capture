using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PInvoke.User32;
using static PInvoke.Kernel32;

namespace _10_csharp_winforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg) {
                case (int)WindowMessage.WM_CREATE:
                    AllocConsole();
                    break;
                /*
                case (int)WindowMessage.WM_MOUSEMOVE:
                case (int)WindowMessage.WM_MOUSEWHEEL:
                case (int)WindowMessage.WM_LBUTTONDOWN:
                case (int)WindowMessage.WM_LBUTTONUP:
                case (int)WindowMessage.WM_KEYDOWN:
                case (int)WindowMessage.WM_KEYUP:
                default:
                    Console.WriteLine((WindowMessage)m.Msg);
                    break;
                */
            }
            Console.WriteLine(m.HWnd);
            base.WndProc(ref m);
        }
    }
}
