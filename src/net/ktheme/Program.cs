// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;

namespace KTheme;

internal class Program
{
    private static async Task Main(string[] args)
    {

        var rootCmd = new RootCommand("Transform a default Kraken theme to your theme");
        CmdLineSymbols.RootSymbols.ForEach(s => rootCmd.Add(s));
        rootCmd.Handler = CommandHandler.Create<CmdThemeConfig>(c => ThemeProcessor.Execute(c));

        // var transformCmd = new Command("transform", "Transform a default Kraken theme to your theme");
        // transformCmd.Handler = CommandHandler.Create<CmdThemeConfig>(c => ThemeProcessor.Execute(c));
        // rootCmd.AddCommand(transformCmd);


        int res = await rootCmd.InvokeAsync(args);
    }
}