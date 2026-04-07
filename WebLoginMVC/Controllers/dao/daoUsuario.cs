using System.Security.Cryptography;
using System.Text;
using WebLoginMVC.Controllers.db;
using WebLoginMVC.Models;

namespace WebLoginMVC.Controllers.dao
{
    public class daoUsuario
    {
        clsDB clsDB = new clsDB();

        private string GenerarHash(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));

                return Convert.ToBase64String(bytes);
            }
        }
        public Usuario Login(string email, string contrasenia)
        {
            try
            {
                string hashContra = GenerarHash(contrasenia);

                clsDB.Sentencia($"sp_LoginUser '{email.Trim()}','{hashContra.Trim()}'");

                string[] registro = clsDB.getRegistro();

                if (registro == null || registro.Length == 0)
                {
                    return null;
                }

                return new Usuario
                {
                    id = int.Parse(registro[0]),
                    nombre = registro[1],
                    apellido = registro[2],
                    dni = registro[3],
                    email = registro[4],
                    contrasenia = hashContra
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error crítico en Login: {ex.Message}");
                return null;
            }
        }

        public string Insertar(string nombre, string apellido, string dni, string email, string contrasenia)
        {
            try
            {
                string hashContra = GenerarHash(contrasenia);
                clsDB.Sentencia($"EXEC sp_CreateUser '{nombre}','{apellido}','{dni}','{email}','{hashContra}'");
                return "Usuario creado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error al crear el usuario: {ex.Message}";
            }
        }
    }
}
