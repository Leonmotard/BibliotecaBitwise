using BibliotecaAPIBitwise.Models;

namespace BibliotecaAPIBitwise.DAL.Interfaces
{
    public interface IGeneroRepository : IGenericRepository<Genero>
    {
        public Task<IEnumerable<Genero>> ObtenerConLibros();
    }
}
