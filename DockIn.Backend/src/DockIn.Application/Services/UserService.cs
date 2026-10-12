using System.ComponentModel;
using DockIn.Application.Abstractions;
using DockIn.Application.Dtos;
using DockIn.Application.Mappers;
using DockIn.Domain.Entities;

namespace DockIn.Application.Services
{
    public class UserService
    {
        private IUserRepository _repository;
        private IPasswordHasher _passwordHasher;

        public UserService(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> ObterUserPorId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            User? user = await _repository.ObterUserId(id);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.ToDto();
        }

        public async Task<IEnumerable<User>> ObterUsers(int pagina = 1, int tamanho = 10)
        {
            if (pagina <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pagina));
            }

            if (tamanho > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(tamanho));
            }

            return await _repository.ObterUsersPaginados(pagina, tamanho);
        }

        public async Task<UserDto> CriarUserAsync(CriarUserDto dto)
        {
            if (dto == null)
            {
                throw new DomainException(nameof(dto));
            }

            var emailExists = await _repository.ObterUserEmail(dto.Email);
            if (emailExists)
            {
                throw new InvalidEnumArgumentException(nameof(emailExists));
            }

            if (dto.Password.Length <= 5)
            {
                throw new DomainException(nameof(dto.Password));
            }

            var hashPassword = _passwordHasher.HashPassword(dto.Password);
            var novoUser = dto.ToEntity(hashPassword);

            await _repository.CriarUser(novoUser);
            return novoUser.ToDto();
        }
    }
}