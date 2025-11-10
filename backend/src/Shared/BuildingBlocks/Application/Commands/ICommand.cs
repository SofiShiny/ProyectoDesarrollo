using MediatR;

namespace Bloques.Aplicacion.Comandos;

public interface IComando : IRequest
{
}

public interface IComando<out TResultado> : IRequest<TResultado>
{
}
