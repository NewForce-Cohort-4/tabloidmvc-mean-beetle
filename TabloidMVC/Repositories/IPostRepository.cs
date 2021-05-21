using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        public void DeletePost(int postId);
        public void UpdatePost(Post post);
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id);
        List<Post> GetAllUserPostById(int userProfileId);
        Post GetUserPostById(int id, int userProfileId);
        public Post GetPostById(int id);
    }
}