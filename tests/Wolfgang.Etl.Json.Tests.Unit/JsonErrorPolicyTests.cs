using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Wolfgang.Etl.Abstractions;
using Wolfgang.Etl.Json;
using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

public sealed class JsonErrorPolicyTests
{
    private static ItemErrorContext Context() =>
        new(1, new JsonException("bad"), () => "raw");



    [Fact]
    public void Skip_returns_Skip()
    {
        Assert.Equal
        (
            ItemErrorAction.Skip,
            JsonErrorPolicy.Skip(Context())
        );
    }



    [Fact]
    public void Abort_returns_Abort()
    {
        Assert.Equal
        (
            ItemErrorAction.Abort,
            JsonErrorPolicy.Abort(Context())
        );
    }



    [Fact]
    public void SkipAndLog_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => JsonErrorPolicy.SkipAndLog(null!));
    }



    [Fact]
    public void SkipAndLog_logs_the_failure_and_returns_Skip()
    {
        var logger = new CapturingLogger();

        var action = JsonErrorPolicy.SkipAndLog(logger)(Context());

        Assert.Equal(ItemErrorAction.Skip, action);
        Assert.Single(logger.Entries);
    }



    [Fact]
    public void SkipAndDeadLetter_collection_when_deadLetters_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => JsonErrorPolicy.SkipAndDeadLetter((ICollection<ItemErrorContext>)null!)
        );
    }



    [Fact]
    public void SkipAndDeadLetter_collection_records_the_failure_and_returns_Skip()
    {
        var deadLetters = new List<ItemErrorContext>();
        var context = Context();

        var action = JsonErrorPolicy.SkipAndDeadLetter(deadLetters)(context);

        Assert.Equal(ItemErrorAction.Skip, action);
        Assert.Same(context, Assert.Single(deadLetters));
    }



    [Fact]
    public void SkipAndDeadLetter_channel_when_deadLetters_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => JsonErrorPolicy.SkipAndDeadLetter((ChannelWriter<ItemErrorContext>)null!)
        );
    }



    [Fact]
    public void SkipAndDeadLetter_channel_writes_the_failure_and_returns_Skip()
    {
        var channel = Channel.CreateUnbounded<ItemErrorContext>();
        var context = Context();

        var action = JsonErrorPolicy.SkipAndDeadLetter(channel.Writer)(context);

        Assert.Equal(ItemErrorAction.Skip, action);
        Assert.True(channel.Reader.TryRead(out var written));
        Assert.Same(context, written);
    }



    [Fact]
    public void SkipDeadLetterAndLog_collection_when_deadLetters_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => JsonErrorPolicy.SkipDeadLetterAndLog((ICollection<ItemErrorContext>)null!, new CapturingLogger())
        );
    }



    [Fact]
    public void SkipDeadLetterAndLog_collection_when_logger_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => JsonErrorPolicy.SkipDeadLetterAndLog(new List<ItemErrorContext>(), null!)
        );
    }



    [Fact]
    public void SkipDeadLetterAndLog_collection_records_logs_and_returns_Skip()
    {
        var deadLetters = new List<ItemErrorContext>();
        var logger = new CapturingLogger();
        var context = Context();

        var action = JsonErrorPolicy.SkipDeadLetterAndLog(deadLetters, logger)(context);

        Assert.Equal(ItemErrorAction.Skip, action);
        Assert.Same(context, Assert.Single(deadLetters));
        Assert.Single(logger.Entries);
    }



    [Fact]
    public void SkipDeadLetterAndLog_channel_when_deadLetters_is_null_throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>
        (
            () => JsonErrorPolicy.SkipDeadLetterAndLog((ChannelWriter<ItemErrorContext>)null!, new CapturingLogger())
        );
    }



    [Fact]
    public void SkipDeadLetterAndLog_channel_when_logger_is_null_throws_ArgumentNullException()
    {
        var channel = Channel.CreateUnbounded<ItemErrorContext>();

        Assert.Throws<ArgumentNullException>
        (
            () => JsonErrorPolicy.SkipDeadLetterAndLog(channel.Writer, null!)
        );
    }



    [Fact]
    public void SkipDeadLetterAndLog_channel_writes_logs_and_returns_Skip()
    {
        var channel = Channel.CreateUnbounded<ItemErrorContext>();
        var logger = new CapturingLogger();
        var context = Context();

        var action = JsonErrorPolicy.SkipDeadLetterAndLog(channel.Writer, logger)(context);

        Assert.Equal(ItemErrorAction.Skip, action);
        Assert.True(channel.Reader.TryRead(out var written));
        Assert.Same(context, written);
        Assert.Single(logger.Entries);
    }



    private sealed class CapturingLogger : ILogger
    {
        public List<string> Entries { get; } = new();

        public IDisposable BeginScope<TState>(TState state)
            where TState : notnull => NullScope.Instance;

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
            Entries.Add(formatter(state, exception));
        }



        private sealed class NullScope : IDisposable
        {
            public static readonly NullScope Instance = new();

            public void Dispose()
            {
            }
        }
    }
}
