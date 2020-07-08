using System;

namespace KnowledgeBase
{
    public class Tag
    {
        public string Value { get; set; }
        public DateTime DateAdded { get; set; }

        public Tag(string value)
        {
            Value = value;
            DateAdded = DateTime.Now;
        }
    }
}
