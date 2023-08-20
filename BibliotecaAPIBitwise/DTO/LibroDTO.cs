using BibliotecaAPIBitwise.Models;

namespace BibliotecaAPIBitwise.DTO
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public bool ParaPrestar { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public string NombreAutor { get; set; }
        public string NombreGenero { get; set; }
        public HashSet<ComentarioListaDTO> Comentarios { get; set; } = new HashSet<ComentarioListaDTO>();
    }
}
