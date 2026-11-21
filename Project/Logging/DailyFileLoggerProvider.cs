using System.Collections.Concurrent;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Project.Logging;

/// <summary>
/// Writes application log entries to one UTF-8 file per local calendar day.
/// </summary>
public sealed class DailyFileLoggerProvider : ILoggerProvider, ISupportExternalScope
{
    private readonly string _logDirectory;
    private readonly object _writeLock = new();
    private readonly ConcurrentDictionary<string, DailyFileLogger> _loggers = new();
    private IExternalScopeProvider _scopeProvider = new LoggerExternalScopeProvider();
    private int _writeFailureReported;

    public DailyFileLoggerProvider(string contentRootPath)
    {
        _logDirectory = Path.Combine(contentRootPath, "Logs");
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, name => new DailyFileLogger(this, name));
    }

    public void SetScopeProvider(IExternalScopeProvider scopeProvider)
    {
        _scopeProvider = scopeProvider;
    }

    public void Dispose()
    {
        _loggers.Clear();
    }

    private void Write(string category, LogLevel level, EventId eventId, string message, Exception? exception)
    {
        var now = DateTimeOffset.Now;
        var entry = new StringBuilder()
            .Append(now.ToString("yyyy-MM-dd HH:mm:ss.fff zzz"))
            .Append(" [").Append(level).Append("] ")
            .Append(category);

        if (eventId.Id != 0 || !string.IsNullOrWhiteSpace(eventId.Name))
        {
            entry.Append(" [EventId: ").Append(eventId.Id);
            if (!string.IsNullOrWhiteSpace(eventId.Name))
                entry.Append('/').Append(eventId.Name);
            entry.Append(']');
        }

        entry.Append(" - ").Append(message);
        _scopeProvider.ForEachScope(static (scope, builder) =>
        {
            builder.AppendLine().Append("    => ").Append(scope);
        }, entry);

        if (exception is not null)
            entry.AppendLine().Append(exception);

        try
        {
            var filePath = Path.Combine(_logDirectory, $"app-{now:yyyy-MM-dd}.log");
            lock (_writeLock)
            {
                Directory.CreateDirectory(_logDirectory);
                File.AppendAllText(filePath, entry.AppendLine().ToString(), new UTF8Encoding(false));
            }
        }
        catch (Exception writeException) when (writeException is IOException or UnauthorizedAccessException)
        {
            // Keep logging failures from taking down request handling. The default console
            // provider remains available as a fallback, and this warning is reported once.
            if (Interlocked.Exchange(ref _writeFailureReported, 1) == 0)
                Console.Error.WriteLine($"File logging is unavailable: {writeException.Message}");
        }
    }

    private sealed class DailyFileLogger : ILogger
    {
        private readonly DailyFileLoggerProvider _provider;
        private readonly string _category;

        public DailyFileLogger(DailyFileLoggerProvider provider, string category)
        {
            _provider = provider;
            _category = category;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _provider._scopeProvider.Push(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            _provider.Write(_category, logLevel, eventId, formatter(state, exception), exception);
        }
    }
}
