using BibliotecaAPIBitwise.Models;

namespace BibliotecaAPIBitwise.DAL.Interfaces
{
    public interface ILibroRepository : IGenericRepository<Libro>
    {
        public Task<Libro> ObtenerPorIdConRelacion(int id);
    }
}
