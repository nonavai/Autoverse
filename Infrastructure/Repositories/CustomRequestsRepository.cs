using Application.Repositories;
using Domain.Contracts.GetModels;
using Domain.Entities;
using Domain.Entities.CustomEntities;
using Infrastructure.Context;
using Infrastructure.Extensions;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Repositories;

public class CustomRequestsRepository : ICustomRequestsRepository
{
    private AutoVerseContext _dataBase;

    public CustomRequestsRepository(AutoVerseContext dataBase)
    {
        _dataBase = dataBase;
    }

    public async Task<IEnumerable<WeeklyCar>> GetWeeklyCarAsync(WeeklyCarsDto dto, CancellationToken token = default)
    {
        return await _dataBase.Generations
            .AsNoTracking()
            .Where(generation => dto.GenerationIds.Contains(generation.Id))
            .Include(generation => generation.Model)
            .ThenInclude(model => model.Mark).Select(result =>
                new WeeklyCar(result.Model.Mark.Id, result.Model.Id, result.Model.Name, result.Model.Mark.Name, result.Id))
            .ToListAsync(cancellationToken: token);
    }
    
    public async Task<WeeklyCar?> GetManyCarsAsync(int pageNumber, int pageSize, CancellationToken token = default)
    {
        return await _dataBase.Generations
            .AsNoTracking()
            .Paginate(pageSize, pageNumber)
            .Include(generation => generation.Model)
            .ThenInclude(model => model.Mark)
            .Select(result =>
                new WeeklyCar(result.Model.Mark.Id, result.Model.Id, result.Model.Name, result.Model.Mark.Name, result.Id))
            .FirstOrDefaultAsync(cancellationToken: token);
    }
    
    public async Task<Mark?> GetMarkConfigurationsAsync(GetMarkConfigurationsDto dto, CancellationToken token = default)
    {
        return await _dataBase.Marks
            .AsNoTracking()
            .Where(mark => mark.Id == dto.Id)
            .Include(mark => mark.Models)
                .ThenInclude(models => models.Generation)
                .ThenInclude(generation => generation.CarConfigurations)
            .FirstOrDefaultAsync(cancellationToken: token);
    }
    
    public async Task<CarConfiguration?> GetConfigurationFullInfoAsync(GetConfigurationFullInfoDto dto, CancellationToken token = default)
    {
        return await _dataBase.CarConfigurations
            .AsNoTracking()
            .Where(configuration => configuration.Id == dto.CarConfigurationId)
            .Include(configuration => configuration.Modifications)
            .Include(configuration => configuration.Generation)
                .ThenInclude(generation => generation.Model)
                .ThenInclude(model => model.Mark)
            .FirstOrDefaultAsync(cancellationToken: token);
        
        //if needed to gain all information about modification, use asQueryable to include of modification
    }
    
    public async Task<IEnumerable<Modification>> GetModificationsFullInfoAsync(GetModificationsInfoDto dto, CancellationToken token = default)
    {
        return await _dataBase.Modifications
            .AsNoTracking()
            .Where(modification => dto.ModificationIds.Contains(modification.Id))
            .Include(modification => modification.Comfort)
            .Include(modification => modification.Emissions)
            .Include(modification => modification.Engine)
            .Include(modification => modification.Dimension)
            .Include(modification => modification.Exterior)
            .Include(modification => modification.Interior)
            .Include(modification => modification.Mobility)
            .Include(modification => modification.Performance)
            .Include(modification => modification.Safety)
            .Include(modification => modification.Weight)
            .ToListAsync(cancellationToken: token);
    }
    
    public async Task<IEnumerable<Generation>> GetRandomCars(GetRandomCarsDto dto, CancellationToken token = default)
    {
        /*return await _dataBase.Generations
            .AsNoTracking()
            .OrderBy(mark => EF.Functions.Random())
            .Take(dto.Amount)
            .Include(generation => generation.Model)
                .ThenInclude(generation => generation.Mark)
            .Include(generation => generation.CarConfigurations
                .AsQueryable()
                .OrderBy(mark => EF.Functions.Random())
                .Take(1)
                .Include(configuration => configuration.Modifications
                    .Take(1).AsQueryable()
                    .Include(modification => modification.Comfort)
                    .Include(modification => modification.Emissions)
                    .Include(modification => modification.Engine)
                    .Include(modification => modification.Dimension)
                    .Include(modification => modification.Exterior)
                    .Include(modification => modification.Interior)
                    .Include(modification => modification.Mobility)
                    .Include(modification => modification.Performance)
                    .Include(modification => modification.Safety)
                    .Include(modification => modification.Weight)
                ))
            .ToListAsync(cancellationToken: token);*/
        
        return await _dataBase.Generations
        .AsNoTracking()
        .OrderBy(g => EF.Functions.Random())
        .Take(dto.Amount)
        .Select(generation => new Generation
        {
            // копируем основные поля поколения
            Id = generation.Id,
            // подгружаем связанные данные модели и марки
            Model = new Model
            {
                Id = generation.Model.Id,
                // …копируем другие свойства модели…
                Mark = new Mark
                {
                    Id = generation.Model.Mark.Id,
                    // …копируем свойства марки…
                }
            },
            // выбираем случайную конфигурацию
            CarConfigurations = generation.CarConfigurations
                .OrderBy(c => EF.Functions.Random())
                .Take(1)
                .Select(configuration => new CarConfiguration
                {
                    Id = configuration.Id,
                    // …копируем другие свойства конфигурации…
                    // выбираем одну модификацию с вложенными данными
                    Modifications = configuration.Modifications
                        .OrderBy(m => EF.Functions.Random())
                        .Take(1)
                        .Select(modification => new Modification
                        {
                            Id = modification.Id,
                            // Если есть навигационные свойства – можно их проецировать аналогично:
                            //Comfort = modification.Comfort,
                            //Emissions = modification.Emissions,
                            Engine = modification.Engine,
                            //Dimension = modification.Dimension,
                            //Exterior = modification.Exterior,
                            //Interior = modification.Interior,
                            Mobility = modification.Mobility,
                            //Performance = modification.Performance,
                            //Safety = modification.Safety,
                            //Weight = modification.Weight
                        }).ToList()
                }).ToList()
        }).ToListAsync(token);
    }
    
    public async Task<IEnumerable<Mark>> GetByFilter(GetByFilterDto dto, CancellationToken token = default)
    {
        var spec = new MarkSpecification(dto);
        var query = spec.Apply(_dataBase.Marks.AsNoTracking().AsQueryable());

        return await query.Paginate(dto.PageSize, dto.PageNumber).ToListAsync(cancellationToken: token);
    }

    public async Task SaveChangesAsync(CancellationToken token = default)
    {
        await _dataBase.SaveChangesAsync(token);
    }
}