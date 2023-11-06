using System;
using System.Collections.Generic;

namespace GrandReal.DBconnection;

public partial class Staff
{
    public int IdStaff { get; set; }

    public string? SurnameStaff { get; set; }

    public string? NameStaff { get; set; }

    public string? PatronymicStaff { get; set; }

    public decimal? PhoneStaff { get; set; }

    public string? PasswordStaff { get; set; }

    public int? ViewStaff { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
