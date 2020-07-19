using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KnowledgeBase.SmartThoughtsEditor
{
    public class SmartThoughtsEditorControlDataContext : INotifyPropertyChanged
    {
        private readonly Action<SmartThought> saveAction;

        public SmartThoughtsEditorControlDataContext(Action<SmartThought> saveAction)
        {
            this.saveAction = saveAction;
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
                    SmartThoughtTagEditRequired.Invoke(this, new SmartThoughtTagsEditor());
                }));
            }
        }



        public EventHandler CloseRequired { get; set; }
        public EventHandler<SmartThoughtTagsEditor> SmartThoughtTagEditRequired { get; set; }



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
