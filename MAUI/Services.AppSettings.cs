namespace MAUI;

public static class AppSettings
{
    private const string CurrentDateKey = "settings.currentDate";
    private const string PrimaryColorKey = "settings.primaryColor";
    private const string FontFamilyKey = "settings.fontFamily";
    private const string FontSizeKey = "settings.fontSize";

    public const string DefaultPrimaryColor = "#512BD4";
    public const string DefaultFontFamily = "OpenSansRegular";
    public const double DefaultFontSize = 14;

    public static DateTime CurrentDate
    {
        get => Preferences.Default.Get(CurrentDateKey, DateTime.Today);
        set => Preferences.Default.Set(CurrentDateKey, value.Date);
    }

    public static string PrimaryColor
    {
        get => Preferences.Default.Get(PrimaryColorKey, DefaultPrimaryColor);
        set => Preferences.Default.Set(PrimaryColorKey, value);
    }

    public static string FontFamily
    {
        get => Preferences.Default.Get(FontFamilyKey, DefaultFontFamily);
        set => Preferences.Default.Set(FontFamilyKey, value);
    }

    public static double FontSize
    {
        get => Preferences.Default.Get(FontSizeKey, DefaultFontSize);
        set => Preferences.Default.Set(FontSizeKey, value);
    }

    public static void ResetToDefaults()
    {
        CurrentDate = DateTime.Today;
        PrimaryColor = DefaultPrimaryColor;
        FontFamily = DefaultFontFamily;
        FontSize = DefaultFontSize;
    }

    public static void ApplyToResources(ResourceDictionary resources)
    {
        resources["AppPrimaryColor"] = Color.FromArgb(PrimaryColor);
        resources["AppFontFamily"] = FontFamily;
        resources["AppFontSize"] = FontSize;
    }
}
