using System;
using System.Collections.Generic;

namespace DataTransfer.Models;

public partial class Configuration
{
    public string Id { get; set; } = null!;

    public sbyte? DoorsCount { get; set; }

    public string? BodyType { get; set; }

    public string? ConfigurationName { get; set; }

    public string? GenerationId { get; set; }

    public virtual Generation? Generation { get; set; }

    public virtual ICollection<Modification> Modifications { get; set; } = new List<Modification>();
}
