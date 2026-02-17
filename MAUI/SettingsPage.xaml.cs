namespace MAUI;

public partial class SettingsPage : ContentPage
{
    private readonly List<SettingOption> _colorOptions =
    [
        new("Фиолетовый", "#512BD4"),
        new("Зелёный", "#2E7D32"),
        new("Синий", "#1565C0")
    ];

    private readonly List<SettingOption> _fontOptions =
    [
        new("Open Sans", "OpenSansRegular"),
        new("Open Sans SemiBold", "OpenSansSemibold")
    ];

    public SettingsPage()
    {
        InitializeComponent();

        PrimaryColorPicker.ItemsSource = _colorOptions.Select(option => option.Name).ToList();
        FontFamilyPicker.ItemsSource = _fontOptions.Select(option => option.Name).ToList();

        LoadSettings();
    }

    private void LoadSettings()
    {
        CurrentDatePicker.Date = AppSettings.CurrentDate;

        var selectedColorIndex = _colorOptions.FindIndex(option => option.Value == AppSettings.PrimaryColor);
        PrimaryColorPicker.SelectedIndex = selectedColorIndex >= 0 ? selectedColorIndex : 0;

        var selectedFontIndex = _fontOptions.FindIndex(option => option.Value == AppSettings.FontFamily);
        FontFamilyPicker.SelectedIndex = selectedFontIndex >= 0 ? selectedFontIndex : 0;

        FontSizeSlider.Value = AppSettings.FontSize;
        UpdateFontSizeLabel();
    }

    private void OnFontSizeChanged(object? sender, ValueChangedEventArgs e)
    {
        UpdateFontSizeLabel();
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        var selectedColorIndex = PrimaryColorPicker.SelectedIndex >= 0 ? PrimaryColorPicker.SelectedIndex : 0;
        var selectedFontIndex = FontFamilyPicker.SelectedIndex >= 0 ? FontFamilyPicker.SelectedIndex : 0;

        AppSettings.CurrentDate = CurrentDatePicker.Date;
        AppSettings.PrimaryColor = _colorOptions[selectedColorIndex].Value;
        AppSettings.FontFamily = _fontOptions[selectedFontIndex].Value;
        AppSettings.FontSize = Math.Round(FontSizeSlider.Value, 0);

        AppSettings.ApplyToResources(Application.Current!.Resources);

        await DisplayAlert("Настройки", "Настройки сохранены", "Ок");
        await Shell.Current.GoToAsync("..");
    }

    private void OnResetClicked(object? sender, EventArgs e)
    {
        AppSettings.ResetToDefaults();
        AppSettings.ApplyToResources(Application.Current!.Resources);
        LoadSettings();
    }

    private void UpdateFontSizeLabel()
    {
        FontSizeValueLabel.Text = $"Размер: {Math.Round(FontSizeSlider.Value, 0)}";
    }

    private sealed record SettingOption(string Name, string Value);
}
