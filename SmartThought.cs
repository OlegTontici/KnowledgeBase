using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBase
{
    public class SmartThought
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FormattedContent { get; set; }
        public string Preview { get; set; }
        public DateTime DateAdded { get; set; }
        public IList<string> Tags { get; set; }

        public SmartThought()
        {
            Id = Guid.NewGuid();
            DateAdded = DateTime.Now;
        }

        public SmartThought(string title, string formattedContent, string preview, IList<string> tags) : this()
        {
            Title = title;
            FormattedContent = formattedContent;
            Preview = preview;
            Tags = tags;
        }
    }
}
