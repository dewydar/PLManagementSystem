using PLManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Dtos.Request
{
    public class RequestLessonGroupsDto
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        public int ClassId { get; set; }
        public double Price { get; set; }
        public int OrderNo { get; set; }
        public bool IsDeleted { get; set; }

    }
}
