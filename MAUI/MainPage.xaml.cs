using System.Collections.ObjectModel;

namespace MAUI;

public partial class MainPage : ContentPage
{
    public ObservableCollection<NoteItem> VisibleNotes { get; } = [];

    private string _currentDateLabel = string.Empty;

    public string CurrentDateLabel
    {
        get => _currentDateLabel;
        set
        {
            if (_currentDateLabel == value)
            {
                return;
            }

            _currentDateLabel = value;
            OnPropertyChanged();
        }
    }

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CurrentDateLabel = AppSettings.CurrentDate.ToString("dd.MM.yyyy");
        ApplyFilter();
    }

    private async void OnCreateClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NoteEditorPage));
    }

    private async void OnSettingsClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    private async void OnEditClicked(object? sender, EventArgs e)
    {
        if (sender is not Button { CommandParameter: Guid noteId })
        {
            return;
        }

        var parameters = new Dictionary<string, object>
        {
            ["NoteId"] = noteId.ToString()
        };

        await Shell.Current.GoToAsync(nameof(NoteEditorPage), parameters);
    }

    private async void OnDeleteClicked(object? sender, EventArgs e)
    {
        if (sender is not Button { CommandParameter: Guid noteId })
        {
            return;
        }

        var confirmed = await DisplayAlert("Удаление", "Удалить заметку?", "Да", "Нет");

        if (!confirmed)
        {
            return;
        }

        NoteStorage.Delete(noteId);
        ApplyFilter();
    }

    private void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilter(e.NewTextValue);
    }

    private void ApplyFilter(string? searchText = null)
    {
        var query = (searchText ?? string.Empty).Trim();
        var allNotes = NoteStorage.GetAll();

        var filtered = string.IsNullOrWhiteSpace(query)
            ? allNotes
            : allNotes.Where(note =>
                note.Title.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                note.Text.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                .ToList();

        VisibleNotes.Clear();

        foreach (var note in filtered)
        {
            VisibleNotes.Add(note);
        }
    }
}
