namespace InfluencerConnect.Domain.ApplicationUsers;

public sealed record BirthDate
{
    public DateOnly Value { get; private set; }

    public BirthDate(DateOnly value)
    {
        if (value > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Birthdate cannot be in the future.");
        }
        Value = value;
    }

    public int Age
    {
        get
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - Value.Year;

            // Adjust if the birthday hasn't occurred yet this year
            if (Value > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
