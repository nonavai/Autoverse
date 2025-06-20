using Application.Dto_s.Responses;
using Mediator;

namespace Application.UseCases.Commands;

public record UpdateWeeklyCarsCommand(IEnumerable<string> GenerationIds) : ICommand<IEnumerable<UpdateWeeklyCarsResponse>>;
