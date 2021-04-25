using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static PInvoke.Kernel32;
using static PInvoke.User32;
using static LocalKernel32;
using static LocalUser32;

namespace _40_csharp_dummy
{
    public unsafe partial class Form1 : Form
    {
        protected SafeHookHandle llms, llkb;
        protected int sz;
        protected RawInput *ri;


        public Form1()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            AllocConsole();

            llms = SetWindowsHookEx(WindowsHookType.WH_KEYBOARD_LL, LLKeyboardProc, 
                GetModuleHandle(null), 0);
            llkb = SetWindowsHookEx(WindowsHookType.WH_MOUSE_LL, LLMouseProc,
                GetModuleHandle(null), 0);

            sz = 8192;
            ri = (RawInput *)Marshal.AllocHGlobal(8192);

            var regs = new RawInputDevice[] {
                new RawInputDevice {
                    UsagePage = HIDUsagePage.Generic,
                    Usage = HIDUsage.Mouse,
                    Flags = RawInputDeviceFlags.InputSink |
                        RawInputDeviceFlags.NoLegacy,
                    WindowHandle = this.Handle
                },
                new RawInputDevice {
                    UsagePage = HIDUsagePage.Generic,
                    Usage = HIDUsage.Keyboard,
                    Flags = RawInputDeviceFlags.InputSink |
                        RawInputDeviceFlags.NoLegacy,
                    WindowHandle = this.Handle
                }
            };

            RegisterRawInputDevices(
                regs, regs.Length,
                Marshal.SizeOf(typeof(RawInputDevice))
            );

            InitializeComponent();
        }

        int LLKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var kb = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(
                lParam, typeof(KBDLLHOOKSTRUCT));
            Console.WriteLine("a");
            
            return CallNextHookEx(
                llkb.DangerousGetHandle(),
                nCode, wParam, lParam
            );
        }

        int LLMouseProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var ms = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(
                lParam, typeof(MSLLHOOKSTRUCT));
            Console.WriteLine("b");
            
            return CallNextHookEx(
                llms.DangerousGetHandle(),
                nCode, wParam, lParam
            );
        }

        void RawInputProc(ref Message m)
        {
            Console.WriteLine("c");
        }

        protected override void WndProc(ref Message m)
        {
            switch ((WindowMessage)m.Msg) {
                case WindowMessage.WM_INPUT:
                    RawInputProc(ref m);
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
