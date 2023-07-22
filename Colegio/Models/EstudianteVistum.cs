using System;
using System.Collections.Generic;

namespace Colegio.Models;

public partial class EstudianteVistum
{
    public int IdEstudiante { get; set; }

    public string EstudianteNombres { get; set; } = null!;

    public string EdadEstudiante { get; set; } = null!;

    public string NombreCurso { get; set; } = null!;

    public string NombresProfesor { get; set; } = null!;
}
