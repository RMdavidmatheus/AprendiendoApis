using System;
using System.Collections.Generic;

namespace Colegio.Models;

public partial class ProfesorTable
{
    public int IdProfesor { get; set; }

    public string NombreProfesor { get; set; } = null!;

    public string ApellidoProfesor { get; set; } = null!;

    public string EdadProfesor { get; set; } = null!;

    public int IdCurso { get; set; }

    public DateTime RowCreatedAt { get; set; }

    public virtual ICollection<EstudianteTable> EstudianteTables { get; set; } = new List<EstudianteTable>();

    public virtual CursoTable IdCursoNavigation { get; set; } = null!;
}
