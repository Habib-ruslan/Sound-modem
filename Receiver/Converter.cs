namespace Receiver;

public static class Converter
{
    public static string GetTextFromBytes(string bytes)
    {
        var list = new List<byte>();
        for (var i = 0; i < bytes.Length; i+=8)
        {
            var substr = bytes.Substring(i, 8);
            list.Add((byte) Int32.Parse(substr));
        }
        var text = System.Text.Encoding.UTF8.GetString(list.ToArray());
        return text;
    }
}