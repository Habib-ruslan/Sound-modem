namespace Receiver;

public static class Program
{
    public static void Main()
    {
        var recorder = new Recorder("test2.wav");
        Console.WriteLine("Начать запись?");
        Console.Read();
        recorder.Start();
        Console.WriteLine("Нажмите Enter чтобы остановить запись...");
        Console.ReadKey();
        recorder.Stop();
        Converter.ToTxt("test.wav");
        var result = File.ReadAllText("../../../test.txt");
        Console.WriteLine(result);
    }
}