namespace SupportCode;

public abstract class DayTests
{
    private string TypeName => GetType().Name[..^"Tests".Length];
    protected List<string> GetTestLines(string suffix = "") => TypeName.ReadTestLines(suffix);
    protected List<string> GetRealLines() => TypeName.ReadRealLines();
}
