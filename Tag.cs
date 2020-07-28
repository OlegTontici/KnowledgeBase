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

        public override bool Equals(object obj)
        {
            return obj != null && obj is Tag tag && Value == tag.Value && DateAdded == tag.DateAdded;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
