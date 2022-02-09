using System.ComponentModel.DataAnnotations.Schema;
using SinglePageContactApplication.RuntimePlugins;

namespace SinglePageContactApplication.Models.Entities
{
    public class Contacts
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}