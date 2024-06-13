using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FileBrowser.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser
{
    public class TabViewModel : ObservableObject
    {
        private ObservableCollection<FileItem> _leftPaneItems;
        public ObservableCollection<FileItem> LeftPaneItems
        {
            get => _leftPaneItems;
            set => SetProperty(ref _leftPaneItems, value);
        }

        private ObservableCollection<FileItem> _rightPaneItems;
        public ObservableCollection<FileItem> RightPaneItems
        {
            get => _rightPaneItems;
            set => SetProperty(ref _rightPaneItems, value);
        }

        private string _leftPanePath;
        public string LeftPanePath
        {
            get => _leftPanePath;
            set => SetProperty(ref _leftPanePath, value);
        }

        private string _rightPanePath;
        public string RightPanePath
        {
            get => _rightPanePath;
            set => SetProperty(ref _rightPanePath, value);
        }

        private FileItem _selectedLeftPaneItem;
        public FileItem SelectedLeftPaneItem
        {
            get => _selectedLeftPaneItem;
            set => SetProperty(ref _selectedLeftPaneItem, value);
        }

        private FileItem _selectedRightPaneItem;
        public FileItem SelectedRightPaneItem
        {
            get => _selectedRightPaneItem;
            set => SetProperty(ref _selectedRightPaneItem, value);
        }

        public string Title => Path.GetFileName(LeftPanePath);

        public IRelayCommand<string> LoadLeftPaneCommand { get; }
        public IRelayCommand<string> LoadRightPaneCommand { get; }
        public IRelayCommand<(FileItem, bool)> OpenItemCommand { get; }

        public TabViewModel()
        {
            LeftPaneItems = new ObservableCollection<FileItem>();
            RightPaneItems = new ObservableCollection<FileItem>();

            LeftPanePath = "C:\\";
            RightPanePath = "C:\\";

            LoadLeftPaneCommand = new RelayCommand<string>(path => LoadFiles(path, true));
            LoadRightPaneCommand = new RelayCommand<string>(path => LoadFiles(path, false));
            OpenItemCommand = new RelayCommand<(FileItem, bool)>(tuple => OpenItem(tuple.Item1, tuple.Item2));

            LoadFiles(LeftPanePath, true);
            LoadFiles(RightPanePath, false);
        }

        private void LoadFiles(string path, bool isLeftPane)
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
                OnPropertyChanged(nameof(Title));
            }
            else
            {
                RightPaneItems = items;
                RightPanePath = path;
            }
        }

        private void OpenItem(FileItem item, bool isLeftPane)
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
