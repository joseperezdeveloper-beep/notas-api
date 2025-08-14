using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using InscripcionNotasApi.Models;

namespace InscripcionNotasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriasController : ControllerBase
    {
        private readonly IConfiguration _config;

        public MateriasController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("listar")]
        public IActionResult ListarMaterias()
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("conexionBase"));
                var materias = conn.Query<MateriasDTO>("SELECT idMateria, NombreMateria FROM Materias").ToList();
                return Ok(materias);
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
