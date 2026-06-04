using GestionUsuarios;

class Program
{

    static void Main(string[] args)
    {
        var ur = new UsuarioRepository();
        string? opcion;
        do
        {
            opcion = MostrarMenu();
            switch (opcion)
            {
                case "1":
                {
                    ur.AgregarUsuario();
                    break;
                }
                case "2":
                {
                    ur.EliminarUsuario();
                    break;
                }
                case "3":
                {
                    ur.MostrarUsuario();
                    break;
                }
                case "4":
                {
                    ur.MostrarTodosLosUsuarios();
                    break;
                }

                case "0":
                {
                    Console.WriteLine("\n--- HASTA PRONTO ---");
                    break;
                }
                default:
                {
                    Console.WriteLine("\nOpción incorrecta elegir entre 0-4.");   
                    break;
                } 
            }
        }
        while (opcion != "0");
    }

    private static string? MostrarMenu()
    {
        string? opcion;
        Console.WriteLine("""

                 === MENÚ ===
            1.- Agregar usuario.
            2.- Eliminar usuario.
            3.- Mostrar un usuario.
            4.- Mostrar todos los usuarios.
            0.- Salir.
            """);
        Console.Write("\nOpción: ");
        opcion = Console.ReadLine();
        return opcion;
    }

}