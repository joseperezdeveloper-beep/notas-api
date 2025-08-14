using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using InscripcionNotasApi.Models;

namespace InscripcionNotasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotasController : ControllerBase
    {
        private readonly IConfiguration _config;

        public NotasController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("insertar")]
        public IActionResult InsertarNotas([FromBody] NotaDTO nota)
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("conexionBase"));
                conn.Execute("sp_InsertarNotas", nota, commandType: CommandType.StoredProcedure);
                return Ok(new { mensaje = "Notas ingresadas correctamente" });
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
        public IActionResult ListarNotas()
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("conexionBase"));
                var notas = conn.Query<NotasConsolidado>(
                    "ObtenerNotasConDetalles",
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return Ok(notas);
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
        public IActionResult ActualizarNotas([FromBody] NotaUpdateDTO nota)
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("conexionBase"));
                conn.Execute("sp_ActualizarNotas", nota, commandType: CommandType.StoredProcedure);
                return Ok(new { mensaje = "Notas actualizadas correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id}")]
        public IActionResult EliminarNotas(int id)
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("conexionBase"));
                conn.Execute("sp_EliminarNotas", new { IdNota = id }, commandType: CommandType.StoredProcedure);
                return Ok(new { mensaje = "Notas eliminadas correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
