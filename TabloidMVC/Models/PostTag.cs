using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    public class PostTag
    {
        public List<Tag> AddedTags { get; set; }
        public List<Tag> Tags { get; set; }
        public List<int> AddTagIds { get; set; }
        public List<int> DeleteTagIds { get; set; }
    }
}
