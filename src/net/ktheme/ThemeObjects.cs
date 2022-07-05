using System.CommandLine;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace KTheme;


public class Meta
{

    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("scheme")] public string Scheme { get; set; } = "";
}

public class Theme
{
    [JsonPropertyName("meta")] public Meta Meta { get; set; } = new();
    // [JsonPropertyName("themeValues")] public Dictionary<string, Dictionary<string, string>> ThemeValues { get; set; } = new Dictionary<string, Dictionary<string, string>>();
    [JsonPropertyName("themeValues")] public ThemeValues ThemeValues { get; set; } = new();
}

public class ThemeValues
{
    [JsonPropertyName("root")] public Settings Root { get; set; } = new();
    [JsonPropertyName("toolbar")] public Settings Toolbar { get; set; } = new();
    [JsonPropertyName("tabsbar")] public Settings TabsBar { get; set; } = new();
}

public class Settings : Dictionary<string, string>
{
    public Settings() : base()
    {
    }
}

// public record class ThemeValues(
//     Dictionary<string,string>  
// );