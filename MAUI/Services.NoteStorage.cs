namespace MAUI;

public static class NoteStorage
{
    private static readonly List<NoteItem> Notes =
    [
        new NoteItem
        {
            Title = "Пример заметки",
            Text = "Нажмите «Редактировать», чтобы изменить эту заметку.",
            DateTime = DateTime.Now
        }
    ];

    public static IReadOnlyList<NoteItem> GetAll() => Notes
        .OrderByDescending(note => note.DateTime)
        .ToList();

    public static NoteItem? GetById(Guid id) => Notes.FirstOrDefault(note => note.Id == id);

    public static void Save(NoteItem note)
    {
        var existing = GetById(note.Id);

        if (existing is null)
        {
            Notes.Add(note);
            return;
        }

        existing.Title = note.Title;
        existing.Text = note.Text;
        existing.DateTime = note.DateTime;
    }

    public static void Delete(Guid id)
    {
        var note = GetById(id);

        if (note is not null)
        {
            Notes.Remove(note);
        }
    }
}
