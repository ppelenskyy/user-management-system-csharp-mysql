using System.Text.RegularExpressions;

namespace GestionUsuarios
{
    class Utils
    {
        public static string? MostrarMenuPrincipal()
        {
            string? opcion;
            Console.WriteLine("""

                ┌────────────────────────────────────┐
                │    MENÚ DE GESTIÓN DE USUSARIOS    │
                │  ────────────────────────────────  │
                │ 1.- Agregar usuario.               │
                │ 2.- Modificar usuario.             │
                │ 3.- Eliminar usuario.              │
                │ 4.- Mostrar un usuario.            │
                │ 5.- Mostrar todos los usuarios.    │
                │ 0.- Salir.                         │
                └────────────────────────────────────┘
                """);
            Console.Write("\nOpción: ");
            opcion = Console.ReadLine();
            return opcion;
        }
        public static string? MostrarMenuModificaciones()
        {
            string? opcion;
            Console.WriteLine("""

                ┌────────────────────────────────────┐
                │       MENÚ DE MODIFICACIONES       │
                │  ────────────────────────────────  │
                │ 1.- Modificar nombre.              │
                │ 2.- Modificar apellido.            │
                │ 3.- Modificar dni.                 │
                │ 4.- Modificar edad.                │
                │ 5.- Guardar y Salir.               │
                │ 0.- Cancelar                       │
                └────────────────────────────────────┘
                """);
            Console.Write("\nOpción: ");
            opcion = Console.ReadLine();
            return opcion;
        }
        public static string? LeerCadena(string campo)
        {
            bool error;
            string? cadena;
            string? cadenaFormateada = "";
            do
            {
                error = false;
                Console.Write($"\nIntroducir {campo}: ");
                cadena = Console.ReadLine();
                if (cadena.IsWhiteSpace() || cadena == null)
                {
                    Console.WriteLine($"\nError: {campo} no puede estar vacio.");
                    error = true;
                    continue;
                }
                cadena = cadena.ToLower();
                char[] arrayCadena = cadena.ToArray();
                string primeraMayuscula = arrayCadena[0].ToString();
                primeraMayuscula = primeraMayuscula.ToUpper();
                arrayCadena[0] = Char.Parse(primeraMayuscula);
                for (int i = 0; i < arrayCadena.Length; i++)
                {
                    cadenaFormateada += arrayCadena[i];
                }
            } while (error);
            return cadenaFormateada;
        }

        public static int LeerEntero(string campo)
        {
            int entero;
            bool error;
            do
            {
                error = false;
                Console.Write($"\nIntroducir {campo}: ");
                if (!int.TryParse(Console.ReadLine(), out entero))
                {
                    Console.WriteLine("\nError: tiene que ser un número entero.");
                    error = true;
                }
            } while (error);
            return entero;
        }

        public static string LeerDni(string campo)
        {
            string dni;
            bool error;
            do
            {
                error = false;
                dni = Utils.LeerCadena(campo)!.ToUpper();
                string pattern = @"^\d{8}[A-Z]$";
                if (!Regex.IsMatch(dni, pattern))
                {
                    Console.WriteLine($"\nDNI: {dni} es incorrecto.");
                    error = true; 
                }
            } while (error);
            return dni;
        }

        public static int LeerEdad(string campo)
        {
            int edad;
            bool error;
            do
            {
                error = false;
                edad = Utils.LeerEntero(campo);
                if (edad < 0)
                {
                    Console.WriteLine("\nError: edad no puede ser negativa.");
                    error = true; 
                }
            } while (error);
            return edad;
        }
    }
}