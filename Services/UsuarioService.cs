using FirebaseAdmin.Auth;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Mappers;
using TuristeiRV.API.Repositories;

namespace TuristeiRV.API.Services;
public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<string> CadastrarUsuarioAsync(UsuarioDto usuarioDto, string senha)
    {
        var usuario = UsuarioMapper.ToModel(usuarioDto);

        var existingUser = await _usuarioRepository.GetUsuarioByEmailAsync(usuario.Email);
        if (existingUser != null)
        {
            throw new Exception("Usuário já cadastrado com este email.");
        }

        var userRecordArgs = new UserRecordArgs
        {
            Email = usuario.Email,
            Password = senha,  
        };

        UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);

        usuario.Id = userRecord.Uid;

        await _usuarioRepository.AddUsuarioAsync(usuario);

        return userRecord.Uid;  
    }

    public async Task EditarUsuarioAsync(string id, UsuarioDto usuarioDto)
    {
        var usuario = UsuarioMapper.ToModel(usuarioDto);

        await _usuarioRepository.UpdateUsuarioAsync(id, usuario);
    }

    public async Task<bool> AutenticarUsuarioAsync(string email, string senha)
    {
        var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(email);

        string senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(senhaHash, usuario.SenhaHash))
        {
            return false;  
        }

        return true;
    }
}