using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebLoginMVC.Controllers.dao;
using WebLoginMVC.Models;

namespace WebLoginMVC.Controllers
{
    public class UsuarioController : Controller
    {
        daoUsuario daoUsuario = new daoUsuario();

        public IActionResult Inicio()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult DashBoard()
        {
            string nombre = HttpContext.Session.GetString("UsuarioNombre");
            string email = HttpContext.Session.GetString("UsuarioEmail");

            if (string.IsNullOrEmpty(nombre))
            {
                return RedirectToAction("Inicio");
            }

            ViewBag.Nombre = nombre;
            ViewBag.Email = email;

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string contrasenia)
        {
            Usuario usuario = daoUsuario.Login(email, contrasenia);

            if (usuario == null)
            {
                ViewBag.Error = "Email o contraseña incorrectos.";
                return View("Inicio");
            }

            HttpContext.Session.SetString("UsuarioNombre", usuario.nombre);
            HttpContext.Session.SetString("UsuarioEmail", usuario.email);

            return RedirectToAction("DashBoard");
        }

        [HttpPost]
        public IActionResult Registro(Usuario model)
        {
            string resultado = daoUsuario.Insertar(
                model.nombre,
                model.apellido,
                model.dni,
                model.email,
                model.contrasenia
            );

            return RedirectToAction("Inicio");
        }
    }
}
