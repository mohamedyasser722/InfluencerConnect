namespace InfluencerConnect.Domain.Influencers;

public sealed record BirthDate
{
    private BirthDate() { }

    private static readonly DateOnly MinDate = new DateOnly(1900, 1, 1);

    public DateOnly Value { get; private set; }

    public BirthDate(DateOnly value)
    {
        if (value > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Birthdate cannot be in the future.");
        }
        if (value < MinDate)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"Birthdate cannot be earlier than {MinDate}.");
        }
        Value = value;
    }

    public int Age => CalculateAge(Value);

    private static int CalculateAge(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age))
        {
            age--;
        }
        return age;
    }

    public override string ToString() => Value.ToString("yyyy-MM-dd");
}
