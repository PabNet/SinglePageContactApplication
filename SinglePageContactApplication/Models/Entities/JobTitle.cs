using System.ComponentModel.DataAnnotations.Schema;
using SinglePageContactApplication.RuntimePlugins;

namespace SinglePageContactApplication.Models.Entities
{
    public class JobTitle
    {
        [Column(TypeName = DataTypes.KeyType)]
        public uint Id { get; set; }
        
        [Column(TypeName = DataTypes.JobTitleType)]
        public string Position { get; set; }
    }
}