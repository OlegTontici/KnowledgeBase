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
        private List<Tag> _tags;
        private readonly Resources.Tags _tagsResource;

        public ObservableCollection<Tag> Tags { get; set; }

        public TagsControlDataContext(Resources.Tags tags)
        {
            _tags = tags.GetAll().Select(x => new Tag { Value = x.Value }).ToList();
            Tags = new ObservableCollection<Tag>(_tags);
            _tagsResource = tags;
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
              

        private string tagSearchText = string.Empty;

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
                        foreach (var tag in _tags)
                        {
                            Tags.Add(tag);
                        }
                    }
                    else
                    {
                        foreach (var tag in _tags.Where(t => t.Value.IndexOf(tagSearchText, StringComparison.InvariantCultureIgnoreCase) != -1).ToList())
                        {
                            Tags.Add(tag);
                        }
                    }
                }
            }
        }

        private ICommand clearTagSearchText;

        public ICommand ClearTagSearchText
        {
            get
            {
                return clearTagSearchText ?? (clearTagSearchText = new CommandExecutor(() =>
                {
                    TagSearchText = string.Empty;
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public EventHandler<string> TagSelected { get; set; }
        public EventHandler<string> TagRemoved { get; set; }

        public void RefreshTags()
        {
            _tags = _tagsResource.GetAll().Select(x => new Tag { Value = x.Value }).ToList();
            Tags.Clear();

            foreach (var t in _tags.Where(t => t.Value.IndexOf(tagSearchText, StringComparison.InvariantCultureIgnoreCase) != -1).ToList())
            {
                Tags.Add(t);
            }
        }

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
