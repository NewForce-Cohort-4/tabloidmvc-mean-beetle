using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostCommentsViewModel
    {
        public List<Comment> Comment { get; set; }
        public Post Post { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
