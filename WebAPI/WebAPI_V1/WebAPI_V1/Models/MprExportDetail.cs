using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class MprExportDetail
{
    public Guid MprExportId { get; set; }

    public Guid? MprId { get; set; }

    public virtual Mpr? Mpr { get; set; }

    public virtual MprExport MprExport { get; set; } = null!;
}
