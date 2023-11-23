using GrandReal.DBconnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrandReal.Controllers
{
    public class DocsController : Controller
    {
        GrandrealContext context;
        int idUser;
        public DocsController()
        {
            context = new GrandrealContext();
        }
        // GET: DocsController
        public ActionResult Index()
        {
            //var docs = context.Contracts.Where(a=> a.id)
            return View();
        }

        // GET: DocsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public int? checkAuth()
        {
            //Не получится засунуть в конструктор, так как там контекст запроса не инициализирован

            if (HttpContext.Request.Cookies["idUser"] != null)
                if (Int32.TryParse(HttpContext.Request.Cookies["idUser"], out int id))
                {
                    idUser = id;
                    return id;
                }

            return null;
        }
    }
}
