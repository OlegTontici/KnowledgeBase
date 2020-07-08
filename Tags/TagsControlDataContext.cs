using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KnowledgeBase.Tags
{
    public class TagsControlDataContext : INotifyPropertyChanged
    {
        private readonly List<Tag> tags = new List<Tag> { new Tag { Value = "DDD" }, new Tag { Value = "CQRS" } , new Tag { Value = "Event Sourcing" } };

        public ObservableCollection<Tag> Tags { get; set; }

        public TagsControlDataContext()
        {
            Tags = new ObservableCollection<Tag>(tags);
        }

        private ICommand selectTag;
        public ICommand SelectTag
        {
            get
            {
                return selectTag ?? (selectTag = new CommandExecutor<Tag>(tag => 
                {
                    tag.IsSelected = !tag.IsSelected;

                    if (tag.IsSelected)
                    {
                        TagSelected?.Invoke(this, tag.Value);
                    }
                    else
                    {
                        TagRemoved?.Invoke(this, tag.Value);
                    }                    
                }));
            }
        }
              

        private string tagSearchText;

        public string TagSearchText
        {
            get { return tagSearchText; }
            set
            {
                if(value != tagSearchText)
                {
                    tagSearchText = value;
                    NotifyPropertyChanged(nameof(TagSearchText));

                    Tags.Clear();

                    if (string.IsNullOrWhiteSpace(tagSearchText))
                    {
                        var usedTags = Tags.Where(t => t.IsSelected).ToList();
                        foreach (var tag in tags.Except(usedTags).ToList())
                        {
                            Tags.Add(tag);
                        }
                    }
                    else
                    {
                        var usedTags = Tags.Where(t => t.IsSelected).ToList();
                        foreach (var tag in tags.Except(usedTags).Where(t => t.Value.IndexOf(tagSearchText, StringComparison.InvariantCultureIgnoreCase) != -1).ToList())
                        {
                            Tags.Add(tag);
                        }
                    }
                }
            }
        }

        private ICommand clearTagSearchText;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ClearTagSearchText
        {
            get { return clearTagSearchText ?? (clearTagSearchText = new CommandExecutor(() => 
            {
                TagSearchText = string.Empty;
            })); }
        }


        public EventHandler<string> TagSelected { get; set; }
        public EventHandler<string> TagRemoved { get; set; }

        public class Tag : INotifyPropertyChanged
        {
            public string Value { get; set; }
            private bool isSelected;

            public bool IsSelected
            {
                get { return isSelected; }
                set 
                { 
                    isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}
