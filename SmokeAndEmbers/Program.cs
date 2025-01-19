using System.Text;
using Spectre.Console;

namespace SmokeAndEmbers;

class Program
{
    static void Main(string[] args)
    {
        string finalOutput = "";
        if (GameMain.Instance.Run(out finalOutput))
        {
            AnsiConsole.MarkupLine(finalOutput);
        }
    }
}
