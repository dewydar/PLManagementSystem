
namespace PLManagementSystem.Helpers.Sheard
{
    public class TableCloumnVM
    {
        public string ColumnName { get; set; }
        public string ColumnDisplayName { get; set; }
        public bool IsShow { get; set; } = true;
        public bool IsSortable { get; set; } = true;
    }
}
