using InfluencerConnect.Domain.ApplicationUsers;
using System.Text.Json.Serialization;

namespace InfluencerConnect.Api.Controllers.Auth.Contracts;

public sealed record RegisterUserRequest
(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    [property: JsonConverter(typeof(UserTypeJsonConverter))]
    UserType UserType
);