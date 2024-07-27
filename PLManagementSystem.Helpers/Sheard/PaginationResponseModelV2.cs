namespace PLManagementSystem.Helpers.Sheard
{
    public class PaginationResponseModelV2
    {
        public PaginationResponseModelV2(int PageIndex, object ReturnData, int PageSize = 0, int TotalItemCount = 0, int TotalPagesCount = 0)
        {
            this.PageIndex = PageIndex;
            this.ReturnData = ReturnData;
            this.PageSize = PageSize;
            this.TotalItemCount = TotalItemCount;
            //this.TotalPagesCount = TotalPagesCount;
        }
        public PaginationResponseModelV2()
        {

        }
        public int PageIndex { get; set; }
        public object ReturnData { get; set; }
        public int PageSize { get; set; } = 0;
        public int TotalItemCount { get; set; } = 0;

        public string SortDirection { get; set; } = "asc";
        public string SortColumn { get; set; } = "OrderNo";

        public bool ShowPrevious => PageIndex > 1;
        public bool ShowNext => PageIndex < TotalPagesCount;
        public bool ShowFirst => PageIndex != 1;
        public bool ShowLast => PageIndex != TotalPagesCount;
        public int FirstRowOnPage
        {
            get { return (PageIndex - 1) * PageSize + 1; }
        }
        public int LastRowOnPage
        {
            get { return Math.Min(PageIndex * PageSize, TotalItemCount); }
        }
        public int TotalPagesCount
        {
            get
            {
                if (PageSize > 0 && TotalItemCount > 0)
                {
                    return (int)Math.Ceiling(TotalItemCount / (double)PageSize);
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
