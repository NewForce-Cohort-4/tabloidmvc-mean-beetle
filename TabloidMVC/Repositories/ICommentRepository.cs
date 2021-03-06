using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        public List<Comment> GetCommentsByPostId(int postId);
        public void AddComment(Comment comment);
        public Comment GetCommentById(int id);
        public void DeleteComment(int id);
    }
}
