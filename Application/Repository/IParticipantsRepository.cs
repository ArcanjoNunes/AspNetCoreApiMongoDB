using AspNetCoreApiMongoDB.Domain.Entities;
using AspNetCoreApiMongoDB.Domain.Models;

namespace AspNetCoreApiMongoDB.Application.Repository;

public interface IParticipantsRepository
{
    Task<Participant> Add(Participant participant);
    Task<Participant> Update(Participant participant);
    Task<DeleteResult> Delete(Participant participant);
    Task<Participant> GetById(ParticipantRequest participantRequest);
    Task<List<Participant>> GetAll();
}
