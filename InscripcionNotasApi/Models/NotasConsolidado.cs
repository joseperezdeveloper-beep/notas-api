namespace InscripcionNotasApi.Models
{
    public class NotasConsolidado
    {
        public int IdNota { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public int Edad { get; set; }
        public string NombreMateria { get; set; }
        public string NombreCarrera { get; set; }
        public decimal Nota1 { get; set; }
        public decimal Nota2 { get; set; }
        public decimal Nota3 { get; set; }
        public decimal PromedioFinal { get; set; }
        public int IdMateria { get; set; }
        public int IdCarrera { get; set; }
        public int IdEstudiante { get; set; }

    }
}
