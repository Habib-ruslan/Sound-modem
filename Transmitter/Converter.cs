using NAudio.Wave;

namespace Transmitter;

public static class Converter
{
    private const string Directory = "../../../";
    public static void ToWav(string path)
    {
        var test = GetFileAsBytes(Directory + path);
        var writer = new WaveFileWriter(Directory + GetFileNameWithoutExtension(path) + ".wav", new WaveFormat(4000, 24, 2));
        writer.Write(test, 0, test.Length);
        writer.Dispose();
    }

    private static byte[] GetFileAsBytes(string filename)
    {
        return File.ReadAllBytes(filename);
    }

    private static string GetFileNameWithoutExtension(string filename)
    {
        var pos = filename.LastIndexOf('.');
        return filename[..pos];
    }
}