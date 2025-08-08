using Domain;
using Microsoft.EntityFrameworkCore;
using PruebaBlazor.Data;
using System.Security.Cryptography;
using System.Text;
//SE ACTIVAN PERMISOS DE CARPETA PARA USAR EL NAMESPACE Domain
namespace PruebaBlazor.Services
{
    // 1. Definimos los posibles resultados del login
    public enum LoginResult
    {
        Success,
        UserNotFound,
        WrongPassword
    }

    public interface IAuthService
    {
        Task<bool> RegisterAsync(string email, string password, string nombre);
        // 2. Actualizamos la firma para devolver el resultado y el usuario
        Task<(LoginResult result, Usuario? user)> LoginAsync(string email, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly LibreriaDbContext _context;

        public AuthService(LibreriaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterAsync(string email, string password, string nombre)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
                return false;

            var usuario = new Usuario
            {
                Email = email,
                PasswordHash = HashPassword(password),
                Nombre = nombre
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        // 3. Implementamos la nueva lógica de login
        public async Task<(LoginResult result, Usuario? user)> LoginAsync(string email, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
            {
                return (LoginResult.UserNotFound, null);
            }

            if (usuario.PasswordHash != HashPassword(password))
            {
                return (LoginResult.WrongPassword, null);
            }

            return (LoginResult.Success, usuario);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}

