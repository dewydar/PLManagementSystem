using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Helpers.Helpers
{
    public class PaginationResponseModel
    {
        public PaginationResponseModel(int PageIndex, object ReturnData, int PageSize = 0, int TotalItemCount = 0, int TotalPagesCount = 0)
        {
            this.PageIndex = PageIndex;
            this.ReturnData = ReturnData;
            this.PageSize = PageSize;
            this.TotalItemCount = TotalItemCount;
            this.TotalPagesCount = TotalPagesCount;
        }
        public PaginationResponseModel()
        {

        }
        public int PageIndex { get; set; }
        public object ReturnData { get; set; }
        public int PageSize { get; set; } = 0;
        public int TotalItemCount { get; set; } = 0;
        public int TotalPagesCount { get; set; } = 0;
        public string SortBy { get; set; } = "asc";
        public string SortColumn { get; set; } = "Id";

    }
}
