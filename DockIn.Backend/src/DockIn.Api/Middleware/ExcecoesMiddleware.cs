using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Npgsql;

public class ExcecoesMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex) when (ex is NpgsqlException || ex is InvalidOperationException)
        {
            await TratarErroInfraestruturaAsync(
                context,
                HttpStatusCode.ServiceUnavailable,
                "Serviço de base de dados temporariamente indisponível.");
        }
        catch (DbUpdateException)
        {
            await TratarErroInfraestruturaAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Ocorreu um erro ao guardar os dados na base de dados.");
        }
        catch (Exception)
        {
            await TratarErroInfraestruturaAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Ocorreu um erro inesperado no servidor.");
        }
    }

    private static Task TratarErroInfraestruturaAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string mensagem)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return context.Response.WriteAsJsonAsync(new { mensagem }, options);
    }
}
