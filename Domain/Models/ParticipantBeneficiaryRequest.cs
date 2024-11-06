namespace AspNetCoreApiMongoDB.Domain.Models;

public class ParticipantBeneficiaryRequest
{
    public string ParticipantId { get; set; } = default!;
    public List<string> BeneficiaryIds { get; set; } = default!;
}
