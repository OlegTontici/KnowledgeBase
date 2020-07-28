using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KnowledgeBase.SmartThoughtsEditor
{
    public class SmartThoughtsEditorControlDataContext : INotifyPropertyChanged
    {
        private readonly Action<SmartThought> saveAction;
        private readonly Resources.Tags _tags;

        public SmartThoughtsEditorControlDataContext(Action<SmartThought> saveAction, Resources.Tags tags)
        {
            this.saveAction = saveAction;
            _tags = tags;
        }

        private readonly ICommand saveSmartThoughtCommand;
        public ICommand SaveSmartThoughtCommand
        {
            get { return saveSmartThoughtCommand ?? (new CommandExecutor(() => 
            {                
                saveAction(SmartThoughtUnderEdit);
            })); }
        }

        private ICommand closeSmartThoughtsEditorControlCommand;

        public ICommand CloseSmartThoughtsEditorControlCommand
        {
            get
            {
                return closeSmartThoughtsEditorControlCommand ?? (closeSmartThoughtsEditorControlCommand = 
                    new CommandExecutor(() => 
                    {
                        CloseRequired?.Invoke(this, null);
                    }));
            }
        }

        private ICommand editSmartThoughtTags;

        public ICommand EditSmartThoughtTags
        {
            get
            {
                return editSmartThoughtTags ?? (editSmartThoughtTags = new CommandExecutor(() => 
                {
                    var dataContext = new SmartThoughtTagsEditorDataContext(_tags, smartThoughtUnderEdit.Tags.Select(x => new Tag(x)).ToList(), tags => 
                    {                        
                        SmartThoughtUnderEdit.Tags = tags.Select(x => x.Value).ToList();
                        SmartThoughtTagCloseRequired?.Invoke(this, null);
                        SmartThoughtUnderEdit = SmartThoughtUnderEdit;
                    });
                    SmartThoughtTagEditRequired.Invoke(this, new SmartThoughtTagsEditor(dataContext));
                }));
            }
        }



        public EventHandler CloseRequired { get; set; }
        public EventHandler<SmartThoughtTagsEditor> SmartThoughtTagEditRequired { get; set; }
        public EventHandler SmartThoughtTagCloseRequired { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }       

        private SmartThought smartThoughtUnderEdit;

        public SmartThought SmartThoughtUnderEdit
        {
            get { return smartThoughtUnderEdit; }
            set
            {
                smartThoughtUnderEdit = value;
                NotifyPropertyChanged(nameof(SmartThoughtUnderEdit));
            }
        }

        public void Load(SmartThought smartThought)
        {
            SmartThoughtUnderEdit = smartThought;
        }
    }
}
