using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    interface ICommentRepository
    {
        public List<Comment> GetCommentsByPostId(int postId);
    }
}
