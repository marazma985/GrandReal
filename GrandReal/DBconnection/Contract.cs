using System;
using System.Collections.Generic;

namespace GrandReal.DBconnection;

public partial class Contract
{
    public int IdContract { get; set; }

    public int? IdByerContract { get; set; }

    public int? IdObjectContract { get; set; }

    public int? IdStaff { get; set; }

    public DateOnly? DateConclusionContract { get; set; }

    public DateOnly? ValidUntilContract { get; set; }

    public int? ViewContract { get; set; }

    public virtual Client? IdByerContractNavigation { get; set; }

    public virtual Object? IdObjectContractNavigation { get; set; }

    public virtual Staff? IdStaffNavigation { get; set; }
}
