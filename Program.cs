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




// ******* Credenciais para o Railway *******
// string ?credentialsPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
// if (FirebaseApp.DefaultInstance == null && !string.IsNullOrEmpty(credentialsPath))
// {
//     FirebaseApp.Create(new AppOptions()
//     {
//         Credential = GoogleCredential.FromFile(credentialsPath)
//     });
// }


// builder.Services.AddSingleton<FirestoreDb>(provider =>
// {
//     GoogleCredential credential = GoogleCredential.FromFile("D:\\Projetos\\Credencial\\turisteirv.json");

//     string projectId = "turisteirv";  

//     FirestoreDb firestoreDb = FirestoreDb.Create(projectId, new FirestoreClientBuilder
//     {
//         ChannelCredentials = credential.ToChannelCredentials()
//     }.Build());

//     return firestoreDb;
// });


//******** Conexao para ambiente de desenvolvimento ****
string? credentialsPath = Environment.GetEnvironmentVariable("GOOGLE_CREDENTIALS_BASE64");
builder.Services.AddSingleton<FirestoreDb>(provider =>
{
    GoogleCredential credential = GoogleCredential.FromFile(credentialsPath)
        .CreateScoped(new[] { "https://www.googleapis.com/auth/datastore" }); 

    FirestoreDb firestoreDb = FirestoreDb.Create("turisteirv", new FirestoreClientBuilder
    {
        ChannelCredentials = credential.ToChannelCredentials()
    }.Build());

    return firestoreDb;
});


//****** Conexão para o servidor Railway e ambiente local *******
// string? credentialsPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");

// builder.Services.AddSingleton<FirestoreDb>(provider =>
// {
//     GoogleCredential credential;

//     if (string.IsNullOrEmpty(credentialsPath))
//     {
//         credential = GoogleCredential.FromFile("D:\\Projetos\\Credencial\\turisteirv.json")
//             .CreateScoped(new[] { "https://www.googleapis.com/auth/datastore" });
//     }
//     else
//     {
//         credential = GoogleCredential.FromFile(credentialsPath)
//             .CreateScoped(new[] { "https://www.googleapis.com/auth/datastore" });
//     }

//     FirestoreDb firestoreDb = FirestoreDb.Create("turisteirv", new FirestoreClientBuilder
//     {
//         ChannelCredentials = credential.ToChannelCredentials()
//     }.Build());

//     return firestoreDb;
// });

// if (FirebaseApp.DefaultInstance == null && !string.IsNullOrEmpty(credentialsPath))
// {
//     FirebaseApp.Create(new AppOptions()
//     {
//         Credential = GoogleCredential.FromFile(credentialsPath)
//     });
// }

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IPontoTuristicoRepository, PontoTuristicoRepository>();
builder.Services.AddScoped<IPontoTuristicoService, PontoTuristicoService>();

builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

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
