using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace CarnGo
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _headerBarVisibility;
        public string HeaderBarVisibility
        {
            get => _headerBarVisibility;
            set
            {
                _headerBarVisibility = value;
                OnPropertyChanged(nameof(HeaderBarVisibility));
            }
        }
        private ICommand _loadingCommand;
        private bool _loadingFlag;

        public bool LoadingFlag
        {
            get => _loadingFlag;
            set
            {
                if (_loadingFlag == value)
                    return;
                _loadingFlag = value;
                OnPropertyChanged(nameof(LoadingFlag));
            }
        }

        public ICommand LoadingCommand
        {
            get => _loadingCommand ?? new DelegateCommand(async () =>
            {
                if (LoadingFlag)
                    return;

                LoadingFlag = true;
                await Task.Delay(2000);
                LoadingFlag = false;
            });
            private set => _loadingCommand = value;
        }
    }
}