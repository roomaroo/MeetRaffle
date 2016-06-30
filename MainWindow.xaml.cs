using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace MeetRaffle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Raffler raffler;

        public MainWindow()
        {
            this.raffler = new Raffler();

            var dataFile = Environment.GetCommandLineArgs()[1];
            this.raffler.LoadNames(new FileInfo(dataFile));
            this.DataContext = this.raffler;
            InitializeComponent();
        }

        private async void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.raffler.DrawInProgress)
            {
                await this.raffler.StartDraw(TimeSpan.FromSeconds(4), TimeSpan.FromMilliseconds(70), Properties.Resources.DrumRoll);
            }
        }
    }
}
