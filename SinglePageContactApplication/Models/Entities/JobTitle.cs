using System.ComponentModel.DataAnnotations.Schema;
using SinglePageContactApplication.RuntimePlugins;

namespace SinglePageContactApplication.Models.Entities
{
    [Table(DataBaseComponentNames.JobTitleTable)]
    public class JobTitle
    {
        [Column(TypeName = DataTypes.KeyType)]
        public uint Id { get; set; }
        
        [Column(TypeName = DataTypes.JobTitleType)]
        public string Position { get; set; }
    }
}