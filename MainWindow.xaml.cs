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

namespace Factorio.Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FactorioInstallation FactorioInstance;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FactorioInstance = new FactorioInstallation(@"E:\games\factorio");
            FactorioPathLabel.Content = FactorioInstance.FactorioPath;
            foreach (var dir in FactorioInstance.ModDirectoryList)
            {
                var dinfo = new DirectoryInfo(dir);

                ModDirListBox.Items.Add(dinfo.Name);
            }
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            LaunchSelected();
        }

        private void ModDirListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LaunchButton.IsEnabled = true;
        }

        private void ModDirListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LaunchButton.IsEnabled)
            {
                LaunchSelected();
            }
        }

        private void LaunchSelected()
        {
            var dinfo = (string)ModDirListBox.SelectedItems[0];
            FactorioInstance.Launch(dinfo);
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            
            var diag = new InputDialog("Create new mod-directory:","mods-folder");

            if (diag.ShowDialog() == true)
            {
                FactorioInstance.Launch(diag.Answer);
            }
        }
    }
}
