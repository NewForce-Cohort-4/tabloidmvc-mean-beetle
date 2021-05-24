using System.ComponentModel;

namespace TabloidMVC.Models
{
    public class UserType
    {
        public int Id { get; set; }
        [DisplayName("User Profile Type")]
        public string Name { get; set; }
    }
}