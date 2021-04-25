using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static PInvoke.Kernel32;
using static PInvoke.User32;

namespace _20_csharp_llhook
{
    [StructLayout(LayoutKind.Sequential)]
    public class KBDLLHOOKSTRUCT
    {
        public uint vkCode;
        public uint scanCode;
        public KBDLLHOOKSTRUCTFlags flags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }

    [Flags]
    public enum KBDLLHOOKSTRUCTFlags : uint {
        LLKHF_EXTENDED = 0x01,
        LLKHF_INJECTED = 0x10,
        LLKHF_ALTDOWN = 0x20,
        LLKHF_UP = 0x80,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public int mouseData;
        public int flags;
        public int time;
        public UIntPtr dwExtraInfo;
    }

    public partial class Form1 : Form
    {
        SafeHookHandle mouse, keyboard;

        public Form1()
        {
            InitializeComponent();
            AllocConsole();
            mouse = SetWindowsHookEx(WindowsHookType.WH_KEYBOARD_LL, LLKeyboardProc, 
                GetModuleHandle(null), 0);
            keyboard = SetWindowsHookEx(WindowsHookType.WH_MOUSE_LL, LLMouseProc,
                GetModuleHandle(null), 0);
        }

        int LLKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var kbll = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            Console.WriteLine((WindowMessage)wParam + " " + kbll.flags + " " + kbll.dwExtraInfo);
            return CallNextHookEx(keyboard.DangerousGetHandle(), nCode, wParam, lParam);
        }

        int LLMouseProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var msll = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            Console.WriteLine((WindowMessage)wParam + " " + msll.pt.X + " " + msll.pt.Y);
            return CallNextHookEx(keyboard.DangerousGetHandle(), nCode, wParam, lParam);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }
    }
}
