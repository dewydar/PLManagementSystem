using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Dtos.Response
{
    public class ResponseLessonGroupsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public double Price { get; set; }
        public int OrderNo { get; set; }
    }
}
