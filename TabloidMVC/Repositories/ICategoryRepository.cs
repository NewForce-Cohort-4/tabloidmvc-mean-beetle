using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        public void AddCategory(Category category);
        Category GetCategorygById(int id);
        public void UpdateCategory(Category category);
        public void DeleteCategory(int Id);
    }
}