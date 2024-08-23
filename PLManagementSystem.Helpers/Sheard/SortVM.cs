namespace PLManagementSystem.Helpers.Sheard
{
    public class SortVM
    {
        public string SortDirection { get; set; }
        public string SortColumn { get; set; }
        public List<TableCloumnVM> Columns { get; set; } = new List<TableCloumnVM>();
    }
}
