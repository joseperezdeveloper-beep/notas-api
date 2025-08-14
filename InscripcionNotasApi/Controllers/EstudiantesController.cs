using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using InscripcionNotasApi.Models;

namespace InscripcionNotasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public EstudiantesController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("insertar")]
        public IActionResult InsertarEstudiante([FromBody] EstudianteDTO estudiante)
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("conexionBase"));
                conn.Execute("sp_InsertarEstudiante", estudiante, commandType: CommandType.StoredProcedure);
                return Ok(new { mensaje = "Estudiante ingresado correctamente" });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { error = "Error en base de datos: " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("listar")]
        public IActionResult ListarEstudiantes()
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("conexionBase"));
                var estudiantes = conn.Query<EstudianteDTO>("SELECT IdEstudiante, Nombre, Identificacion, Edad FROM Estudiantes").ToList();
                return Ok(estudiantes);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { error = "Error en base de datos: " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("actualizar")]
        public IActionResult ActualizarEstudiante([FromBody] EstudianteDTO estudiante)
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("conexionBase"));
                conn.Execute("sp_ActualizarEstudiante", estudiante, commandType: CommandType.StoredProcedure);
                return Ok(new { mensaje = "Estudiante actualizado correctamente" });
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { error = "Error en base de datos: " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

    }
}
