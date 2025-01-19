using System.Text;
using Spectre.Console;

namespace SmokeAndEmbers;

public class GameMain
{
    private static GameMain _instance;
    private static readonly object _lock = new();
    
    private ConsoleWindowMonitor cwm = new ConsoleWindowMonitor();
    
    public bool AlternateBufferPossible { get; private set; }
    public bool UnicodeEnabled { get; private set; }

    private GameMain()
    {
        // Set encoding
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        
        // Check console profile settings
        AlternateBufferPossible = AnsiConsole.Console.Profile.Capabilities.AlternateBuffer;
        UnicodeEnabled = AnsiConsole.Console.Profile.Capabilities.Unicode;
        
        // Add event handler for periodically checking window size for updates
        cwm.WindowSizeChanged += (sender, e) =>
        {
            // TODO: Handle window size update
        };
    }

    public static GameMain Instance
    {
        get
        {
            lock (_lock)
            {
                _instance ??= new GameMain();
                return _instance;
            }
        }
    }

    public bool Run(out string output)
    {
        output = null;

        bool outputGiven = false;
        string tOutput = "";
        
        if (AlternateBufferPossible)
        {
            AnsiConsole.AlternateScreen(() =>
            {
                outputGiven = ShowMenu(out tOutput);
            });
            output = tOutput;
        }
        else
        {
            outputGiven = ShowMenu(out tOutput);
            output = tOutput;
        }

        // Return false if no output was given
        return outputGiven;
    }

    private bool ShowMenu(out string output)
    {
        // TODO: Show menu
        throw new NotImplementedException("do the menu PLS");
        
        return false;
    }
}