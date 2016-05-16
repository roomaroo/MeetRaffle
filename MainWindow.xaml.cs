using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Raffle
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
                await this.raffler.StartDraw(TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(70));
            }
        }
    }
}
