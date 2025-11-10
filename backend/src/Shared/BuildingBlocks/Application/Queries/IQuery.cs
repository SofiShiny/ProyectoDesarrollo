using MediatR;

namespace Bloques.Aplicacion.Consultas;

public interface IConsulta<out TResult> : IRequest<TResult>
{
}
