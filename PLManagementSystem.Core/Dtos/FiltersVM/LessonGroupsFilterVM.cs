using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Dtos.FiltersVM
{
    public class LessonGroupsFilterVM
    {
        public string? name { get; set; }
        public int? classId { get; set; }
        public int? dayId { get; set; }
    }
}
