using System;
using System.Collections.Generic;

namespace KnowledgeBase.Resources
{
    public class Tags
    {
        public List<Tag> GetAll()
        {
            return new List<Tag> { new Tag("DDD"), new Tag("CQRS"), new Tag("Event Sourcing") };
        }

        internal void Add(Tag tag)
        {
           
        }
    }
}
