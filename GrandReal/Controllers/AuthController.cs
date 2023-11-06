using GrandReal.DBconnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;

namespace GrandReal.Controllers
{
    public class AuthController : Controller
    {
        GrandrealContext context;
        public AuthController() {
            context = new GrandrealContext();
        }
        // GET: AuthController
        public ActionResult Index()
        {
            return View("Auth");
        }

        // GET: AuthController/Details/5
        public object login(string PhoneClient, string PasswordClient)
        {
            var auth = context.Clients.FirstOrDefault(c => c.PhoneClient == PhoneClient && c.PasswordClient == PasswordClient);
            if (auth == null)
                return new { error = "Пользователь не найден"};

            return auth;
        }

        // GET: AuthController/Create
        public string CreateClient(Client client)
        {
            try
            {
                var id = context.Clients.Max(a => a.IdClient) + 1;
                client.IdClient = id;
                context.Clients.Add(client);
                context.SaveChanges();
                return "Регистрация прошла успешно";
            }
            catch (Exception ex)
            {
                return $"Возникла ошибка: {ex.Message}";
            }
            
        }

        // POST: AuthController/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Auth");
            }
        }

        // GET: AuthController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
