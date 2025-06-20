using System;
using System.Collections.Generic;

namespace DataTransfer.Models;

public partial class Model
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? CyrillicName { get; set; }

    public string? Class { get; set; }

    public short? YearFrom { get; set; }

    public short? YearTo { get; set; }

    public string? MarkId { get; set; }

    public virtual ICollection<Generation> Generations { get; set; } = new List<Generation>();

    public virtual Mark? Mark { get; set; }
}
