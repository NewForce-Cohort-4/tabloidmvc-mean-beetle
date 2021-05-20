using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    public class PostTag
    {
        public List<Tag> Tags { get; set; }
        public List<int> TagIds { get; set; }
    }
}
