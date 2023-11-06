using System;
using System.Collections.Generic;

namespace GrandReal.DBconnection;

public partial class Object
{
    public int IdObject { get; set; }

    public int? TypeObject { get; set; }

    public decimal? PriceObject { get; set; }

    public int? NumRoomsObject { get; set; }

    public string? LivingAreaObject { get; set; }

    public string? PlotAreaObject { get; set; }

    public int? FloorObject { get; set; }

    public int? IdSobObject { get; set; }

    public string? CityObject { get; set; }

    public string? DistrictObject { get; set; }

    public string? AddressObject { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<FavoriteClintObject> FavoriteClintObjects { get; set; } = new List<FavoriteClintObject>();

    public virtual Client? IdSobObjectNavigation { get; set; }

    public virtual ICollection<ImagesObject> ImagesObjects { get; set; } = new List<ImagesObject>();

    public virtual TypeObject? TypeObjectNavigation { get; set; }
}
