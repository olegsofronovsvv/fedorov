using System.Text;

namespace GameConcept.Logging;

internal sealed class Logger : IDisposable
{
    private readonly StreamWriter _writer;

    public Logger(string filePath)
    {
        _writer = new StreamWriter(filePath, false, new UTF8Encoding(true));
        _writer.AutoFlush = true;
    }

    public void Write(string message)
    {
        _writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
    }

    public void Show(string message)
    {
        Console.WriteLine(message);
        Write(message);
    }

    public void Dispose()
    {
        _writer.Dispose();
    }
}
