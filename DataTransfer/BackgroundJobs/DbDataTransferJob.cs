using DataTransfer.BackgroundJobs.Interfaces;
using DataTransfer.Contexts;
using Domain.Entities;
using Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace DataTransfer.BackgroundJobs;

public class DbDataTransferJob : IDbDataTransferJob
{
    private readonly OldAutoVerseContext _oldContext;
    private readonly AutoVerseContext _newContext;
    private readonly ILogger<DbDataTransferJob> _logger;
    
    public DbDataTransferJob(
        OldAutoVerseContext oldContext,
        AutoVerseContext newContext,
        ILogger<DbDataTransferJob> logger)
    {
        _oldContext = oldContext;
        _newContext = newContext;
        _logger = logger;
    }
    
    [Hangfire.AutomaticRetry(Attempts = 0)]
    public async Task Run(CancellationToken cancellationToken)
    {
        Console.WriteLine("job started");

        await UpdateMarks(cancellationToken);
        await UpdateModels(cancellationToken);
        await UpdateGenerations(cancellationToken);
        await UpdateConfigurations(cancellationToken);
        await UpdateModifications(cancellationToken);
        await UpdateNewModels(cancellationToken);
        
    }

    private async Task UpdateMarks(CancellationToken cancellationToken)
    {
        var savedMarkIds = _newContext.Marks.Select(mark => mark.Id.ToString()).ToList();
        var unsavedMarks = _oldContext.Marks.AsNoTracking().Where(mark => !savedMarkIds.Contains(mark.Id));
        var unsavedMarkEntities = unsavedMarks.Adapt<IEnumerable<Domain.Entities.Mark>>();
        await _newContext.Marks.AddRangeAsync(unsavedMarkEntities, cancellationToken);
        await _newContext.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateModels(CancellationToken cancellationToken)
    {
        var savedModelIds = _newContext.Models.Select(model => model.Id.ToString()).ToList();
        var unsavedModels = _oldContext.Models.AsNoTracking().Where(model => !string.IsNullOrEmpty(model.MarkId) && !savedModelIds.Contains(model.Id));
        var unsavedModelEntities = unsavedModels.Adapt<IEnumerable<Domain.Entities.Model>>();
        await _newContext.Models.AddRangeAsync(unsavedModelEntities, cancellationToken);
        await _newContext.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateGenerations(CancellationToken cancellationToken)
    {
        var savedGenerationIds = _newContext.Generations.Select(model => model.Id.ToString()).ToList();
        var unsavedGenerations = _oldContext.Generations.AsNoTracking().Where(model => !string.IsNullOrEmpty(model.ModelId) && !savedGenerationIds.Contains(model.Id));
        var unsavedGenerationEntities = unsavedGenerations.Adapt<IEnumerable<Domain.Entities.Generation>>();
        await _newContext.Generations.AddRangeAsync(unsavedGenerationEntities, cancellationToken);
        await _newContext.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateConfigurations(CancellationToken cancellationToken)
    {
        var savedConfigurationIds = _newContext.CarConfigurations.Select(model => model.Id.ToString()).ToList();
        var unsavedConfigurations = _oldContext.Configurations.AsNoTracking().Where(model => !string.IsNullOrEmpty(model.GenerationId) && !savedConfigurationIds.Contains(model.Id));
        var unsavedConfigurationEntities = unsavedConfigurations.Adapt<IEnumerable<Domain.Entities.CarConfiguration>>();
        await _newContext.CarConfigurations.AddRangeAsync(unsavedConfigurationEntities, cancellationToken);
        await _newContext.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateModifications(CancellationToken cancellationToken)
    {
        var savedModificationIds = _newContext.Modifications.Select(model => model.Id.ToString()).ToList();
        var unsavedModifications = _oldContext.Modifications.AsNoTracking().Where(model => !string.IsNullOrEmpty(model.ConfigurationId) && !savedModificationIds.Contains(model.ComplectationId));
        var unsavedModificationEntities = unsavedModifications.Adapt<IEnumerable<Domain.Entities.Modification>>();
        await _newContext.Modifications.AddRangeAsync(unsavedModificationEntities, cancellationToken);
        await _newContext.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateNewModels(CancellationToken cancellationToken)
    {
        var specifications = await _oldContext.Specifications.Where(model => !string.IsNullOrEmpty(model.ComplectationId)).AsNoTracking().ToListAsync(cancellationToken);
        var options = await _oldContext.Options.Where(model => !string.IsNullOrEmpty(model.ComplectationId)).AsNoTracking().ToListAsync(cancellationToken);
        var comforts = options.Adapt<IEnumerable<Comfort>>();
        var dimensions = specifications.Adapt<IEnumerable<Dimension>>();
        var emissions = specifications.Adapt<IEnumerable<Emissions>>();
        var engine = specifications.Adapt<IEnumerable<Engine>>();
        var exterior = options.Adapt<IEnumerable<Exterior>>();
        var interiorOptions = options.Adapt<List<Interior>>();
        var interiorSpecifications = specifications.Adapt<IEnumerable<Interior>>();
        Console.WriteLine("first loop started");
        for (int i = 0; i <= interiorOptions.Count/2; i++)
        {
            var interiorSpecification = interiorSpecifications.FirstOrDefault(spec => spec.ModificationId == interiorOptions[i].ModificationId);
            interiorOptions[i].Seats = interiorSpecification.Seats;
            interiorOptions[i].TrunksMinCapacity = interiorSpecification.TrunksMinCapacity;
            interiorOptions[i].TrunksMaxCapacity = interiorSpecification.TrunksMaxCapacity;
        }
        Console.WriteLine("first loop done");

        interiorSpecifications = interiorSpecifications.Where(interior =>
            interiorOptions.All(opt => opt.ModificationId != interior.ModificationId));
        var mobilityOptions = options.Adapt<IEnumerable<Mobility>>().ToArray();
        var mobilitySpecifications = specifications.Adapt<IEnumerable<Mobility>>();
        for (int i = 0; i <= mobilityOptions.Length/2; i++)
        {
            var mobilitySpecification = mobilitySpecifications.FirstOrDefault(spec => spec.ModificationId == mobilityOptions[i].ModificationId);
            mobilityOptions[i].FrontBrake = mobilitySpecification.FrontBrake;
            mobilityOptions[i].BackBrake = mobilitySpecification.BackBrake;
            mobilityOptions[i].FrontSuspension = mobilitySpecification.FrontSuspension;
            mobilityOptions[i].BackSuspension = mobilitySpecification.BackSuspension;
            mobilityOptions[i].Transmission = mobilitySpecification.Transmission;
            mobilityOptions[i].Drive = mobilitySpecification.Drive;
        }
        Console.WriteLine("second loop done");
        
        var performance = specifications.Adapt<IEnumerable<Performance>>();
        var safetyOptions = specifications.Adapt<IEnumerable<Safety>>().ToArray();
        var safetySpecifications = specifications.Adapt<IEnumerable<Safety>>();
        for (int i = 0; i <= safetyOptions.Length/2; i++)
        {
            var safetySpecification = safetySpecifications.FirstOrDefault(spec => spec.ModificationId == safetyOptions[i].ModificationId);
            safetyOptions[i].SafetyGrade = safetySpecification.SafetyGrade;
            safetyOptions[i].SafetyRating = safetySpecification.SafetyRating;
        }
        Console.WriteLine("third loop done");
        var weight = specifications.Adapt<IEnumerable<Weight>>();
        
        
        await _newContext.Comforts.AddRangeAsync(comforts, cancellationToken);
        await _newContext.Dimensions.AddRangeAsync(dimensions, cancellationToken);
        await _newContext.Emissions.AddRangeAsync(emissions, cancellationToken);
        await _newContext.Engines.AddRangeAsync(engine, cancellationToken);
        await _newContext.Exteriors.AddRangeAsync(exterior, cancellationToken);
        await _newContext.Interiors.AddRangeAsync(interiorOptions, cancellationToken);
        await _newContext.Mobilities.AddRangeAsync(mobilityOptions, cancellationToken);
        await _newContext.Performances.AddRangeAsync(performance, cancellationToken);
        await _newContext.Safeties.AddRangeAsync(safetyOptions, cancellationToken);
        await _newContext.Weights.AddRangeAsync(weight, cancellationToken);
        await _newContext.SaveChangesAsync(cancellationToken);
        Console.WriteLine("done");
    }
}