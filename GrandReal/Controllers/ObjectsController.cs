﻿using GrandReal.DBconnection;
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
            ViewData["Title"] = "Объекты недвижимости";

            var objects = context.ObjectViews.Where(a=>a.IsActive == 1).ToList();
            var imagesObjects = context.ImagesObjects.ToList();
            var FavoriteClientObjects = context.FavoriteClientObjects.Where(a=>a.Client == idUser).Select(a=>a.Object).ToList();
            var AplicationsClient = context.Applications.Where(a=>a.IdClient == idUser).Select(a=>a.IdObject).ToList();
            

            var model = new ObjectsModel()
            {
                Objects = objects,
                ImagesObjects = imagesObjects,
                FavoriteClientObjects = FavoriteClientObjects,
                AplicationsClient = AplicationsClient
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

                var checkLike = context.FavoriteClientObjects.Any(a=>a.Client == idUser && a.Object == idObject);
                if (checkLike) return new { error = "Лайк уже стоит, обратитесь к раззарботчику" };

                #endregion

                int lastId = context.FavoriteClientObjects.Max(x => x.Id);
                var likeRecord = new FavoriteClientObject() { 
                    Id = lastId+1,
                    Client = idUser,
                    Object = idObject,
                };
                context.FavoriteClientObjects.Add(likeRecord);
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

                var likeRecord = context.FavoriteClientObjects.FirstOrDefault(a=>a.Client == idUser && a.Object == idObject);

                #region проверка, есть ли лайк в базе

                if (likeRecord == null) return new { error = "Лайк не найден, обратитесь к раззарботчику" };

                #endregion

                context.FavoriteClientObjects.Remove(likeRecord);
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
            //Не получится засунуть в конструктор, так как там контекст запроса не инициализирован

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
            ViewData["Title"] = "Избранные объекты";

            var FavoriteClientObjects = context.FavoriteClientObjects.Where(a => a.Client == idUser).Select(a => a.Object).ToList();
            var objects = context.ObjectViews.Where(a => FavoriteClientObjects.Contains(a.IdObject) && a.IsActive == 1).ToList();
            var imagesObjects = context.ImagesObjects.Where(a=> FavoriteClientObjects.Contains(a.Object)).ToList();
            var AplicationsClient = context.Applications.Where(a => a.IdClient == idUser).Select(a => a.IdObject).ToList();

            var model = new ObjectsModel()
            {
                Objects = objects,
                ImagesObjects = imagesObjects,
                FavoriteClientObjects = FavoriteClientObjects,
                AplicationsClient = AplicationsClient
            };
            return View("Index", model);
        }
        public object SubmitApplication(int idObject)
        {
            try
            {
                #region проверка на авторизацию
                if (checkAuth() == null)
                    return View("../Auth/Auth");
                #endregion

                var application = new Application()
                {
                    IdClient = idUser,
                    IdObject = idObject
                };
                context.Applications.Add(application);
                context.SaveChanges();

                return new { message = "Заявка успешно создана" };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }

            
        }
        public ActionResult ApplicationsObjects()
        {
            #region проверка на авторизацию
            if (checkAuth() == null)
                return View("../Auth/Auth");
            #endregion
            ViewData["Title"] = "Оставленные заявки";

            var FavoriteClientObjects = context.FavoriteClientObjects.Where(a => a.Client == idUser).Select(a => a.Object).ToList();               
            var AplicationsClient = context.Applications.Where(a => a.IdClient == idUser).Select(a => a.IdObject).ToList();
            var objects = context.ObjectViews.Where(a => AplicationsClient.Contains(a.IdObject) && a.IsActive == 1).ToList();
            var imagesObjects = context.ImagesObjects.Where(a => AplicationsClient.Contains(a.Object ?? 0)).ToList();

            var model = new ObjectsModel()
            {
                Objects = objects,
                ImagesObjects = imagesObjects,
                FavoriteClientObjects = FavoriteClientObjects,
                AplicationsClient = AplicationsClient
            };
            return View("Index", model);
        }
        public ActionResult SellObject()
        {
            #region проверка на авторизацию
            if (checkAuth() == null)
                return View("../Auth/Auth");
            #endregion
            ViewData["Title"] = "Продать объект";


            return View("SellObject");
        }
    }
}
