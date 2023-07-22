using Colegio.Models;
using Colegio.Models.bodys.StudentBody;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Colegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class studentController : ControllerBase
    {
        private IConfiguration _configuration;

        public studentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> StudentGet() 
        {
            using (var Context = new ColegioContext(_configuration))
            {
                if (await Context.EstudianteVista.ToListAsync() != null)
                {
                    if (Context.EstudianteVista.ToList().Count == 0)
                    {
                        return NotFound("No hay datos..");
                    }
                    return Ok(await Context.EstudianteVista.ToListAsync());
                }
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentByID(int id) 
        {
            using (var Context = new ColegioContext(_configuration))
            {
                if (id == null)
                {
                    return BadRequest("El parametro id no puede estar vacio...");
                }
                if (await Context.EstudianteTables.FindAsync(id) != null)
                {
                    var StudentFind = await Context.EstudianteVista.Where(s => s.IdEstudiante == id).FirstOrDefaultAsync();
                    return Ok(StudentFind);
                }
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> StudentAdd(body body) 
        {
            using (var Context = new ColegioContext(_configuration))
            {
                if (body == null)
                {
                    return BadRequest("El body esta vacio..");
                }
                EstudianteTable newEstudiante = new EstudianteTable();
                newEstudiante.NombreEstudiante = body.NombreEstudiante;
                newEstudiante.ApellidoEstudiante = body.ApellidoEstudiante;
                newEstudiante.IdCurso = body.IdCurso;
                newEstudiante.EdadEstudiante = body.EdadEstudiante;
                newEstudiante.IdProfesor = body.IdProfesor;
                if (await Context.EstudianteTables.AddAsync(newEstudiante) != null)
                {
                    await Context.SaveChangesAsync();
                    return Ok("Se ingreso el registro bajo el ID: "+newEstudiante.IdEstudiante);
                }
                return BadRequest("No se pudo ingresar el registro.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> StudentUpdate(int id, body body) 
        {
            using (var Context = new ColegioContext(_configuration))
            {
                if (id == null || body == null)
                {
                    return BadRequest("Los parametros no pueden estar vacios");
                }
                var StudentFind = await Context.EstudianteTables.FindAsync(id);
                if (StudentFind != null)
                {
                    StudentFind.NombreEstudiante = body.NombreEstudiante;
                    StudentFind.ApellidoEstudiante = body.ApellidoEstudiante;
                    StudentFind.IdCurso = body.IdCurso;
                    StudentFind.EdadEstudiante = body.EdadEstudiante;
                    StudentFind.IdProfesor = body.IdProfesor;
                    if (Context.EstudianteTables.Update(StudentFind) != null)
                    {
                        await Context.SaveChangesAsync();
                        return Ok("El registro bajo el Id: "+StudentFind.IdEstudiante+" fue actualizado");
                    }
                    return BadRequest();
                }
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> StudentDelete(int id) 
        {
            using (var Context = new ColegioContext(_configuration))
            {
                if (id == null)
                {
                    return BadRequest();
                }
                var StudentDelete = await Context.EstudianteTables.FindAsync(id);
                if (Context.EstudianteTables.Remove(StudentDelete) != null)
                {
                    await Context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
        }
    }
}
