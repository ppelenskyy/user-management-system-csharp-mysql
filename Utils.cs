using System.Text.RegularExpressions;

namespace GestionUsuarios
{
    class Utils
    {
        public static string? LeerCadena(string campo)
        {
            bool error;
            string? cadena;
            do
            {
                error = false;
                Console.Write($"\nIntroducir {campo}: ");
                cadena = Console.ReadLine();
                if (cadena.IsWhiteSpace() || cadena == null)
                {
                    Console.WriteLine($"\nError: {campo} no puede estar vacio.");
                    error = true;
                }
            } while (error);
            return cadena;
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

        public static string LeerDni()
        {
            string dni;
            bool error;
            do
            {
                error = false;
                dni = Utils.LeerCadena("dni")!.ToUpper();
                string pattern = @"^\d{8}[A-Z]$";
                if (!Regex.IsMatch(dni, pattern))
                {
                    Console.WriteLine($"\nDNI: {dni} es incorrecto.");
                    error = true; 
                }
            } while (error);
            return dni;
        }

        public static int LeerEdad()
        {
            int edad;
            bool error;
            do
            {
                error = false;
                edad = Utils.LeerEntero("edad");
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