using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Wolfgang.Etl.Json.Tests.Unit.TestModels;

/// <summary>
/// Test double that records the level and event id of every log entry, so tests can assert
/// which diagnostic a stage emitted.
/// </summary>
internal sealed class RecordingLogger<T> : ILogger<T>
{
    public List<(LogLevel Level, EventId EventId)> Entries { get; } = new();



    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull => null;



    public bool IsEnabled(LogLevel logLevel) => true;



    public void Log<TState>
    (
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    )
    {
        Entries.Add((logLevel, eventId));
    }
}
