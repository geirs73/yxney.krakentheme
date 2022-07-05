// using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace KTheme;



public class CmdThemeConfig
{
    public FileInfo TemplateFile { get; set; }
    public FileInfo? File { get; set; }
    public CmdThemeConfig()
    {
        var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        TemplateFile = new FileInfo(@$"{localAppDataPath}\.gitkraken\themes\dark.jsonc-default");
    }
}


public static class CmdLineSymbols
{
    public static List<Symbol> RootSymbols => new()
    {
        new Argument<FileInfo?>("file", "Config file for kraken theme" ),
        new Option<FileInfo?>(new[]{"--template-file", "-t"}, "Template file for kraken theme (default dark.jsonc-default)" )
    };
}
