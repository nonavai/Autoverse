using Application.Dto_s.Responses;
using Mediator;

namespace Application.UseCases.Queries;

public record GetRandomCarsQuery(int Amount) : IQuery<IEnumerable<GetRandomCarsResponse>>;