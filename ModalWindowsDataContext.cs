using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KnowledgeBase
{
    public class ModalWindowsDataContext : PropertyChangedNotifier
    {
        private bool _isModalOpen;
        public bool IsModalOpen
        {
            get
            {
                return _isModalOpen;
            }
            set
            {
                _isModalOpen = value;
                NotifyPropertyChanged(nameof(IsModalOpen));
            }
        }

        private ICommand _closeModalCommand;
        public ICommand CloseModalCommand
        {
            get
            {
                return _closeModalCommand ?? (_closeModalCommand = new CommandExecutor(() =>
                {
                    IsModalOpen = false;
                }));
            }
        }

        private FrameworkElement _modalContent;
        public FrameworkElement ModalContent
        {
            get
            {
                return _modalContent;
            }
            set
            {
                _modalContent = value;
                NotifyPropertyChanged(nameof(ModalContent));
            }
        }

        public ModalWindowsDataContext()
        {
            
        }

        public void ShowModalContent(FrameworkElement content)
        {
            ModalContent = content;
            IsModalOpen = true;
        }

        public void HideModal()
        {
            IsModalOpen = false;
        }
    }
}
