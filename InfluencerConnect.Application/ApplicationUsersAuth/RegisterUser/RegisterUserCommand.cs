using InfluencerConnect.Application.Abstractions.Messaging;
using InfluencerConnect.Domain.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InfluencerConnect.Application.ApplicationUsersAuth.RegisterUser;
public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    UserType UserType) : ICommand<string>;

