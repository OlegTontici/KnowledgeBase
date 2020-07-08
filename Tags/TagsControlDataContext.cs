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
        private readonly List<string> tags = new List<string> { "DDD" ,"CQRS" ,"Event Sourcing" };

        public ObservableCollection<string> AvailableTags { get; set; }
        public ObservableCollection<string> UsedTags { get; set; }

        public TagsControlDataContext()
        {
            AvailableTags = new ObservableCollection<string>(tags);
            UsedTags = new ObservableCollection<string>();
        }

        private ICommand selectTag;
        public ICommand SelectTag
        {
            get
            {
                return selectTag ?? (selectTag = new CommandExecutor<string>(tag => 
                {
                    var selectedTag = AvailableTags.FirstOrDefault(x => x == tag);
                    AvailableTags.Remove(selectedTag);

                    UsedTags.Add(selectedTag);

                    TagSelected?.Invoke(this, selectedTag);
                }));
            }
        }

        private ICommand removeTag;
        public ICommand RemoveTag
        {
            get
            {
                return removeTag ?? (removeTag = new CommandExecutor<string>(tag =>
                {
                    var selectedTag = UsedTags.FirstOrDefault(x => x == tag);
                    UsedTags.Remove(selectedTag);

                    AvailableTags.Add(selectedTag);

                    TagRemoved?.Invoke(this, selectedTag);
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

                    AvailableTags.Clear();

                    if (string.IsNullOrWhiteSpace(tagSearchText))
                    {
                        var usedTags = UsedTags.ToList();
                        foreach (var tag in tags.Except(usedTags).ToList())
                        {
                            AvailableTags.Add(tag);
                        }
                    }
                    else
                    {
                        var usedTags = UsedTags.ToList();
                        foreach (var tag in tags.Except(usedTags).Where(t => t.IndexOf(tagSearchText, StringComparison.InvariantCultureIgnoreCase) != -1).ToList())
                        {
                            AvailableTags.Add(tag);
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
    }
}
