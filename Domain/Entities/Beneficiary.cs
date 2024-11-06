namespace AspNetCoreApiMongoDB.Domain.Entities;

public class Beneficiary
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = default!;
    public int Age { get; set; }
    public string DocumentId { get; set; } = default!;
}
