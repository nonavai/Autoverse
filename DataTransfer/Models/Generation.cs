using System;
using System.Collections.Generic;

namespace DataTransfer.Models;

public partial class Generation
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public short? YearStart { get; set; }

    public short? YearStop { get; set; }

    public sbyte? IsRestyle { get; set; }

    public string? ModelId { get; set; }

    public virtual ICollection<Configuration> Configurations { get; set; } = new List<Configuration>();

    public virtual Model? Model { get; set; }
}
