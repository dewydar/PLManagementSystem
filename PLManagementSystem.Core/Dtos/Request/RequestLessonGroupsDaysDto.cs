using PLManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Dtos.Request
{
    public class RequestLessonGroupsDaysDto
    {
        public int Id { get; set; }
        public int LessonGroupId { get; set; }
        public int DayId { get; set; }
        public TimeOnly Time { get; set; }
        public bool IsDeleted { get; set; }
    }
}
