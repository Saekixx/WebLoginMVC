using Org.BouncyCastle.Crypto.Generators;
using WebLoginMVC.Controllers.db;
using WebLoginMVC.Models;

namespace WebLoginMVC.Controllers.dao
{
    public class daoUsuario
    {
        clsDB clsDB = new clsDB();

        public Usuario Login(string email, string contrasenia)
        {
            try
            {
                string hashContra = Convert.ToBase64String(Pkcs5S2ParametersGenerator.Pkcs5PasswordToBytes(contrasenia.ToCharArray()));
                clsDB.Sentencia($"sp_LoginUser({email},{hashContra})");
                string[] registro = clsDB.getRegistro();
                if (registro == null) return null;
                return new Usuario
                {
                    id = int.Parse(registro[0]),
                    nombre = registro[1],
                    apellido = registro[2],
                    dni = registro[3],
                    email = registro[4],
                    contrasenia = hashContra,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar sesión: {ex.Message}");
                return null;
            }
        }

        public string Insertar(string nombre, string apellido, string dni, string email, string contrasenia)
        {
            try
            {
                string hashContra = Convert.ToBase64String(Pkcs5S2ParametersGenerator.Pkcs5PasswordToBytes(contrasenia.ToCharArray()));
                clsDB.Sentencia($"sp_CreateUser({nombre},{apellido},{dni},{email},{hashContra})");
                return "Usuario creado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error al crear el usuario: {ex.Message}";
            }
        }
    }
}
