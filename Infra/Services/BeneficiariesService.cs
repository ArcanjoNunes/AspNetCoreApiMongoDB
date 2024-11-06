namespace AspNetCoreApiMongoDB.Infra.Services;

public class BeneficiariesService : IBeneficiariesRepository
{
    private readonly IMongoCollection<Beneficiary> _beneficiaryCollection;
    private readonly IMongoCollection<Participant> _participantCollection;

    public BeneficiariesService(IOptions<MongoDBDatabaseSettings> mongoDBDatabaseSettings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(mongoDBDatabaseSettings.Value.DatabaseName);
        _beneficiaryCollection = database.GetCollection<Beneficiary>(mongoDBDatabaseSettings.Value.BeneficiariesCollectionName);
        _participantCollection = database.GetCollection<Participant>(mongoDBDatabaseSettings.Value.ParticipantsCollectionName);
    }

    public async Task<Beneficiary> Add(Beneficiary pBeneficiary)
    {
        pBeneficiary.Id = ObjectId.GenerateNewId().ToString();
        await _beneficiaryCollection.InsertOneAsync(pBeneficiary);
        return pBeneficiary;
    }

    public async Task<Beneficiary> Update(Beneficiary pBeneficiary)
    {
        await _beneficiaryCollection.ReplaceOneAsync(u => u.Id == pBeneficiary.Id, pBeneficiary);
        return pBeneficiary;
    }

    public async Task<DeleteResult> Delete(Beneficiary pBeneficiary)
    {
        var result = await _beneficiaryCollection.DeleteOneAsync(d => d.Id == pBeneficiary.Id);
        return result;
    }

    public async Task<Beneficiary> GetById(BeneficiaryRequest beneficiaryRequest)
    {
        var beneficiary = await _beneficiaryCollection.Find(s => s.Id == beneficiaryRequest.Id).FirstOrDefaultAsync();
        return beneficiary;
    }

    public async Task<List<Beneficiary>> GetAll(ParticipantRequest participantRequest)
    {
        var participant = await _participantCollection.Find(s => s.Id == participantRequest.Id).FirstOrDefaultAsync();
        if (participant is null || participant.BeneficiariesId.Count == 0) { return []; }

        var filter = Builders<Beneficiary>.Filter.In(s => s.Id, participant.BeneficiariesId);

        var beneficiaries = await _beneficiaryCollection.Find(filter).ToListAsync();
        return beneficiaries;
    }
}
