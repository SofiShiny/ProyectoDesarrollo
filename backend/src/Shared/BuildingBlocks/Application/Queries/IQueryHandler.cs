using MediatR;

namespace Bloques.Aplicacion.Consultas;

public interface IConsultaHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IConsulta<TResult>
{
}
