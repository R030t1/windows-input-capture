using System;
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
            AllocConsole();
        }

        protected override void WndProc(ref Message m)
        {
            Console.WriteLine((WindowMessage)m.Msg);
            base.WndProc(ref m);
        }
    }
}
