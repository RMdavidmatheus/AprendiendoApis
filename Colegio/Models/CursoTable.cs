using System;
using System.Collections.Generic;

namespace Colegio.Models;

public partial class CursoTable
{
    public int IdCurso { get; set; }

    public string NombreCurso { get; set; } = null!;

    public DateTime CreatedRowAt { get; set; }

    public virtual ICollection<EstudianteTable> EstudianteTables { get; set; } = new List<EstudianteTable>();

    public virtual ICollection<ProfesorTable> ProfesorTables { get; set; } = new List<ProfesorTable>();
}
