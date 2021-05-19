using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        public List<Tag> GetAllTags();
        public void Add(Tag tag);
        public Tag GetTagById(int tagId);

        public void DeleteTag(int tagId);
        public void UpdateTag(Tag tag);
        public void AddTagToPost(int postId, List<int> tagIds);
    }
}
