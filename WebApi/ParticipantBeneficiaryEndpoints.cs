namespace AspNetCoreApiMongoDB.WebApi;

public static class ParticipantBeneficiaryEndpoints
{
    public static void MapParticipantBeneciaryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/participantbeneficiary");

        group.MapPost("/add", AddParticipantBeneficiary);
        group.MapPost("/delete", DeleteParticipantBeneficiary);
    }

    public static async Task<IResult>
        AddParticipantBeneficiary(ParticipantBeneficiaryRequest pbRequest, IParticipantBeneficiariesRepository participantBeneficiariesService)
    {
        var added = await participantBeneficiariesService.Add(pbRequest);
        return added ? TypedResults.Ok() : TypedResults.NotFound();
    }

    public static async Task<IResult>
        DeleteParticipantBeneficiary(ParticipantBeneficiaryRequest pbRequest, IParticipantBeneficiariesRepository participantBeneficiaryService)
    {
        var deleted = await participantBeneficiaryService.Delete(pbRequest);
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }
}
