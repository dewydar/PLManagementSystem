using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Dtos.Request
{
    public class RequestDayDto
    {
        public int Id { get; set; }
        [StringLength(20)]
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
