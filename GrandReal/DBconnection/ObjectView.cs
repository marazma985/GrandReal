using System;
using System.Collections.Generic;

namespace GrandReal.DBconnection;

public partial class ObjectView
{
    public int IdObject { get; set; }

    public string? Type { get; set; }

    public string? Cost { get; set; }

    public int? Rooms { get; set; }

    public int? LivingArea { get; set; }

    public int? PlotArea { get; set; }

    public string? Floors { get; set; }

    public int? Flat { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? Address { get; set; }

    public string? FullAddress { get; set; }

    public int? IsActive { get; set; }
}
