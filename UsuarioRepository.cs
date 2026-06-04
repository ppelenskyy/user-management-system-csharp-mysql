using MySql.Data.MySqlClient;
namespace GestionUsuarios
{
    class UsuarioRepository
    {
        private const string CONNECTION_STRING = "server=127.0.00.1;port=3306;uid=root;pwd=;database=tienda";
        public void AgregarUsuario()
        {
            string sql = "INSERT INTO usuarios (nombre, apellido, dni, edad) VALUES (@nombre, @apellido, @dni, @edad)";
            int numFilasAfectadas = 0;
            using var conexion = ConexionBD();
            try
            {
                conexion.Open();
                using var cmd = new MySqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@nombre", Utils.LeerCadena("nombre"));
                cmd.Parameters.AddWithValue("@apellido", Utils.LeerCadena("apellido"));
                cmd.Parameters.AddWithValue("@dni", Utils.LeerDni());
                cmd.Parameters.AddWithValue("@edad", Utils.LeerEdad());
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
            using var conexion = ConexionBD();
            try
            {
                conexion.Open();
                using var cmd = new MySqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@dni", Utils.LeerDni());
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
            string sql = "SELECT * FROM usuarios WHERE dni = @dni";
            using var conexion = ConexionBD();
            try
            {
                conexion.Open();
                using var cmd = new MySqlCommand(sql, conexion);
                string dni = Utils.LeerDni();
                cmd.Parameters.AddWithValue("@dni", dni);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine(
                        $"ID: {reader[0]} | " +
                        $"Nombre: {reader["nombre"]} | " +
                        $"Apellido: {reader["apellido"]} | " +
                        $"DNI: {reader["dni"]} | " +
                        $"Edad: {reader["edad"]}"
                    );
                }
                else
                {
                    Console.WriteLine("\nEl usuario no existe.");
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
        }

        private MySqlConnection ConexionBD()
        {
            return new MySqlConnection(CONNECTION_STRING); ;
        }
    }
}