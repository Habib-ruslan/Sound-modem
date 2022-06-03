using NAudio.Wave;

namespace Receiver;

public class Receiver
{
    private const int RangeAStart = 200;
    private const int RangeBStart = 1200;
    private const int RangeCStart = 3000;
    private const double Threshold = 0.02f;
    private const int SpaceLimit = 5;
        
    private bool isStarting = true;
    private bool isStartRecord;
    private int spaceCount = 0;
    private int count;
    private string data = "";
    
    private WaveInEvent? _waveIn;
    private WaveFileWriter? _writer;
    private string _outputFilename;

    public Receiver(string path)
    {
        this._outputFilename = "../../../" + path;
        this._waveIn = new WaveInEvent();
        this._waveIn.DeviceNumber = 0;
        this._waveIn.DataAvailable += waveIn_DataAvailable;
        this._waveIn.RecordingStopped += waveIn_RecordingStopped;
        this._waveIn.WaveFormat = new WaveFormat(8000, 2);
        this._writer = new WaveFileWriter(_outputFilename, _waveIn.WaveFormat);
    }

    private void waveIn_DataAvailable(object? sender, WaveInEventArgs e)
    {
        if (e == null)
        {
            return;
        }
        this._writer?.WriteData(e.Buffer, 0, e.BytesRecorded);
        
        double sound = 0;
        for (var index = 0; index < e.BytesRecorded; index += 2)
        {
            double tmp = (short)((e.Buffer[index + 1] << 8) | e.Buffer[index + 0]);
            tmp /= 32768.0;
            sound += tmp * tmp;
            if (sound > Threshold)
            {
                this.count++;
            }
            //Console.WriteLine("Debug: " + sound);
        }
    }

    private void waveIn_RecordingStopped(object? sender, StoppedEventArgs stoppedEventArgs)
    {
        _waveIn?.Dispose();
        _waveIn = null;
        _writer?.Close();
        _writer = null;
    }

    public void Start()
    {
        _waveIn?.StartRecording();
        while (this.isStarting)
        {
            this.Update();
            Task.Delay(1200).Wait();
        }
    }

    private void Update()
    {
        if (this.count == 0)
        {
            this.spaceCount++;
            if (this.spaceCount < SpaceLimit) return;
            this.isStartRecord = false;
            this.isStarting = false;
            return;
        }

        this.spaceCount = 0;
        //Проверка сигнала для начала записи данных
        if (this.data == "" && !this.isStartRecord)
        {
            this.isStartRecord = true;
            return;
        }
        switch (this.count)
        {
            case > RangeAStart and < RangeBStart:
                Console.WriteLine("0 byte:" + this.count);
                this.data += '0';
                break;
            case < RangeCStart and >= RangeBStart:
                Console.WriteLine("1 byte:" + this.count);
                this.data += '1';
                break;
        }
        this.count = 0;
    }

    public string GetData()
    {
        var rest = this.data.Length % 8;
        if (rest == 0) return this.data;
        for (var i = 0; i < 8 - rest; i++)
        {
            this.data += "0";
        }
        return this.data;
    }

    public void Stop()
    {
        this.isStarting = false;
        _waveIn?.StopRecording();
    }
}