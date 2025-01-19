using System.Text;
using Spectre.Console;

namespace SmokeAndEmbers;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        bool alternateBufferEnabled = AnsiConsole.Console.Profile.Capabilities.AlternateBuffer;
        bool unicodeEnabled = AnsiConsole.Console.Profile.Capabilities.Unicode;

        string executionOutput = null;
        bool hasExecutionOutput = false;

        if (alternateBufferEnabled)
        {
            AnsiConsole.AlternateScreen(() =>
            {
                hasExecutionOutput = MenuFlow(alternateBufferEnabled, unicodeEnabled, out executionOutput);
            });
        }
        else
        {
            hasExecutionOutput = MenuFlow(alternateBufferEnabled, unicodeEnabled, out executionOutput);
        }

        if (hasExecutionOutput)
        {
            AnsiConsole.MarkupLine(executionOutput);
        }
    }

    static bool MenuFlow(bool abEnabled, bool unicodeEnabled, out string outputMessage)
    {
        outputMessage = null;
        
        // AnsiConsole.Console.Profile.Width

        ConsoleWindowMonitor cwm = new ConsoleWindowMonitor();
        cwm.WindowSizeChanged += (sender, e) =>
        {
            AnsiConsole.MarkupLine("New: {0} x {1} (from {2} x {3})", e.NewWidth, e.NewHeight, e.OldWidth, e.OldHeight);
        };

        AnsiConsole.Prompt(new TextPrompt<string>("..."));

        return false;
    }

    static string GetCurrentTimestamp()
    {
        return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
    }
}
