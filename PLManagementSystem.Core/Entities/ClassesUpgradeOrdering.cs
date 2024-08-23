using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLManagementSystem.Core.Entities
{
    public class ClassesUpgradeOrdering : BaseClass
    {
        [Key, Column(Order = 0)]
        public int LowerClassId { get; set; }
        [ForeignKey(name: nameof(LowerClassId))]
        [DeleteBehavior(DeleteBehavior.ClientSetNull)]
        public Entities.Class LowerClass { get; set; }
        [Key, Column(Order = 2)]
        public int UpperClassId { get; set; }
        [ForeignKey(name: nameof(UpperClassId))]
        [DeleteBehavior(DeleteBehavior.ClientSetNull)]
        public Entities.Class UpperClass { get; set; }
    }
}
