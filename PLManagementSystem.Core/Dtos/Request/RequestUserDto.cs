using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Dtos.Request
{
    public class RequestUserDto
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}
