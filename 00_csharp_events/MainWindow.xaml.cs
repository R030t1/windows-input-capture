using System;
using System.Windows;
using System.Windows.Input;
using static PInvoke.Kernel32;

namespace _00_csharp_events
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs rea)
        {
            AllocConsole();
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs mea)
        {
            Console.WriteLine("mouse");
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs kea)
        {
            Console.WriteLine("key down");
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs kea)
        {
            Console.WriteLine("key up");
        }
    }
}
