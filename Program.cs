using Microsoft.Azure.Cosmos;
using ProgramAplicationAPI.Repository.Interface;
using ProgramAplicationAPI.Repository.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton((provider) =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var endpointUri = configuration["CosmosDbSettings:EndpointUri"];
    var primaryKey = configuration["CosmosDbSettings:PrimaryKey"];
    var cosmosClient = new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions() { SerializerOptions = new CosmosSerializationOptions() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase } });
    return cosmosClient;
});


builder.Services.AddSingleton<IQuestionService, QuestionService>(provider =>
{
    var cosmosClient = provider.GetRequiredService<CosmosClient>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    var databaseName = configuration["CosmosDbSettings:DatabaseName"];
    var containerName = configuration["CosmosDbSettings:ContainerName"];
    var logger = provider.GetRequiredService<ILogger<QuestionService>>();
    return new QuestionService(cosmosClient, databaseName, containerName );
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
