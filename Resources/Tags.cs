using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Tag> GetNewTags()
        {
            var tags = new List<Tag>(_newTags);
            _newTags.Clear();
            return tags;
        }

        public void UpsertRange(IList<Tag> tags)
        {
            var tagsToAdd = tags.Where(x => !_tags.Any(y => x.Value == y.Value)).ToList();
            _tags.AddRange(tagsToAdd);
        }
    }
}
