namespace DemoAPI;

public static class API
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/Persons", GetPersons);
        app.MapGet("/Person/{id}", GetPerson);
        app.MapPost("/Person", InsertPerson);
        app.MapPut("/Person", UpdatePerson);
        app.MapDelete("/Person", DeletePerson);
    }

    private static async Task<IResult> GetPersons(IPersonData data)
    {
        try
        {
            return Results.Ok(await data.GetPersons());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetPerson(Guid id, IPersonData data)
    {
        try
        {
            var result = await data.GetPerson(id);
            if(result == null)
                return Results.NotFound();
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertPerson(PersonModel person, IPersonData data)
    {
        try
        {
            await data.InsertPerson(person);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdatePerson(PersonModel person, IPersonData data)
    {
        try
        {
            await data.UpdatePerson(person);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeletePerson(Guid id, IPersonData data)
    {
        try
        {
            await data.DeletePerson(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
