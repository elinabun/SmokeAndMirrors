using Spectre.Console;

namespace SmokeAndEmbers;

public class ConsoleWindowSizeChangedEventArgs : EventArgs
{
    public int OldWidth { get; set; }
    public int OldHeight { get; set; }
    public int NewWidth { get; set; }
    public int NewHeight { get; set; }
    
    public int OldAnsiWidth { get; set; }
    public int OldAnsiHeight { get; set; }
    public int NewAnsiWidth { get; set; }
    public int NewAnsiHeight { get; set; }
}

public class ConsoleWindowMonitor
{
    public event EventHandler<ConsoleWindowSizeChangedEventArgs> WindowSizeChanged;

    private int _lastWidth;
    private int _lastHeight;

    private int _lastAnsiWidth;
    private int _lastAnsiHeight;

    private readonly Timer _timer;

    private const int _checkInterval = 100;

    public ConsoleWindowMonitor()
    {
        _lastWidth = Console.WindowWidth;
        _lastHeight = Console.WindowHeight;
        _lastAnsiWidth = AnsiConsole.Console.Profile.Width;
        _lastAnsiHeight = AnsiConsole.Console.Profile.Height;

        _timer = new Timer(CheckForChanges, null, 0, _checkInterval);
    }

    private void CheckForChanges(object state)
    {
        int currentWidth = Console.WindowWidth;
        int currentHeight = Console.WindowHeight;
        int currentAnsiWidth = AnsiConsole.Console.Profile.Width;
        int currentAnsiHeight = AnsiConsole.Console.Profile.Height;

        if (currentWidth != _lastWidth || currentHeight != _lastHeight || currentAnsiWidth != _lastAnsiWidth || currentAnsiHeight != _lastAnsiHeight)
        {
            WindowSizeChanged?.Invoke(this, new ConsoleWindowSizeChangedEventArgs
            {
                OldWidth = _lastWidth,
                OldHeight = _lastHeight,
                NewWidth = currentWidth,
                NewHeight = currentHeight,
                
                OldAnsiWidth = _lastAnsiWidth,
                OldAnsiHeight = _lastAnsiHeight,
                NewAnsiWidth = currentAnsiWidth,
                NewAnsiHeight = currentAnsiHeight,
            });

            _lastWidth = currentWidth;
            _lastHeight = currentHeight;
            _lastAnsiWidth = currentAnsiWidth;
            _lastAnsiHeight = currentAnsiHeight;
        }
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}