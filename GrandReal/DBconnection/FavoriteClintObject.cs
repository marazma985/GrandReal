using System;
using System.Collections.Generic;

namespace GrandReal.DBconnection;

public partial class FavoriteClintObject
{
    public int Id { get; set; }

    public int? Client { get; set; }

    public int? Object { get; set; }

    public virtual Client? ClientNavigation { get; set; }

    public virtual Object? ObjectNavigation { get; set; }
}
