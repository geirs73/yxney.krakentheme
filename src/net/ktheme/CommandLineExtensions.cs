using System.CommandLine;

namespace KTheme;

public static class CommandLineExtensions {
    public static void Add(this Command command, Symbol symbol)
    {
        dynamic s = symbol;
        command.Add(s);
    }
}