namespace AspNetCoreApiMongoDB.Domain.Models;

public class MongoDBDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ParticipantsCollectionName { get; set; } = null!;
    public string BeneficiariesCollectionName { get; set; } = null!;
}
