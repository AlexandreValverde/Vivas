namespace Tareas.Clases
{
    public class Preferencias
    {
        // Variables
        public int Orden { set; get; } // 0 fecha ascendiente, 1 fecha descendiente
        public int Mostrar { set; get; } // 0 todas, 1 sin realizar, 2 realizadas
        public int Tipo { set; get; } // 0 todas, 1 urgentes, 2 normales
    }
}