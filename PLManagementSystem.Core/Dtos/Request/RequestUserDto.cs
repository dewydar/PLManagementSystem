using System.ComponentModel.DataAnnotations;

namespace PLManagementSystem.Core.Dtos.Request
{
    public class RequestUserDto
    {
        public int Id { get; set; }
        [StringLength(20)]
        [Required]
        public string Name { get; set; }
        [StringLength(20)]
        [Required]
        public string UserName { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

    }
}
