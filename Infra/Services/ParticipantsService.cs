namespace AspNetCoreApiMongoDB.Infra.Services;

public class ParticipantsService : IParticipantsRepository
{
    private readonly IMongoCollection<Participant> _participantColletion;

    public ParticipantsService(IOptions<MongoDBDatabaseSettings> mongoDBDatabaseSettings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(mongoDBDatabaseSettings.Value.DatabaseName);
        _participantColletion = database.GetCollection<Participant>(mongoDBDatabaseSettings.Value.ParticipantsCollectionName);
    }

    public async Task<Participant> Add(Participant participant)
    {
        participant.Id = ObjectId.GenerateNewId().ToString();
        await _participantColletion.InsertOneAsync(participant);
        return participant;
    }

    public async Task<Participant> Update(Participant participant)
    {
        await _participantColletion.ReplaceOneAsync(u => u.Id == participant.Id, participant);
        return participant;
    }

    public async Task<DeleteResult> Delete(Participant participant)
    {
        var result = await _participantColletion.DeleteOneAsync(d => d.Id == participant.Id);
        return result;
    }

    public async Task<Participant> GetById(ParticipantRequest participantRequest)
    {
        var participant = await _participantColletion.Find(s => s.Id == participantRequest.Id).FirstOrDefaultAsync();
        return participant;
    }

    public async Task<List<Participant>> GetAll()
    {
        var participants = await _participantColletion.Find(s => true).ToListAsync();
        return participants;
    }
}
