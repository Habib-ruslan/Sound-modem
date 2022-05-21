using CSCore;
using CSCore.Codecs.WAV;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;

namespace Receiver;

public class Recorder
{
    private string _output;
    private WasapiCapture _capture;
    private IWaveSource _source;
    private WaveWriter _writer;

    public Recorder(string filename = "output.wav")
    {
        this._output = filename;
        this._capture = new WasapiCapture();
        this._capture.Device = FindMicrophone();;
        this._capture.Initialize();
    }
    
    public void Start()
    {
        var soundInSource = new SoundInSource(this._capture);
        var singleBlockNotificationStream = new SingleBlockNotificationStream(soundInSource.ToSampleSource());
        this._source = singleBlockNotificationStream.ToWaveSource();
    
        this._writer = new WaveWriter("../../../" + this._output, this._source.WaveFormat);
    
        var buffer = new byte[this._source.WaveFormat.BytesPerSecond / 2];
        soundInSource.DataAvailable += (s, e) =>
        {
            int read;
            while ((read = this._source.Read(buffer, 0, buffer.Length)) > 0)
            {
                this._writer.Write(buffer, 0, read);
            }
        };
        
        this._capture.Start();
    }

    public void Stop()
    {
        this._capture.Stop();
        this._capture.Dispose();
        this._source.Dispose();
        this._writer.Dispose();
    }

    private static MMDevice FindMicrophone()
    {
        var deviceEnumerator = new MMDeviceEnumerator();
        var deviceCollection = deviceEnumerator.EnumAudioEndpoints(DataFlow.Capture, DeviceState.Active);
        return deviceCollection[0];
    }
}