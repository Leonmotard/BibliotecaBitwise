using BibliotecaAPIBitwise.DAL.DataContext;
using BibliotecaAPIBitwise.DAL.Interfaces;
using BibliotecaAPIBitwise.DTO;
using BibliotecaAPIBitwise.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using XSystem.Security.Cryptography;

namespace BibliotecaAPIBitwise.DAL.Implementaciones
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context; 
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsUniqueUser(string usuario)
        {
            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == usuario);
            if (usuarioDb == null)
                return true;
            return false;
        }

        public Task<UsuarioRespuestaLoginDTO> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> Registrar(UsuarioRegistroDTO usuarioRegistroDTO)
        {
             
            var passEncriptada = ObtenerMD5(usuarioRegistroDTO.Password);

            var usuarioNuevo = new Usuario()
            {
                NombreUsuario = usuarioRegistroDTO.NombreUsuario,
                Password = usuarioRegistroDTO.Password,
                Nombre = usuarioRegistroDTO.Nombre,
                Role = usuarioRegistroDTO.Role

            };
            _context.Usuarios.Add(usuarioNuevo);
            await _context.SaveChangesAsync();
            usuarioNuevo.Password = passEncriptada;
            return usuarioNuevo;
        }

        public static string ObtenerMD5(string valor)
        {
            MD5CryptoServiceProvider valorEncriptado = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(valor);
            data = valorEncriptado.ComputeHash(data);
            string respuesta = "";

            for(int i = 0; i < data.Length; i++)
            {
                respuesta += data[i].ToString("x2").ToLower();  
            }
            return respuesta;
        }
    }
}
