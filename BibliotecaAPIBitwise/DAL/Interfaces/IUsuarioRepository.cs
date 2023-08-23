using BibliotecaAPIBitwise.DTO;
using BibliotecaAPIBitwise.Models;

namespace BibliotecaAPIBitwise.DAL.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<bool> IsUniqueUser(string usuario);
        Task<Usuario> Registrar(UsuarioRegistroDTO usuarioRegistroDTO);
        Task<UsuarioRespuestaLoginDTO> Login(UsuarioLoginDTO usuarioLoginDTO);
    }
}
