using GrandReal.DBconnection; 

namespace GrandReal.Models
{
    public class ObjectsModel
    {
        public List<ObjectView> Objects { get; set; }
        public List<int?> FavoriteClientObjects { get; set; }
        public List<int?> AplicationsClient { get; set; }
        public List<ImagesObject> ImagesObjects { get; set; }
    }
}