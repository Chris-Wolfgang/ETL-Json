using Xunit;

namespace Wolfgang.Etl.Json.Tests.Unit;

public class JsonReportTests
{
    [Fact]
    public void Constructor_sets_CurrentItemCount()
    {
        var report = new JsonReport(42, 5);

        Assert.Equal(42, report.CurrentItemCount);
    }



    [Fact]
    public void Constructor_sets_CurrentSkippedItemCount()
    {
        var report = new JsonReport(42, 5);

        Assert.Equal(5, report.CurrentSkippedItemCount);
    }



    [Fact]
    public void Constructor_when_zero_values_sets_properties()
    {
        var report = new JsonReport(0, 0);

        Assert.Equal(0, report.CurrentItemCount);
        Assert.Equal(0, report.CurrentSkippedItemCount);
    }



    [Fact]
    public void Record_equality_when_same_values_returns_true()
    {
        var a = new JsonReport(10, 3);
        var b = new JsonReport(10, 3);

        Assert.Equal(a, b);
    }



    [Fact]
    public void Record_equality_when_different_values_returns_false()
    {
        var a = new JsonReport(10, 3);
        var b = new JsonReport(10, 4);

        Assert.NotEqual(a, b);
    }
}
