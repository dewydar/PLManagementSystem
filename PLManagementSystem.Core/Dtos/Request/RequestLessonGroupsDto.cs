using System.ComponentModel.DataAnnotations;

namespace PLManagementSystem.Core.Dtos.Request
{
    public class RequestLessonGroupsDto
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        public int ClassId { get; set; }
        public double Price { get; set; }
        public int OrderNo { get; set; }
        public bool IsDeleted { get; set; }

    }
}
