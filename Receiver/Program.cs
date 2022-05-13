namespace Receiver;

public static class Program
{
    public static void Main()
    {
        Converter.ToTxt("test.wav");
        var result = File.ReadAllText("../../../test.txt");
        Console.WriteLine(result);
    }
}