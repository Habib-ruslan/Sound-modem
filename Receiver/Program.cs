namespace Receiver;

public static class Program
{
    public static void Main()
    {
        var filename = "demo.wav";
        var receiver = new Receiver(filename);
        Console.WriteLine("Начать запись?");
        Console.Read();
        receiver.Start();
        var data = receiver.GetData();
        var text = Converter.GetTextFromBytes(data);
        Console.WriteLine(text);
        Console.WriteLine("Нажмите Enter чтобы остановить запись...");
        Console.ReadKey();
        receiver.Stop();
    }
}