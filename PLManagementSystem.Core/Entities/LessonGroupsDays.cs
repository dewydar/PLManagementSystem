using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Entities
{
    public class LessonGroupsDays
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int LessonGroupId { get; set; }
        [ForeignKey(nameof(LessonGroupId))]
        public LessonGroups LessonGroup { get; set; }
        public int DayId { get; set; }
        [ForeignKey(nameof(DayId))]
        public Day Dayes { get; set; }
        public TimeOnly Time { get; set; }
    }
}
