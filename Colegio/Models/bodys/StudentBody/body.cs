namespace Colegio.Models.bodys.StudentBody
{
    public class body
    {
        public string NombreEstudiante { get; set; } = null!;

        public string ApellidoEstudiante { get; set; } = null!;

        public string EdadEstudiante { get; set; } = null!;

        public int IdCurso { get; set; }

        public int IdProfesor { get; set; }

    }
}
