using SQLite;

namespace MAUI;

[Table("Notes")]
public class NoteItem
{
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();

    [NotNull]
    public string Title { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public DateTime ScheduledAt { get; set; } = DateTime.Now;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
