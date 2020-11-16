using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace KnowledgeBase.SmartThoughtsPreview
{
    public class SmartThoughtsPreviewControlDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<SmartThought> smartThoughts;

        public ObservableCollection<SmartThought> SmartThoughts
        {
            get { return smartThoughts; }
            set
            {
                smartThoughts = value;
                NotifyPropertyChanged(nameof(SmartThoughts));
            }
        }
        public SmartThoughtsPreviewControlDataContext(ObservableCollection<SmartThought> smartThoughts)
        {
            SmartThoughts = smartThoughts;
        }

        private ICommand selectSmartThoughtCommand;

        public ICommand SelectSmartThoughtCommand
        {
            get
            {
                return selectSmartThoughtCommand ?? (selectSmartThoughtCommand =
                    new CommandExecutor<SmartThought>(st =>
                    {
                        SmartThoughtSelected?.Invoke(this, st);
                    }));
            }
        }

        public EventHandler<SmartThought> SmartThoughtSelected { get; set; }
    }
}
