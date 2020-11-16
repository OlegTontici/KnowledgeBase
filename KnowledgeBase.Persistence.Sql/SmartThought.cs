using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeBase.Persistence.Sql
{
    public class SmartThought
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FormattedContent { get; set; }
        public string Preview { get; set; }
        public DateTime DateAdded { get; set; }
        public string Tags { get; set; }
    }
}
