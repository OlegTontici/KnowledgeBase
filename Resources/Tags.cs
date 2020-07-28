using System;
using System.Collections.Generic;

namespace KnowledgeBase.Resources
{
    public class Tags
    {
        private readonly List<Tag> _tags;
        private readonly List<Tag> _newTags;

        public Tags()
        {
            _tags = new List<Tag> { new Tag("DDD"), new Tag("CQRS"), new Tag("Event Sourcing") };
            _newTags = new List<Tag>();
        }
        public List<Tag> GetAll()
        {
            return _tags;
        }

        public void Add(Tag tag)
        {
            _tags.Add(tag);
            _newTags.Add(tag);
        }

        public List<Tag> GetNewTags()
        {
            var tags = new List<Tag>(_newTags);
            _newTags.Clear();
            return tags;
        }
    }
}
