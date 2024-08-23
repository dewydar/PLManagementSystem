using System.ComponentModel.DataAnnotations;

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
