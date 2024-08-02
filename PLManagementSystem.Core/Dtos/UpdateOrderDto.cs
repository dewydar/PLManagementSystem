using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Dtos
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public int MaxOrderNo { get; set; }
        public int OrderNo { get; set; }
    }
}
