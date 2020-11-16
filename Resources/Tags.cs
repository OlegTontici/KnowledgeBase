using KnowledgeBase.Persistence.Sql;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBase.Resources
{
    public class Tags
    {
        public Tags()
        {
           
        }
        public List<Tag> GetAll()
        {
            return KnowledgeBaseDataContext.GetAllTags().Select(x => new Tag { DateAdded = x.DateAdded, Value = x.Value }).ToList();
        }

        public void UpsertRange(IList<Tag> tags)
        {
            KnowledgeBaseDataContext.UpsertRangeTag(tags.Select(x => new Persistence.Sql.Tag { Value = x.Value, DateAdded = x.DateAdded }).ToList());
        }
    }
}
