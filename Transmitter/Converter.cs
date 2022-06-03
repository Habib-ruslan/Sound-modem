namespace Transmitter;

public static class Converter
{
    private const string Directory = "../../../";

    public static List<string> ToBinary(string path)
    {
        var binary = new List<string>();
        var test = GetFileAsBytes(Directory + path);
        foreach (var item in test)
        {
            var currentByte = Convert.ToString(item, 2);
            currentByte = GetFullByte(currentByte);
            binary.Add(GetFullByte(currentByte));
            Console.WriteLine(currentByte);
        }

        return binary;
    }

    private static string GetFullByte(string b)
    {
        var result = "";
        for (var i = 0; i < 8 - b.Length; i++)
        {
            result += '0';
        }
        result += b;
        return result;
    }

    private static byte[] GetFileAsBytes(string filename)
    {
        return File.ReadAllBytes(filename);
    }
}