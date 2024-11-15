using TuristeiRV.API.DTOs;
using TuristeiRV.API.Mappers;
using TuristeiRV.API.Repositories;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TuristeiRV.API.Configurations;
using TuristeiRV.API.Models.Entidade;
using FirebaseAdmin.Auth;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace TuristeiRV.API.Services;
public class UsuarioService : IUsuarioService
{
    private readonly HttpClient httpClient;
    private readonly IUsuarioRepository usuarioRepository;
    private readonly FirebaseAuth auth;
    private readonly string apiKey;

    public UsuarioService(HttpClient httpClient, FirebaseAuth auth, IUsuarioRepository usuarioRepository, IOptions<FirebaseConfig> firebaseConfig)
    {
        this.httpClient = httpClient;
        this.auth = auth;
        this.usuarioRepository = usuarioRepository;
        this.apiKey = firebaseConfig.Value.Client.ApiKey;
    }

    public async Task<UsuarioDto> RegistrarUsuarioAsync(Usuario usuario, string senha)
    {
        // Cria o usuário no Firebase Authentication
        var userRecordArgs = new UserRecordArgs
        {
            Email = usuario.Email,
            Password = senha
        };

        UserRecord userRecord = await auth.CreateUserAsync(userRecordArgs);
        string uid = userRecord.Uid;

        // Hash da senha para armazenar na coleção
        usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

        // Salva o usuário na coleção 'users'
        await usuarioRepository.SalvarUsuarioAsync(uid, usuario);

        // Mapeia para o DTO
        return new UsuarioDto
        {
            Email = usuario.Email,
            Nome = usuario.Nome,
            Sobrenome = usuario.Sobrenome,
            Endereco = usuario.Endereco,
            Numero = usuario.Numero,
            Bairro = usuario.Bairro,
            Complemento = usuario.Complemento
        };
    }

    public async Task<UsuarioDto> AutenticarUsuarioAsync(string email, string senha)
    {
        string requestUri = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}";

        var payload = new
        {
            email = email,
            password = senha,
            returnSecureToken = true
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new UnauthorizedAccessException("Credenciais inválidas.");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine(jsonResponse);
        var authResponse = JsonSerializer.Deserialize<FirebaseAuthResponse>(jsonResponse);

        var usuario = await usuarioRepository.ObterUsuarioAsync(authResponse.LocalId);
        if (usuario == null)
        {
            throw new UnauthorizedAccessException("Usuário não encontrado.");
        }

        return new UsuarioDto
        {
            Email = usuario.Email,
            Nome = usuario.Nome,
            Sobrenome = usuario.Sobrenome,
            Endereco = usuario.Endereco,
            Numero = usuario.Numero,
            Bairro = usuario.Bairro,
            Complemento = usuario.Complemento
        };
    }
}
public class FirebaseAuthResponse
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }

    [JsonPropertyName("localId")]
    public string LocalId { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("idToken")]
    public string IdToken { get; set; }

    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; }

    [JsonPropertyName("expiresIn")]
    public string ExpiresIn { get; set; }
}