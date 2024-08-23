using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLManagementSystem.Core.Entities
{
    public class Class : BaseClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [StringLength(60)]
        public string Name { get; set; }
        [StringLength(20)]
        public string? ShortName { get; set; }
        public int OrderNo { get; set; }
    }
}
