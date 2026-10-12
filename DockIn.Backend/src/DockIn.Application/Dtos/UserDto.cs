using System;
using System.Collections.Generic;
using System.Text;

namespace DockIn.Application.Dtos
{
    public record UserDto(
        int UserId,
        Perfil Role,
        bool AtivoFlag,
        string Email,
        string PasswordHash,
        DateTime CreatedAt);

    public record CriarUserDto(
        string Email,
        Perfil Role,
        string Password);
}