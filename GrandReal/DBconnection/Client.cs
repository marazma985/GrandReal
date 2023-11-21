using System;
using System.Collections.Generic;

namespace GrandReal.DBconnection;

public partial class Client
{
    public int IdClient { get; set; }

    public string? SurnameClient { get; set; }

    public string? NameClient { get; set; }

    public string? PatronymicClient { get; set; }

    public string? PhoneClient { get; set; }

    public string? PasswordClient { get; set; }

    public string? RequisitesClient { get; set; }

    public string? EmailClient { get; set; }

    public int? ViewClient { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<FavoriteClientObject> FavoriteClientObjects { get; set; } = new List<FavoriteClientObject>();

    public virtual ICollection<Object> Objects { get; set; } = new List<Object>();
}
