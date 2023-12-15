using System;
using System.Collections.Generic;

namespace WebAPI_V1.Models;

public partial class AuditLog
{
    public int LogId { get; set; }

    public string? TableName { get; set; }

    public string? Operation { get; set; }

    public Guid? RecordId { get; set; }

    public DateTime? LogDateTime { get; set; }
}
