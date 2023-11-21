using System;
using System.Collections.Generic;

namespace GrandReal.DBconnection;

public partial class Application
{
    public int Id { get; set; }

    public int IdClient { get; set; }

    public int IdStaff { get; set; }

    public int IdObject { get; set; }

    public DateTime DateCreate { get; set; }
}
