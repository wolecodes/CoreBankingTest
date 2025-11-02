using CoreBanking.Test.APP.Common.Models;
using MediatR;


namespace CoreBanking.Test.APP.Common.Interfaces
{
  public interface ICommand : IRequest<Result> { }
  public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
  public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
}