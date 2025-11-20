public class Tag : ValueObject
{

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Tag()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting

    private Tag(string value)
    {
        Value = value;
    }

    public static Tag Create(string value)
        => new (value);

    public string Value { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}