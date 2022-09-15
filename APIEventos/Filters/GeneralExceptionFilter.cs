using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;

namespace APIEventos.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Erro inesperado. Tente novamente.",
                Detail = "Erro inesperado. Tente novamente.",
                Type = context.Exception.GetType().Name
            };
            Console.WriteLine($"Tipo da exceção {context.Exception.GetType().Name}, mensagem {context.Exception.Message}, stack trace {context.Exception.StackTrace}.");
            switch (context.Exception)
            {
                case ArgumentNullException:
                    problem.Status = 417;
                    problem.Detail = "Erro inesperado no sistema";
                    problem.Title = "Erro inesperado no sistema";
                    context.Result = new ObjectResult(problem)
                    {
                        StatusCode = StatusCodes.Status417ExpectationFailed
                    };
                    break;
                case SqlException:
                    problem.Status = 503;
                    problem.Detail = "Erro inesperado ao se comunicar com o banco de dados";
                    problem.Title = "Erro inesperado ao se comunicar com o banco de dados";
                    context.Result = new ObjectResult(problem)
                    {
                        StatusCode = StatusCodes.Status503ServiceUnavailable
                    };
                    break;
                default:
                    context.Result = new ObjectResult(problem)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    break;
            }
        }
    }
}
