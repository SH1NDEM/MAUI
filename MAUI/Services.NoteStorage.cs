using SQLite;

namespace MAUI;

public static class NoteStorage
{
    private static readonly Lazy<SQLiteConnection> Connection = new(CreateConnection);

    private static SQLiteConnection Db => Connection.Value;

    private static SQLiteConnection CreateConnection()
    {
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "notes.db3");
        var connection = new SQLiteConnection(databasePath);
        connection.CreateTable<NoteItem>();
        return connection;
    }

    public static IReadOnlyList<NoteItem> GetAll() => Db.Table<NoteItem>()
        .OrderByDescending(note => note.ScheduledAt)
        .ToList();

    public static NoteItem? GetById(Guid id) => Db.Find<NoteItem>(id);

    public static void Save(NoteItem note)
    {
        var now = DateTime.Now;
        var existing = GetById(note.Id);

        if (existing is null)
        {
            note.CreatedAt = now;
            note.UpdatedAt = now;
            Db.Insert(note);
            return;
        }

        note.CreatedAt = existing.CreatedAt;
        note.UpdatedAt = now;
        Db.Update(note);
    }

    public static void Delete(Guid id)
    {
        Db.Delete<NoteItem>(id);
    }
}
