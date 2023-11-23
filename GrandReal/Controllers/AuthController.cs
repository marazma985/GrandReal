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

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="PhoneClient">Номер клиента</param>
        /// <param name="PasswordClient">Пароль клиента</param>
        /// <returns>Данные о войдённом юзере или объект с ошибкой</returns>
        public object login(string PhoneClient, string PasswordClient)
        {
            var auth = context.Clients.FirstOrDefault(c => c.PhoneClient == PhoneClient && c.PasswordClient == PasswordClient);
            if (auth == null)
                return new { error = "Пользователь не найден"};

            return auth;
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="client">объект с данными новго клиента</param>
        /// <returns> сообщение о результате</returns>
        ///Можно переделать под возвращаемый объект для точности в js
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
    }
}
