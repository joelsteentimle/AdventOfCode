namespace TestOf2023;

public abstract class DayTests
{
    private string TypeName => GetType().Name[..^"Tests".Length];
    protected List<string> TestLines => TypeName.ReadTestLines();
    protected List<string> RealLines => TypeName.ReadRealLines();
}