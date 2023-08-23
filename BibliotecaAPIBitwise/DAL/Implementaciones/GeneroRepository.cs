using BibliotecaAPIBitwise.DAL.DataContext;
using BibliotecaAPIBitwise.DAL.Interfaces;
using BibliotecaAPIBitwise.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPIBitwise.DAL.Implementaciones
{
    public class GeneroRepository : GenericRepository<Genero>, IGeneroRepository
    {
        private readonly ApplicationDbContext _context;
        public GeneroRepository(ApplicationDbContext context) : base(context)
        { 
            _context = context;
        }

        public async Task<IEnumerable<Genero>> ObtenerConLibros()
        {
           return await _context.Generos.Include(l => l.Libros)
                .ToListAsync();
        }
    }
}
