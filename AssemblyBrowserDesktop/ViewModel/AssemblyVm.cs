using AssemblyBrowserDesktop.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AssemblyBrowserDesktop.ViewModel
{
    public class AssemblyVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnAssemblyPathChanged([CallerMemberName] string path = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(path));
        }

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnAssemblyPathChanged();
            }
        }


        public ICommand Browse
        {
            get
            {
                return new ClickCommand((obj) =>
                {
                    var dialog = new AssemblyOpenFileDialog();
                    dialog.OpenFile();
                    FilePath = dialog.FileName;
                    var browser = new Browser();
                    Nodes = new ObservableCollection<TreeNode>(browser.Browse(FilePath));
                }, (obj) => true);
            }
        }

        private ObservableCollection<TreeNode> _nodes;

        public ObservableCollection<TreeNode> Nodes
        {
            get => _nodes;
            set
            {
                _nodes = value;
                OnAssemblyBrowsed();
            }
        }

        public void OnAssemblyBrowsed([CallerMemberName] string path = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(path));
        }
    }
}