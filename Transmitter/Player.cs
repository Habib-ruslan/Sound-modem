using System.Media;
using System.Runtime.Versioning;

namespace Transmitter;

[SupportedOSPlatform("windows")]
public class Player
{
    private const string Directory = "../../../";
    
    private readonly SoundPlayer _player;
    
    public Player()
    {
        this._player = new SoundPlayer();
    }

    public void Play(string path)
    {
        this._player.SoundLocation = Directory + path + ".wav";
        this._player.Play();
        this._player.PlaySync();
    }

    public void Stop()
    {
        this._player.Stop();
    }
}