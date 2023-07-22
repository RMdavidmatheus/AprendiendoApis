using System;
using System.Collections.Generic;

namespace Colegio.Models;

public partial class EstudianteTable
{
    public int IdEstudiante { get; set; }

    public string NombreEstudiante { get; set; } = null!;

    public string ApellidoEstudiante { get; set; } = null!;

    public string EdadEstudiante { get; set; } = null!;

    public int IdCurso { get; set; }

    public int IdProfesor { get; set; }

    public DateTime RowCreatedAt { get; set; }

    public virtual CursoTable IdCursoNavigation { get; set; } = null!;

    public virtual ProfesorTable IdProfesorNavigation { get; set; } = null!;
}
