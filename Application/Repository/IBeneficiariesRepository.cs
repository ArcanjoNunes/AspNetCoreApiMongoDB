using AspNetCoreApiMongoDB.Domain.Entities;
using AspNetCoreApiMongoDB.Domain.Models;

namespace AspNetCoreApiMongoDB.Application.Repository;

public interface IBeneficiariesRepository
{
    Task<Beneficiary> Add(Beneficiary beneficiary);
    Task<Beneficiary> Update(Beneficiary beneficiary);
    Task<DeleteResult> Delete(Beneficiary beneficiary);
    Task<Beneficiary> GetById(BeneficiaryRequest beneficiaryRequest);
    Task<List<Beneficiary>> GetAll(ParticipantRequest participantRequest);
}
