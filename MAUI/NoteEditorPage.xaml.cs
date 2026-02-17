namespace MAUI;

public partial class NoteEditorPage : ContentPage, IQueryAttributable
{
    private Guid? _noteId;

    public NoteEditorPage()
    {
        InitializeComponent();
        SetDefaultValues();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _noteId = null;

        if (query.TryGetValue("NoteId", out var noteIdValue) &&
            Guid.TryParse(noteIdValue?.ToString(), out var parsedId))
        {
            _noteId = parsedId;
        }

        LoadNote();
    }

    private void SetDefaultValues()
    {
        var baseDate = AppSettings.CurrentDate;
        DatePicker.Date = baseDate;
        TimePicker.Time = DateTime.Now.TimeOfDay;
    }

    private void LoadNote()
    {
        if (_noteId is null)
        {
            Title = "Новая заметка";
            TitleEntry.Text = string.Empty;
            TextEditor.Text = string.Empty;
            SetDefaultValues();
            return;
        }

        var note = NoteStorage.GetById(_noteId.Value);

        if (note is null)
        {
            return;
        }

        Title = "Редактирование";
        TitleEntry.Text = note.Title;
        TextEditor.Text = note.Text;
        DatePicker.Date = note.ScheduledAt.Date;
        TimePicker.Time = note.ScheduledAt.TimeOfDay;
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        var title = TitleEntry.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(title))
        {
            await DisplayAlert("Ошибка", "Название не может быть пустым", "Ок");
            return;
        }

        var dateTime = DatePicker.Date + TimePicker.Time;

        var note = new NoteItem
        {
            Id = _noteId ?? Guid.NewGuid(),
            Title = title,
            Text = TextEditor.Text?.Trim() ?? string.Empty,
            ScheduledAt = dateTime
        };

        NoteStorage.Save(note);
        await Shell.Current.GoToAsync("..");
    }
}
