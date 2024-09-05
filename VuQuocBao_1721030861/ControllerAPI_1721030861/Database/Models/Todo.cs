namespace ControllerAPI_1721030861.Database.Models
{
    public class Todo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public string? Secret { get; set; }
    }
}
