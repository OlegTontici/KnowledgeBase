using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KnowledgeBase.SmartThoughtsEditor
{
    public class SmartThoughtTagsEditorDataContext : PropertyChangedNotifier
    {
        private List<Tag> _tags;
        private readonly Resources.Tags _tagresource;
        private readonly Action<List<Tag>> _saveTagsAction;
        public ObservableCollection<Tag> UsedTags { get; set; }
        public ObservableCollection<Tag> AvailableTags { get; set; }

        public SmartThoughtTagsEditorDataContext(Resources.Tags tags, List<Tag> usedTags, Action<List<Tag>> saveTagsAction)
        {
            _tagresource = tags;
            _tags = tags.GetAll();
            UsedTags = new ObservableCollection<Tag>(usedTags);
            AvailableTags = new ObservableCollection<Tag>(_tags.Where(x => !usedTags.Any(y => y.Value == x.Value)).ToList());
            _saveTagsAction = saveTagsAction;
        }

        private ICommand useTagCommand;

        public ICommand UseTagCommand
        {
            get { return useTagCommand ?? (useTagCommand = new CommandExecutor<Tag>(tag => 
            {
                AvailableTags.Remove(tag);
                UsedTags.Add(tag);
            })); }
        }


        private ICommand stopUsingTagCommand;

        public ICommand StopUsingTagCommand
        {
            get
            {
                return stopUsingTagCommand ?? (stopUsingTagCommand = new CommandExecutor<Tag>(tag =>
                {
                    UsedTags.Remove(tag);
                    AvailableTags.Add(tag);
                }));
            }
        }

        private string tagSearchText;

        public string TagSearchText
        {
            get { return tagSearchText; }
            set
            {
                if (value != tagSearchText)
                {
                    tagSearchText = value;
                    NotifyPropertyChanged(nameof(TagSearchText));

                    AvailableTags.Clear();

                    if (string.IsNullOrWhiteSpace(tagSearchText))
                    {
                        var tags = _tags.Where(x => !UsedTags.Any(y => y.Value == x.Value)).ToList();
                        foreach (var t in tags)
                        {
                            AvailableTags.Add(t);
                        }
                    }
                    else
                    {
                        var tags = _tags.Except(UsedTags).Where(t => t.Value.IndexOf(tagSearchText, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
                        foreach (var t in tags)
                        {
                            AvailableTags.Add(t);
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

        private ICommand addNewTagCommand;

        public ICommand AddNewTagCommand
        {
            get { return addNewTagCommand ?? (addNewTagCommand = new CommandExecutor<string>(newTag => 
            {
                if (!string.IsNullOrWhiteSpace(newTag))
                {
                    var tag = new Tag(newTag);
                    AvailableTags.Add(tag);
                    _tags.Add(tag);
                    _tagresource.Add(tag);
                }
            })); }
        }

        private ICommand _saveSmartThoughtTagsCommand;

        public ICommand SaveSmartThoughtTagsCommand
        {
            get { return _saveSmartThoughtTagsCommand ?? (_saveSmartThoughtTagsCommand = new CommandExecutor(() => 
            {
                _saveTagsAction(UsedTags.ToList());
            })); }
        }

    }
}
