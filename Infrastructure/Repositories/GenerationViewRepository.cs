using Application.Repositories;
using Application.Repositories.VIew;
using Domain.Entities;
using Domain.Entities.CustomEntities;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class GenerationViewRepository : BaseGuidRepository<GenerationView>, IGenerationViewRepository
{
    private AutoVerseContext _dataBase;
    public GenerationViewRepository(AutoVerseContext dataBase) : base(dataBase)
    {
        _dataBase = dataBase;
    }
}