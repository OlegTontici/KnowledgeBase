using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.SmartThoughtsEditor
{
    public class SmartThoughtTagsEditorDataContext : PropertyChangedNotifier
    {
        private List<Tag> _tags;
        public ObservableCollection<Tag> UsedTags { get; set; }
        public ObservableCollection<Tag> AvailableTags { get; set; }

        public SmartThoughtTagsEditorDataContext(List<Tag> tags, List<Tag> usedTags)
        {
            _tags = tags;
            UsedTags = new ObservableCollection<Tag>(usedTags);
            AvailableTags = new ObservableCollection<Tag>(tags.Where(x => !usedTags.Any(y => y.Value == x.Value)).ToList());
        }
    }
}
