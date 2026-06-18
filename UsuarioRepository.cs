using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
namespace GestionUsuarios
{
    class UsuarioRepository
    {
        private const string CONNECTION_STRING = "server=127.0.00.1;port=3308;uid=root;pwd=;database=tienda";

        public void AgregarUsuario()
        {
            string sql = "INSERT INTO usuarios (nombre, apellido, dni, edad) VALUES (@nombre, @apellido, @dni, @edad)";
            int numFilasAfectadas = 0;
            string dni = Utils.LeerDni("DNI");
            if (ExisteUsuario(dni))
            {
                Console.WriteLine("\nYa existe un usuario con el DNI introducido.");
                return;
            }
            using var conexion = ConexionBD();
            try
            {
                conexion.Open();
                using var cmd = new MySqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@nombre", Utils.LeerCadena("nombre"));
                cmd.Parameters.AddWithValue("@apellido", Utils.LeerCadena("apellido"));
                cmd.Parameters.AddWithValue("@dni", dni);
                cmd.Parameters.AddWithValue("@edad", Utils.LeerEdad("edad"));
                numFilasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("\nError de base de datos: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError inesperado: " + e.Message);
            }
            Console.WriteLine(numFilasAfectadas > 0 ? "\nComando ejecutado correctamente." : "\nError al ejecutar el comando");
        }

        public void ModificarUsuario()
        {
            string sql = "UPDATE usuarios SET nombre = @nombre, apellido = @apellido, dni = @dni, edad = @edad WHERE dni = @p_dni";
            int numFilasAfectadas = 0;
            string p_dni = Utils.LeerDni("DNI del usuario: ");
            if (!ExisteUsuario(p_dni))
                return;
            string[]? usuario = RecuperarUsuario(p_dni);
            string? nuevoNombre = usuario[1];
            string? nuevoApellido = usuario[2];
            string? nuevoDni = usuario[3];
            int nuevaEdad = Convert.ToInt32(usuario[4]);
            string? opcion = "";
            do
            {
                opcion = Utils.MostrarMenuModificaciones();
                switch (opcion)
                {
                    case "1":
                        {
                            Console.WriteLine($"Nombre actual: {usuario[1]}");
                            nuevoNombre = Utils.LeerCadena("nuevo nombre");
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine($"Apellido actual: {usuario[2]}");
                            nuevoApellido = Utils.LeerCadena("nuevo apellido");
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine($"Dni actual: {usuario[3]}");
                            nuevoDni = Utils.LeerDni("nuevo DNI");
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine($"Edad actual: {usuario[4]}");
                            nuevaEdad = Utils.LeerEdad("nueva edad");
                            break;
                        }
                    case "5":
                        {
                            break;
                        }
                    case "0":
                        {
                            return;
                        }
                    default:
                        {
                            break;
                        }
                }
            } while (opcion != "5");
            using var conexion = ConexionBD();
            try
            {
                conexion.Open();
                using var cmd = new MySqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@p_dni", p_dni);
                cmd.Parameters.AddWithValue("@nombre", nuevoNombre);
                cmd.Parameters.AddWithValue("@apellido", nuevoApellido);
                cmd.Parameters.AddWithValue("@dni", nuevoDni);
                cmd.Parameters.AddWithValue("@edad", nuevaEdad);
                numFilasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("\nError de base de datos: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError inesperado: " + e.Message);
            }
            Console.WriteLine(numFilasAfectadas > 0 ? "\nComando ejecutado correctamente." : "\nError al ejecutar el comando");
        }

        public void EliminarUsuario()
        {
            string sql = "DELETE FROM usuarios WHERE dni = @dni";
            int numFilasAfectadas = 0;
            string dni = Utils.LeerDni("DNI");
            if (!ExisteUsuario(dni))
                return;
            using var conexion = ConexionBD();
            try
            {
                conexion.Open();
                using var cmd = new MySqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@dni", dni);
                numFilasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("\nError de base de datos: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError inesperado: " + e.Message);
            }
            Console.WriteLine(numFilasAfectadas > 0 ? "\nComando ejecutado correctamente." : "\nError al ejecutar el comando");
        }

        public void MostrarTodosLosUsuarios()
        {
            string sql = "SELECT * FROM usuarios";
            int numFilas = 0;
            using var conexion = ConexionBD();
            try
            {
                conexion.Open();
                using var cmd = new MySqlCommand(sql, conexion);
                using var reader = cmd.ExecuteReader();
                Console.WriteLine();
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"ID: {reader[0]} | " +
                        $"Nombre: {reader["nombre"]} | " +
                        $"Apellido: {reader["apellido"]} | " +
                        $"DNI: {reader["dni"]} | " +
                        $"Edad: {reader["edad"]}"
                    );
                    numFilas++;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("\nError de base de datos: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError inesperado: " + e.Message);
            }
            Console.WriteLine(numFilas > 0 ? $"\nSe encontraron {numFilas} usuarios." : "\nNo hay usuarios registrados.");
        }

        public void MostrarUsuario()
        {
            string p_dni = Utils.LeerDni("DNI");
            if (!ExisteUsuario(p_dni))
                return;
            string[]? usuario = RecuperarUsuario(p_dni);
            if (usuario != null)
                Console.WriteLine(
                $"ID: {usuario![0]} | " +
                $"Nombre: {usuario[1]} | " +
                $"Apellido: {usuario[2]} | " +
                $"DNI: {usuario[3]} | " +
                $"Edad: {usuario[4]}"
                );
        }

        public string[]? RecuperarUsuario(string p_dni)
        {
            if (!ExisteUsuario(p_dni))
                return null;
            string sql = "SELECT * FROM usuarios WHERE dni = @p_dni";
            using var conexion = ConexionBD();
            try
            {
                conexion.Open();
                using var cmd = new MySqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@p_dni", p_dni);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string id = Convert.ToString(reader.GetInt32("id"));
                    string nombre = reader.GetString("nombre");
                    string apellido = reader.GetString("apellido");
                    string dni = reader.GetString("dni");
                    string edad = Convert.ToString(reader.GetInt32("edad"));
                    string[] ususario = {id, nombre, apellido, dni, edad};
                    return ususario;
                }
                else
                    Console.WriteLine("\nEl usuario no existe.");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("\nError de base de datos: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError inesperado: " + e.Message);
            }
            return null;
        }

        private bool ExisteUsuario(string p_dni)
        {
            string sql = "SELECT * FROM usuarios WHERE dni = @p_dni";
            using var conexion = ConexionBD();
            try
            {
                conexion.Open();
                using var cmd = new MySqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@p_dni", p_dni);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return true;
                else
                    Console.WriteLine("\nEl usuario no existe.");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("\nError de base de datos: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError inesperado: " + e.Message);
            }
            return false;
        }

        private MySqlConnection ConexionBD()
        {
            return new MySqlConnection(CONNECTION_STRING); ;
        }
    }
}