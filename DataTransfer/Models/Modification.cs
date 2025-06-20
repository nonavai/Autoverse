using System;
using System.Collections.Generic;

namespace DataTransfer.Models;

public partial class Modification
{
    public string ComplectationId { get; set; } = null!;

    public int? OffersPriceFrom { get; set; }

    public int? OffersPriceTo { get; set; }

    public string? GroupName { get; set; }

    public string? ConfigurationId { get; set; }

    public virtual Configuration? Configuration { get; set; }
}
