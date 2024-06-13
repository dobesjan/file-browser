using FileBrowser.Models;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LeftPaneItem_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ViewModel.HasSelectedTab() && ((ListView)sender).SelectedItem is FileItem item)
            {
                ViewModel.SelectedTab.OpenItemCommand.Execute((item, true));
            }
        }

        private void RightPaneItem_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ViewModel.HasSelectedTab() && ((ListView)sender).SelectedItem is FileItem item)
            {
                ViewModel.SelectedTab.OpenItemCommand.Execute((item, false));
            }
        }

        private void LeftPanePath_TextChanged(object sender, TextChangedEventArgs args)
        {
            if (ViewModel.HasSelectedTab() && sender is TextBox textBox)
            {
                var newPath = textBox.Text;
                if (Directory.Exists(newPath))
                {
                    ViewModel.SelectedTab.LoadLeftPaneCommand.Execute(newPath);
                }
            }
        }

        private void RightPanePath_TextChanged(object sender, TextChangedEventArgs args)
        {
            if (ViewModel.HasSelectedTab() && sender is TextBox textBox)
            {
                var newPath = textBox.Text;
                if (Directory.Exists(newPath))
                {
                    ViewModel.SelectedTab.LoadRightPaneCommand.Execute(newPath);
                }
            }
        }
    }
}