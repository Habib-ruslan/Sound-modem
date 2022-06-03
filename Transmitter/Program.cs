using System.Runtime.Versioning;

namespace Transmitter;

[SupportedOSPlatform("windows")]
public static class Program
{
    public static void Main()
    {
        var player = new Player();
        var result = Converter.ToBinary("newText.txt");
        Console.WriteLine("\n" + result.Count);
        var transmitter = new Transmitter(player, result);
        transmitter.Start();
    }
}