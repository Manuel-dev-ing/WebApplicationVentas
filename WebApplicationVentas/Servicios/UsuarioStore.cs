using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebApplicationVentas.Entidades;

namespace WebApplicationVentas.Servicios
{
    public class UsuarioStore : IUserStore<Usuario>, IUserEmailStore<Usuario>, IUserPasswordStore<Usuario>, IUserRoleStore<Usuario>, IUserClaimStore<Usuario>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly List<IdentityUserClaim<string>> userClaims = new List<IdentityUserClaim<string>>();
        private readonly List<Usuario> users = new List<Usuario>();

        public UsuarioStore(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task AddClaimsAsync(Usuario user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
            {
                userClaims.Add(new IdentityUserClaim<string>
                {
                    UserId = Convert.ToString(user.Id),
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value,
                });

            }
            return Task.CompletedTask;
        }

        public Task AddToRoleAsync(Usuario user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(Usuario user, CancellationToken cancellationToken)
        {
            unitOfWork.repositorioUsuarios.crearUsuario(user);
            await unitOfWork.Complete();

            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
           
        }

        public Task<Usuario> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var usuario = await unitOfWork.repositorioUsuarios.obtenerPorId(Convert.ToInt32(userId));
            return usuario;
        }

        public async Task<Usuario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {

            var usuario = await unitOfWork.repositorioUsuarios.buscarUsuarioPorCorreo(normalizedUserName);
            return usuario;
        }

        public async Task<IList<Claim>> GetClaimsAsync(Usuario user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claims = new List<Claim>
            {
                
                new Claim("NombreCompleto", $"{user.Nombre} {user.Apellidos}") // Agregamos el nuevo claim
            };

            return await Task.FromResult(claims);
        }

        public Task<string> GetEmailAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Correo);
        }

        public Task<bool> GetEmailConfirmedAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {

            return Task.FromResult(user.Nombre + ' ' + user.Apellidos);
        }

        public Task<string> GetPasswordHashAsync(Usuario user, CancellationToken cancellationToken)
        {

            return Task.FromResult(user.Password);

        }

        public Task<IList<string>> GetRolesAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Correo);
        }

        public Task<IList<Usuario>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            var user = userClaims.Where(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value)
                .Select(c => users.FirstOrDefault(u => Convert.ToString(u.Id) == c.UserId))
                .Where(u => u != null).ToList();

            return Task.FromResult<IList<Usuario>>(user);
        }

        public Task<IList<Usuario>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsInRoleAsync(Usuario user, string roleName, CancellationToken cancellationToken)
        {
            var rol = await unitOfWork.repositorioRol.obtenerRolPorNombre(roleName);

            if (rol == null)
            {
                return false;
            }

            var usuario = await unitOfWork.repositorioUsuarios.buscarPorId(user);

            return rol.Id == usuario.IdRol;

        }

        public Task RemoveClaimsAsync(Usuario user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task RemoveFromRoleAsync(Usuario user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(Usuario user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetEmailAsync(Usuario user, string email, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(Usuario user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(Usuario user, string normalizedEmail, CancellationToken cancellationToken)
        {

            user.Correo = normalizedEmail;
            return Task.CompletedTask;

        }

        public Task SetNormalizedUserNameAsync(Usuario user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Nombre = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(Usuario user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Usuario user, string userName, CancellationToken cancellationToken)
        {
            user.Nombre = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
