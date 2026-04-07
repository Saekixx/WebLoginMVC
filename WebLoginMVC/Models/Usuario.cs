namespace WebLoginMVC.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string dni { get; set; }
        public string email { get; set; }
        public string contrasenia { get; set; }

        public Usuario() { }
        public Usuario(int id, string nombre, string apellido, string dni, string email, string contrasenia)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni = dni;
            this.email = email;
            this.contrasenia = contrasenia;
        }


    }
}
