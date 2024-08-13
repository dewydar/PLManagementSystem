namespace PLManagementSystem.Core.Dtos.Request
{
    public class RequestLessonGroupsDaysDto
    {
        public int Id { get; set; }
        public int LessonGroupId { get; set; }
        public int DayId { get; set; }
        public TimeOnly Time { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
