using AspNetCoreApiMongoDB.Domain.Models;

namespace AspNetCoreApiMongoDB.Application.Repository;

public interface IParticipantBeneficiariesRepository
{
    Task<bool> Add(ParticipantBeneficiaryRequest pbResquest);
    Task<bool> Delete(ParticipantBeneficiaryRequest pbResquest);
}
