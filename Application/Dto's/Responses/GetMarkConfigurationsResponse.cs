using Application.Dto_s.Models;
using Domain.Contracts.CleanModels;

namespace Application.Dto_s.Responses;

public class GetMarkConfigurationsResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CyrillicName { get; set; }
    public IEnumerable<Model> Models { get; set; }
}