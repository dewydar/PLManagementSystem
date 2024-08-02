using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Dtos.Request
{
    public class RequestClassDto
    {
        public int Id { get; set; }
        [StringLength(60)]
        public string Name { get; set; }
        [StringLength(20)]
        public string? ShortName { get; set; }
        public int OrderNo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
