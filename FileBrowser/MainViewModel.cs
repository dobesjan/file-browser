using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FileBrowser.Models;
using System.Collections.ObjectModel;
using System.IO;

    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System.Collections.ObjectModel;
    using System.IO;

namespace FileBrowser
{
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<TabViewModel> _tabs;
        public ObservableCollection<TabViewModel> Tabs
        {
            get => _tabs;
            set => SetProperty(ref _tabs, value);
        }

        private TabViewModel _selectedTab;
        public TabViewModel SelectedTab
        {
            get => _selectedTab;
            set => SetProperty(ref _selectedTab, value);
        }

        public IRelayCommand AddNewTabCommand { get; }
        public IRelayCommand RemoveCurrentTabCommand { get; }

        public MainViewModel()
        {
            Tabs = new ObservableCollection<TabViewModel>();
            AddNewTabCommand = new RelayCommand(AddNewTab);
            RemoveCurrentTabCommand = new RelayCommand(RemoveCurrentTab);
            AddNewTab();
        }

        private void AddNewTab()
        {
            var tab = new TabViewModel();
            Tabs.Add(tab);
            SelectedTab = tab;
        }

        private void RemoveCurrentTab()
        {
            if (SelectedTab != null)
            {
                if (Tabs != null && Tabs.Any())
                {
                    Tabs.Remove(SelectedTab);
                    SelectedTab = Tabs.Count > 0 ? Tabs[0] : null;
                }
            }
        }

        public bool HasSelectedTab()
        {
            return SelectedTab != null;
        }
    }
}