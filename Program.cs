using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Microsoft.Extensions.DependencyInjection;
using TuristeiRV.API.Repositories;
using TuristeiRV.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string ?credentialsPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
if (FirebaseApp.DefaultInstance == null && !string.IsNullOrEmpty(credentialsPath))
{
    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile(credentialsPath)
    });
}

builder.Services.AddSingleton<FirestoreDb>(provider =>
{
    GoogleCredential credential = GoogleCredential.FromFile("D:\\Projetos\\Credencial\\turisteirv.json");

    string projectId = "turisteirv";  

    FirestoreDb firestoreDb = FirestoreDb.Create(projectId, new FirestoreClientBuilder
    {
        ChannelCredentials = credential.ToChannelCredentials()
    }.Build());

    return firestoreDb;
});

// Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", builder.Configuration["Firebase:D:\\Projetos\\Credencial\\turisteirv.json"]);

// builder.Services.AddSingleton<FirestoreDb>(provider =>
// {
//     string projectId = builder.Configuration["Firebase:turisteirv"];
//     FirestoreDb firestoreDb = FirestoreDb.Create(projectId);
//     return firestoreDb;
// });


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IPontoTuristicoRepository, PontoTuristicoRepository>();
builder.Services.AddScoped<IPontoTuristicoService, PontoTuristicoService>();

builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.MapControllers(); 

app.Run();
