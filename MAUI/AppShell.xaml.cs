namespace MAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(NoteEditorPage), typeof(NoteEditorPage));
    }
}
