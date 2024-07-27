using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Dtos.Response
{
    public class ResponseUserDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}
