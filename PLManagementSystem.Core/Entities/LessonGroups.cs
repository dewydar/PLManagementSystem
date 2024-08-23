using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLManagementSystem.Core.Entities
{
    public class LessonGroups : BaseClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        public int ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Classes { get; set; }
        public double Price { get; set; }
        public int OrderNo { get; set; }
        public List<LessonGroupsDays>? LessonGroupsDays { get; set; }
    }
}
