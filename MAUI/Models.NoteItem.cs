namespace MAUI;

public class NoteItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public DateTime DateTime { get; set; } = DateTime.Now;
}
