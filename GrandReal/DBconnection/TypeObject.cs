using System;
using System.Collections.Generic;

namespace GrandReal.DBconnection;

public partial class TypeObject
{
    public int IdTypeObject { get; set; }

    public string? NameTypeObject { get; set; }

    public virtual ICollection<Object> Objects { get; set; } = new List<Object>();
}
