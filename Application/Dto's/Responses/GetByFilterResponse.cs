using Application.Dto_s.Models;
using Domain.Contracts.CleanModels;

namespace Application.Dto_s.Responses;

public class GetByFilterResponse
{
    public Mark Marks { get; set; }
}