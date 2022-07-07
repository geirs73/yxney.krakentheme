// using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace KTheme;



public class CmdThemeConfig
{
    private string _krakenProfileDirectory;
    public string TemplateFile { get; set; }
    public FileInfo? File { get; set; }
    public CmdThemeConfig()
    {
        var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _krakenProfileDirectory = Path.Combine(@$"{localAppDataPath}", ".gitkraken", "themes");
        TemplateFile = "dark.jsonc-default";
    }

    public string TemplateFilePath => GetFullKrakenThemePath(TemplateFile);

    private string GetFullKrakenThemePath(string profileFileName)
    {
        if (!System.IO.File.Exists(profileFileName))
        {
            var fullPath = Path.Combine(_krakenProfileDirectory, profileFileName);
            if (!System.IO.File.Exists(fullPath))
                throw new FileNotFoundException(fullPath);
            return fullPath;
        } else
            return profileFileName;
    }
}


public static class CmdLineSymbols
{
    public static List<Symbol> RootSymbols => new()
    {
        new Argument<FileInfo?>("file", "Config file for kraken theme" ),
        new Option<string?>(new[]{"--template-file", "-t"}, "Template file for kraken theme (default dark.jsonc-default)" ),
    };
}
