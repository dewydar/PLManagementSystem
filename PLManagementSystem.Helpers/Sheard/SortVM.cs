using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Helpers.Sheard
{
    public class SortVM
    {
        public string SortDirection { get; set; }
        public string SortColumn { get; set; }
        public List<TableCloumnVM> Columns { get; set; } = new List<TableCloumnVM>();
    }
}
