using System.Runtime.Versioning;

namespace Transmitter;

[SupportedOSPlatform("windows")]
public static class Program
{
    public static void Main()
    {
        Converter.ToWav("test.txt");
        var player = new Player();
        player.Play("test");
    }
}