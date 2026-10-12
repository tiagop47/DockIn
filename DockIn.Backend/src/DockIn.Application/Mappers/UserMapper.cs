using DockIn.Application.Dtos;
using DockIn.Domain.Entities;

namespace DockIn.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto(user.UserId,
                user.PerfilUser,
                user.Ativo,
                user.Email,
                user.PasswordHash,
                user.CreatedAt);
        }

        public static User ToEntity(this UserDto dto)
        {
            return new User(dto.Email,
                dto.Role,
                dto.PasswordHash);
        }

        public static User ToEntity(this CriarUserDto dto, string password)
        {
            return new User(dto.Email,
                dto.Role,
                password);
        }
    }
}