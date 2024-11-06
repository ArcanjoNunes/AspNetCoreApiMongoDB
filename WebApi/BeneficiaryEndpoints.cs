namespace AspNetCoreApiMongoDB.WebApi;

public static class BeneficiaryEndpoints
{
    public static void MapBeneficiaryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/beneficiary");

        group.MapPost("/add", AddBeneficiary);
        group.MapPost("/update", UpdateBeneficiary);
        group.MapPost("/delete", DeleteBeneficiary);
        group.MapPost("/select", SelectBeneficiary);
        group.MapPost("/list", GetAllBeneficiary);
    }

    public static async Task<IResult>
        AddBeneficiary([FromBody] Beneficiary beneficiary, IBeneficiariesRepository beneficiaryService)
    {
        var beneficiaryAdded = await beneficiaryService.Add(beneficiary);
        return TypedResults.Ok(beneficiaryAdded);
    }

    public static async Task<IResult>
        UpdateBeneficiary([FromBody] Beneficiary beneficiary, IBeneficiariesRepository beneficiaryService)
    {
        var beneficiaryUpdated = await beneficiaryService.Update(beneficiary);
        return TypedResults.Ok(beneficiaryUpdated);
    }

    public static async Task<IResult>
        DeleteBeneficiary([FromBody] Beneficiary beneficiary, IBeneficiariesRepository beneficiaryService)
    {
        var deleteResult = await beneficiaryService.Delete(beneficiary);
        return TypedResults.Ok(deleteResult);
    }

    public static async Task<IResult>
        SelectBeneficiary([FromBody] BeneficiaryRequest beneficiaryRequest, IBeneficiariesRepository beneficiaryService)
    {
        var beneficiarySelected = await beneficiaryService.GetById(beneficiaryRequest);
        return beneficiarySelected is null ? TypedResults.NotFound() : TypedResults.Ok(beneficiarySelected);
    }

    public static async Task<IResult>
        GetAllBeneficiary([FromBody] ParticipantRequest participantRequest, IBeneficiariesRepository beneficiaryService)
    {
        var beneficiaries = await beneficiaryService.GetAll(participantRequest);
        return TypedResults.Ok(beneficiaries);
    }
}
