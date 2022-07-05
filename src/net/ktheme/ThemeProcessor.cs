using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text.Unicode;
using System.Data;

namespace KTheme;

public class ThemeProcessor
{
    public static Task Execute(CmdThemeConfig config)
    {
        var inFile = config.File ?? throw new ArgumentException("File is null");

        var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var templateFile = File.ReadAllText(config.TemplateFile.FullName);
        var transformFile = inFile.OpenText().ReadToEnd();

        var serOpts = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            WriteIndented = true
        };
        var themeDoc = JsonSerializer.Deserialize<Theme>(templateFile, serOpts) ?? throw new NoNullAllowedException();
        var transformDoc = JsonSerializer.Deserialize<Theme>(transformFile, serOpts) ?? throw new NoNullAllowedException();

        themeDoc.Meta.Name = transformDoc.Meta.Name;
        themeDoc.Meta.Scheme = transformDoc.Meta.Scheme;
        Replace(themeDoc.ThemeValues.Root, transformDoc.ThemeValues.Root);
        Replace(themeDoc.ThemeValues.TabsBar, transformDoc.ThemeValues.TabsBar);
        Replace(themeDoc.ThemeValues.Toolbar, transformDoc.ThemeValues.Toolbar);

        string fileName = transformDoc.Meta.Name;
        foreach (char c in System.IO.Path.GetInvalidFileNameChars())
        {
            fileName = fileName.Replace(c, '_');
        }
        fileName = fileName.Replace(' ', '_');
        fileName += ".json";
        fileName = fileName.ToLowerInvariant();
        string filePath = Path.Combine(@$"{localAppDataPath}\.gitkraken\themes\", fileName);
        var themeString = JsonSerializer.Serialize<Theme>(themeDoc, serOpts);
        File.WriteAllText(filePath, themeString);
        return Task.CompletedTask;
    }

    public static void Replace(Settings target, Settings replace)
    {
        foreach (var entry in target)
        {
            if (replace.ContainsKey(entry.Key))
            {
                target[entry.Key] = replace[entry.Key];
            }
        }
    }
}