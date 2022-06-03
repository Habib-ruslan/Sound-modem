using System.Runtime.Versioning;

namespace Transmitter;

[SupportedOSPlatform("windows")]
public class Transmitter
{
    private const string PathTo0 = "0";
    private const string PathTo1 = "1";
    
    private Player _player;
    private List<string> _data;

    public Transmitter(Player player, List<string> data)
    {
        this._player = player;
        this._data = data;
    }

    public void Start()
    {
        this.PlayStartSound();
        foreach (var value in this._data.SelectMany(currentByte => currentByte))
        {
            Console.WriteLine(value);
            switch (value)
            {
                case '0':
                    this.PlayZeroBitSound();
                    break;
                case '1':
                    this.PlayOneBitSound();
                    break;
            }
            Task.Delay(1500).Wait();
            this._player.Stop();
        }
    }

    private void PlayStartSound()
    {
        this._player.Play(PathTo1);
    }

    private void PlayZeroBitSound()
    {
        this._player.Play(PathTo0);
    }
    
    private void PlayOneBitSound()
    {
        this._player.Play(PathTo1);
    }
}