using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Entities
{
    public class LessonGroups:BaseClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Classes { get; set; }
        public int DayId { get; set; }
        [ForeignKey(nameof(DayId))]
        public Day Dayes { get; set; }
        public TimeOnly Time { get; set; }
    }
}
