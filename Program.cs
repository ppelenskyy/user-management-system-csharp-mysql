using GestionUsuarios;

class Program
{
    static void Main(string[] args)
    {
        var ur = new UsuarioRepository();
        string? opcion;
        do
        {
            opcion = Utils.MostrarMenuPrincipal();
            switch (opcion)
            {
                case "1":
                    {
                        Console.WriteLine("""

                        ┌────────────────────────────────────┐
                        │         AGREGAR UN USUARIO         │
                        └────────────────────────────────────┘
                        """);
                        ur.AgregarUsuario();
                        break;
                    }
                case "2":
                    {
                        Console.WriteLine("""

                        ┌────────────────────────────────────┐
                        │        MODIFICAR UN USUARIO        │
                        └────────────────────────────────────┘
                        """);
                        ur.ModificarUsuario();
                        break;
                    }
                case "3":
                    {
                        Console.WriteLine("""

                        ┌───────────────────────────────────┐
                        │        ELIMINAR UN USUARIO        │
                        └───────────────────────────────────┘
                        """);
                        ur.EliminarUsuario();
                        break;
                    }
                case "4":
                    {
                        Console.WriteLine("""

                        ┌────────────────────────────────────┐
                        │         MOSTRAR UN USUARIO         │
                        └────────────────────────────────────┘
                        """);
                        ur.MostrarUsuario();
                        break;
                    }
                case "5":
                    {
                        Console.WriteLine("""

                        ┌────────────────────────────────────┐
                        │     MOSTRAR TODOS LOS USUARIOS     │
                        └────────────────────────────────────┘
                        """);
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
                        Console.WriteLine("\nOpción incorrecta elegir entre 0-5.");
                        break;
                    }
            }
        }
        while (opcion != "0");
    }
}