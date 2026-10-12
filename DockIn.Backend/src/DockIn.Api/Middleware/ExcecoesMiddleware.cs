using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
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
        catch(Exception ex)
        {
            await MapearExceptions(context, ex);
        }
    }

    public static Task MapearExceptions(HttpContext context, Exception ex)
    {
        HttpStatusCode status;
        string mensagem;

        switch(ex)
        {
            case NotFoundException:
                status = HttpStatusCode.NotFound;
                mensagem = ex.Message;
                break;

            case DomainException:
                status = HttpStatusCode.BadRequest;
                mensagem = HttpStatusCode.BadRequest + ex.Message;
                break;

            case ArgumentNullException:
                status = HttpStatusCode.BadRequest;
                mensagem = ex.Message;
                break;

            default:
                status = HttpStatusCode.InternalServerError;
                mensagem = HttpStatusCode.BadRequest + ex.Message;
                break;

        }

        return TratarErroInfraestruturaAsync(context, status, mensagem);
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
