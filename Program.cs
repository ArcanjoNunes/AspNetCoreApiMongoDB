var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.Configure<MongoDBDatabaseSettings>
    (
        builder.Configuration.GetSection("MongoDBDatabaseSettings")
    );

builder.Services.AddSingleton<IMongoClient>(_ => {
    var connectionString =
        builder
            .Configuration
            .GetSection("MongoDBDatabaseSettings:ConnectionString")?
            .Value;
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<IParticipantsRepository, ParticipantsService>();
builder.Services.AddScoped<IBeneficiariesRepository, BeneficiariesService>();
builder.Services.AddScoped<IParticipantBeneficiariesRepository, ParticipantBeneficiariesService>();


var app = builder.Build();

app.MapControllers();

app.MapParticipantEndpoints();
app.MapBeneficiaryEndpoints();
app.MapParticipantBeneciaryEndpoints();

app.UseHttpsRedirection();

app.MapGet("/", () => "MongoDB.Participants Server is running.");

app.Run();
