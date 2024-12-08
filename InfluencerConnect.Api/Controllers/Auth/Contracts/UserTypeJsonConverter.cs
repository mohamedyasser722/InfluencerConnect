using InfluencerConnect.Domain.ApplicationUsers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfluencerConnect.Api.Controllers.Auth.Contracts;

public class UserTypeJsonConverter : JsonConverter<UserType>
{
    public override UserType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var enumString = reader.GetString();
            if (Enum.TryParse<UserType>(enumString, true, out var result))
            {
                return result;
            }
            throw new JsonException($"Invalid UserType value: {enumString}");
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            var enumValue = reader.GetInt32();
            if (Enum.IsDefined(typeof(UserType), enumValue))
            {
                return (UserType)enumValue;
            }
            throw new JsonException($"Invalid UserType value: {enumValue}");
        }

        throw new JsonException("Invalid JSON token for UserType");
    }

    public override void Write(Utf8JsonWriter writer, UserType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
