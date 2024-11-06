namespace AspNetCoreApiMongoDB.WebApi;

public static class ParticipantEndpoints
{
    public static void MapParticipantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/participant");

        group.MapPost("/add", AddParticipant);
        group.MapPost("/update", UpdateParticipant);
        group.MapPost("/delete", DeleteParticipant);
        group.MapPost("/select", SelectParticipant);
        group.MapPost("/list", GetAllParticipant);

    }

    public static async Task<IResult>
        AddParticipant(Participant participant, IParticipantsRepository participantService)
    {
        var participantAdded = await participantService.Add(participant);
        return TypedResults.Ok(participantAdded);
    }

    public static async Task<IResult>
        UpdateParticipant(Participant participant, IParticipantsRepository participantService)
    {
        var participantUpdated = await participantService.Update(participant);
        return TypedResults.Ok(participantUpdated);
    }

    public static async Task<IResult>
        DeleteParticipant(Participant participant, IParticipantsRepository participantService)
    {
        var deleteResult = await participantService.Delete(participant);
        return TypedResults.Ok(deleteResult);
    }

    public static async Task<IResult>
        SelectParticipant(ParticipantRequest participantRequest, IParticipantsRepository participantService)
    {
        var participantSelected = await participantService.GetById(participantRequest);
        return participantSelected is null ? TypedResults.NotFound() : TypedResults.Ok(participantSelected);
    }

    public static async Task<IResult>
        GetAllParticipant(IParticipantsRepository participantService)
    {
        var participants = await participantService.GetAll();
        return TypedResults.Ok(participants);
    }
}
