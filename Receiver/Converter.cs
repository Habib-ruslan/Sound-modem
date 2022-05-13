namespace Receiver;

public static class Converter
{
    private const string Directory = "../../../";
    public static void ToTxt(string path)
    {
        var test = File.ReadAllBytes(Directory + path);
        var text = System.Text.Encoding.UTF8.GetString(test);
        File.WriteAllText(Directory + GetFileNameWithoutExtension(path) + ".txt", text);
    }

    private static string GetFileNameWithoutExtension(string filename)
    {
        var pos = filename.LastIndexOf('.');
        return filename[..pos];
    }
}