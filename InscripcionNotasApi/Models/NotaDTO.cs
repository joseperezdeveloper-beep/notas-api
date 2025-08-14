namespace InscripcionNotasApi.Models
{
    public class NotaDTO
    {
        public int IdEstudiante { get; set; }
        public int IdCarrera { get; set; }
        public int IdMateria { get; set; }
        public decimal Nota1 { get; set; }
        public decimal Nota2 { get; set; }
        public decimal Nota3 { get; set; }
    }
}
