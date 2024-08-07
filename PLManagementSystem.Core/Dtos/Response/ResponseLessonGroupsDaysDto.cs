using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Dtos.Response
{
    public class ResponseLessonGroupsDaysDto
    {
        public int Id { get; set; }
        public string LessonGroupName { get; set; }
        public string DayName { get; set; }
        public TimeOnly Time { get; set; }
    }
}
