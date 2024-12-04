using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.Brands;
public sealed record BrandInfo(string Name, string? Description, string? WebsiteUrl, string? LogoUrl);