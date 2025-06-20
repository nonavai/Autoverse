using System;
using System.Collections.Generic;

namespace DataTransfer.Models;

public partial class Mark
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? CyrillicName { get; set; }

    public sbyte? Popular { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
