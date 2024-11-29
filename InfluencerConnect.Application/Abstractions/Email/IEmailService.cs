using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.Abstractions.Email;
public interface IEmailService
{
    Task SendAsync(string recepient, string subject, string body);
}
