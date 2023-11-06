using System;
using System.Collections.Generic;

namespace GrandReal.DBconnection;

public partial class ImagesObject
{
    public int IdImage { get; set; }

    public string? NameImage { get; set; }

    public int? Object { get; set; }

    public virtual Object? ObjectNavigation { get; set; }
}
