namespace AspNetCoreApiMongoDB.Infra.Services;

public class ParticipantBeneficiariesService : IParticipantBeneficiariesRepository
{
    private readonly IMongoCollection<Participant> _participantCollection;

    public ParticipantBeneficiariesService(IOptions<MongoDBDatabaseSettings> mongoDBDatabaseSettings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(mongoDBDatabaseSettings.Value.DatabaseName);
        _participantCollection = database.GetCollection<Participant>(mongoDBDatabaseSettings.Value.ParticipantsCollectionName);
    }

    public async Task<bool> Add(ParticipantBeneficiaryRequest pbRequest)
    {
        var participant = await _participantCollection.Find(s => s.Id == pbRequest.ParticipantId).FirstOrDefaultAsync();
        if (participant is null) { return false; }

        foreach (var beneficiaryId in pbRequest.BeneficiaryIds)
        {
            if (participant.BeneficiariesId.Contains(beneficiaryId)) { continue; }
            participant.BeneficiariesId.Add(beneficiaryId);
        }

        var replace = await _participantCollection.ReplaceOneAsync(u => u.Id == participant.Id, participant);

        return replace.IsAcknowledged && replace.ModifiedCount > 0;
    }

    public async Task<bool> Delete(ParticipantBeneficiaryRequest pbRequest)
    {
        var participant = await _participantCollection.Find(s => s.Id == pbRequest.ParticipantId).FirstOrDefaultAsync();
        if (participant is null) { return false; }

        foreach (var beneficiaryId in pbRequest.BeneficiaryIds)
        {
            if (participant.BeneficiariesId.Contains(beneficiaryId))
            {
                participant.BeneficiariesId.Remove(beneficiaryId);
            }
        }

        var replace = await _participantCollection.ReplaceOneAsync(u => u.Id == participant.Id, participant);

        return replace.IsAcknowledged && replace.ModifiedCount > 0;
    }
}
