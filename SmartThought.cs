using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace KnowledgeBase
{
    public class SmartThought : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        private string _title;

        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        private string _formattedContent;

        public string FormattedContent
        {
            get { return _formattedContent; }
            set 
            { 
                _formattedContent = value;
                NotifyPropertyChanged(nameof(FormattedContent));
            }
        }
        
        private string _preview;

        public string Preview
        {
            get { return _preview; }
            set 
            { 
                _preview = value;
                NotifyPropertyChanged(nameof(Preview));
            }
        }
        public DateTime DateAdded { get; set; }
        private ObservableCollection<string> _tags;

        public ObservableCollection<string> Tags
        {
            get { return _tags; }
            set 
            { 
                _tags = value;
                NotifyPropertyChanged(nameof(Tags));
            }
        }

        public SmartThought()
        {
            Id = Guid.NewGuid();
            DateAdded = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
