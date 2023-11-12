using GrandReal.DBconnection; 

namespace GrandReal.Models
{
    public class ObjectsModel
    {
        public List<ObjectView> Objects { get; set; }
        public List<int?> FavoriteClintObjects { get; set; }
        public List<ImagesObject> ImagesObjects { get; set; }
    }
}