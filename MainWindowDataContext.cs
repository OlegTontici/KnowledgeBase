using KnowledgeBase.Resources;
using KnowledgeBase.SmartThoughtsEditor;
using KnowledgeBase.SmartThoughtsPreview;
using KnowledgeBase.Tags;
using KnowlegdeBase.SmartThoughtsPreview;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KnowledgeBase
{
    public class MainWindowDataContext : INotifyPropertyChanged
    {
        private readonly SmartThoughtsEditorControl smartThoughtsEditorControl;
        private readonly SmartThoughtsPreviewControl smartThoughtsPreviewControl;

        private readonly ObservableCollection<SmartThought> smartThoughts;

        private readonly List<string> usedTags;

        private readonly Resources.Tags _tags;

        private FrameworkElement mainContent;
        public FrameworkElement MainContent
        {
            get{ return mainContent; }
            set
            {
                mainContent = value;
                NotifyPropertyChanged(nameof(MainContent));
            }
        }

        private FrameworkElement tagsControl;

        public FrameworkElement TagsControl
        {
            get { return tagsControl; }
            set
            {
                tagsControl = value;
                NotifyPropertyChanged(nameof(TagsControl));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool shouldTagsBeShown = true;
        public bool ShouldTagsBeShown
        {
            get { return shouldTagsBeShown; }
            set
            {
                shouldTagsBeShown = value;
                NotifyPropertyChanged(nameof(ShouldTagsBeShown));
            }
        }

        public ModalWindow ModalWindow { get; set; }


        public MainWindowDataContext()
        {
            _tags = new Resources.Tags();

            var modalWidowDataContext = new ModalWindowsDataContext();
            ModalWindow = new ModalWindow(modalWidowDataContext);


            smartThoughts = new ObservableCollection<SmartThought>(SmartThoughts.GetAll());

            usedTags = new List<string>();

            var tagsControlDataContext = new TagsControlDataContext(_tags);
            TagsControl = new TagsControl(tagsControlDataContext);

           

            var smartThoughtsEditorControlDataContext = new SmartThoughtsEditorControlDataContext(st => 
            {
                SmartThoughts.Upsert(st);
            }, _tags);
            smartThoughtsEditorControl = new SmartThoughtsEditorControl(smartThoughtsEditorControlDataContext);


            var smartThoughtsPreviewControlDataContext = new SmartThoughtsPreviewControlDataContext(smartThoughts);
            smartThoughtsPreviewControl = new SmartThoughtsPreviewControl(smartThoughtsPreviewControlDataContext);            

            MainContent = smartThoughtsPreviewControl;

            smartThoughtsPreviewControlDataContext.SmartThoughtSelected += (s, a) =>
            {
                smartThoughtsEditorControlDataContext.Load(a);
                MainContent = smartThoughtsEditorControl;

                ShouldTagsBeShown = false;
            };

            smartThoughtsEditorControlDataContext.CloseRequired += (s, a) =>
            {
                MainContent = smartThoughtsPreviewControl;

                ShouldTagsBeShown = true;

                tagsControlDataContext.RefreshTags();
            };
            smartThoughtsEditorControlDataContext.SmartThoughtTagEditRequired += (s, a) =>
            {
                modalWidowDataContext.ShowModalContent(a);
            };

            smartThoughtsEditorControlDataContext.SmartThoughtTagCloseRequired += (s, a) =>
            {
                modalWidowDataContext.HideModal();
            };

            tagsControlDataContext.TagSelected += (s, a) =>
            {
                usedTags.Add(a);
                var st = SmartThoughts.GetByTags(usedTags);

                smartThoughts.Clear();

                foreach (var smartThought in st)
                {
                    smartThoughts.Add(smartThought);
                }
            };

            tagsControlDataContext.TagRemoved += (s, a) =>
            {
                usedTags.Remove(a);

                smartThoughts.Clear();

                IList<SmartThought> st = new List<SmartThought>();

                if (usedTags.Any())
                {
                    st = SmartThoughts.GetByTags(usedTags);

                }
                else
                {
                    st = SmartThoughts.GetAll();
                }

                foreach (var smartThought in st)
                {
                    smartThoughts.Add(smartThought);
                }
            };
        }
    }
}
