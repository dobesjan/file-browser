using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FileBrowser.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace FileBrowser
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<FileItem> _leftPaneItems;

        [ObservableProperty]
        private ObservableCollection<FileItem> _rightPaneItems;

        [ObservableProperty]
        private string _leftPanePath;

        [ObservableProperty]
        private string _rightPanePath;

        public MainViewModel()
        {
            LeftPaneItems = new ObservableCollection<FileItem>();
            RightPaneItems = new ObservableCollection<FileItem>();

            LeftPanePath = "C:\\";
            RightPanePath = "C:\\";

            LoadFiles(LeftPanePath, true);
            LoadFiles(RightPanePath, false);
        }

        public IRelayCommand<string> LoadLeftPaneCommand => new RelayCommand<string>(path => LoadFiles(path, true));
        public IRelayCommand<string> LoadRightPaneCommand => new RelayCommand<string>(path => LoadFiles(path, false));
        public IRelayCommand<(FileItem, bool)> OpenItemCommand => new RelayCommand<(FileItem, bool)>(tuple => OpenItem(tuple.Item1, tuple.Item2));

        public void LoadFiles(string path, bool isLeftPane)
        {
            var items = new ObservableCollection<FileItem>();
            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);

            foreach (var directory in directories)
            {
                items.Add(new FileItem
                {
                    Name = new DirectoryInfo(directory).Name,
                    Path = directory,
                    IsDirectory = true
                });
            }

            foreach (var file in files)
            {
                items.Add(new FileItem
                {
                    Name = new FileInfo(file).Name,
                    Path = file,
                    IsDirectory = false
                });
            }

            if (isLeftPane)
            {
                LeftPaneItems = items;
                LeftPanePath = path;
            }
            else
            {
                RightPaneItems = items;
                RightPanePath = path;
            }
        }

        public void OpenItem(FileItem item, bool isLeftPane)
        {
            if (item.IsDirectory)
            {
                LoadFiles(item.Path, isLeftPane);
            }
            else
            {
                // Open file with default application
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = item.Path,
                    UseShellExecute = true
                });
            }
        }
    }
}