using Application.Dto_s.Models;
using Domain.Contracts.CleanModels;
using Domain.Enums;

namespace Application.Dto_s.Responses;

public class GetMarksResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CyrillicName { get; set; }
    public int Popular { get; set; }
    public Country Country { get; set; }
}