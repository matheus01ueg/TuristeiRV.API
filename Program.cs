using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Microsoft.Extensions.DependencyInjection;
using TuristeiRV.API.Configurations;
using TuristeiRV.API.Repositories;
using TuristeiRV.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FirebaseConfig>(builder.Configuration.GetSection("Firebase"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//******** Conexao para ambiente de desenvolvimento ****
// Carregar o conteúdo do JSON da variável de ambiente
string? credentialsJson = Environment.GetEnvironmentVariable("GOOGLE_CREDENTIALS_JSON");

if (!string.IsNullOrEmpty(credentialsJson))
{
    GoogleCredential credential;
    using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(credentialsJson)))
    {
        credential = GoogleCredential.FromStream(stream)
            .CreateScoped("https://www.googleapis.com/auth/cloud-platform");;
    }

    string projectId = "turisteirv";  

    var channelCredentials = credential.ToChannelCredentials();

    var firestoreClient = new FirestoreClientBuilder
    {
        ChannelCredentials = channelCredentials
    }.Build();

    builder.Services.AddSingleton(FirestoreDb.Create(projectId, firestoreClient));
}
else
{
    throw new Exception("A variável de ambiente GOOGLE_CREDENTIALS_JSON não está definida ou está vazia.");
}

builder.Services.AddSingleton(provider =>
{
    if (string.IsNullOrEmpty(credentialsJson))
    {
        throw new Exception("A variável de ambiente GOOGLE_CREDENTIALS_JSON não está definida ou está vazia.");
    }

    GoogleCredential credential;
    using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(credentialsJson)))
    {
        credential = GoogleCredential.FromStream(stream);
    }

    FirebaseApp app = FirebaseApp.Create(new AppOptions
    {
        Credential = credential
    });

    return FirebaseAuth.GetAuth(app);
});

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IPontoTuristicoRepository, PontoTuristicoRepository>();
builder.Services.AddScoped<IPontoTuristicoService, PontoTuristicoService>();

builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddHttpClient();

//****** Definindo conf para o Railway ******
var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

//****** Swagger fora do ambiente de desenvolvimento *****
// app.UseSwagger();
// app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers(); 

app.Run();
