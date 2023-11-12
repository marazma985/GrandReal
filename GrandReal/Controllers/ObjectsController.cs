using GrandReal.DBconnection;
using GrandReal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.Extensions.Hosting.Internal;
using System.Linq;

namespace GrandReal.Controllers
{
    public class ObjectsController : Controller
    {
        GrandrealContext context;
        int idUser;
        
        public ObjectsController()
        {

            context = new GrandrealContext();

        }
        public ActionResult Index() 
        {
            #region проверка на авторизацию
            if (checkAuth() == null)
                return View("../Auth/Auth");
            #endregion

            var objects = context.ObjectViews.Where(a=>a.IsActive == 1).ToList();
            var imagesObjects = context.ImagesObjects.ToList();
            var favoriteClintObjects = context.FavoriteClintObjects.Where(a=>a.Client == idUser).Select(a=>a.Object).ToList();
            

            var model = new ObjectsModel()
            {
                Objects = objects,
                ImagesObjects = imagesObjects,
                FavoriteClintObjects = favoriteClintObjects
            };
            return View(model);
        }
        public object LikeObject(int idObject) {
            try
            {
                #region проверка на авторизацию
                if (checkAuth() == null)
                    return View("../Auth/Auth");
                #endregion

                #region проверка, не поставлен ли лайк уже

                var checkLike = context.FavoriteClintObjects.Any(a=>a.Client == idUser && a.Object == idObject);
                if (checkLike) return new { error = "Лайк уже стоит, обратитесь к раззарботчику" };

                #endregion

                int lastId = context.FavoriteClintObjects.Max(x => x.Id);
                var likeRecord = new FavoriteClintObject() { 
                    Id = lastId+1,
                    Client = idUser,
                    Object = idObject,
                };
                context.FavoriteClintObjects.Add(likeRecord);
                context.SaveChanges();
                return new { message = "Успешно" };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }
        public object RemoveLikeObject(int idObject) {
            try
            {
                #region проверка на авторизацию
                if (checkAuth() == null)
                    return View("../Auth/Auth");
                #endregion

                var likeRecord = context.FavoriteClintObjects.FirstOrDefault(a=>a.Client == idUser && a.Object == idObject);

                #region проверка, есть ли лайк в базе

                if (likeRecord == null) return new { error = "Лайк не найден, обратитесь к раззарботчику" };

                #endregion

                context.FavoriteClintObjects.Remove(likeRecord);
                context.SaveChanges();
                return new { message = "Успешно" };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }
        /// <summary>
        /// Проверка на авторизацию, обязательна, если используется idUser переменная
        /// </summary>
        /// <returns>int id user если пришло в куки и null если не пришло</returns>
        public int? checkAuth() {
            if (HttpContext.Request.Cookies["idUser"] != null)
                if (Int32.TryParse(HttpContext.Request.Cookies["idUser"], out int id))
                {
                    idUser = id;
                    return id;
                }

            return null;
        }

        public ActionResult FavoriteObjects()
        {
            #region проверка на авторизацию
            if (checkAuth() == null)
                return View("../Auth/Auth");
            #endregion

            var favoriteClintObjects = context.FavoriteClintObjects.Where(a => a.Client == idUser).Select(a => a.Object).ToList();
            var objects = context.ObjectViews.Where(a => favoriteClintObjects.Contains(a.IdObject) && a.IsActive == 1).ToList();
            var imagesObjects = context.ImagesObjects.Where(a=> favoriteClintObjects.Contains(a.Object)).ToList();

            var model = new ObjectsModel()
            {
                Objects = objects,
                ImagesObjects = imagesObjects,
                FavoriteClintObjects = favoriteClintObjects
            };
            return View("Index", model);
        }
    }
}
