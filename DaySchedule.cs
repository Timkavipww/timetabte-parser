public class DaySchedule
{
    public required string Day { get; set; }
    public List<List<string>> Items { get; set; } = new List<List<string>>();
}
