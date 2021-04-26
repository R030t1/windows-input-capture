using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static PInvoke.Kernel32;
using static PInvoke.User32;
using static LocalKernel32;
using static LocalUser32;

namespace _30_csharp_rawinput
{
    public unsafe partial class Form1 : Form
    {
        protected int sz;
        protected RawInput *ri;

        public Form1()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            InitializeComponent();
            AllocConsole();

            sz = 8192;
            ri = (RawInput *)Marshal.AllocHGlobal(8192);

            var regs = new RawInputDevice[] {
                new RawInputDevice {
                    UsagePage = HIDUsagePage.Generic,
                    Usage = HIDUsage.Mouse,
                    Flags = RawInputDeviceFlags.InputSink |
                        RawInputDeviceFlags.NoLegacy,
                    WindowHandle = this.Handle
                }
            };

            RegisterRawInputDevices(
                regs, regs.Length,
                Marshal.SizeOf(typeof(RawInputDevice))
            );
        }

        protected void ProcessInput(ref Message m)
        {
            // N.B. this is supposed to be called twice. We
            // preallocate a buffer that should not overflow.
            var rc = GetRawInputData(
                m.LParam,
                RawInputCommand.Input,
                (IntPtr)ri, ref sz,
                Marshal.SizeOf(typeof(RawInputHeader))
            );

            Console.WriteLine(rc + " " + ri->Header.Type);
        }

        protected override void WndProc(ref Message m)
        {
            switch ((WindowMessage)m.Msg) {
                case WindowMessage.WM_INPUT:
                    ProcessInput(ref m);
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
