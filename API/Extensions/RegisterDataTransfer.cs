using DataTransfer.BackgroundJobs.Interfaces;
using Hangfire;

namespace API.Extensions;

public static class RegisterDataTransfer
{
    public static void InitializeDataTransferJob(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var dbTransferJob = scope.ServiceProvider.GetRequiredService<IDbDataTransferJob>();
        var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
            
        /*recurringJobManager.AddOrUpdate(
            "updateDbJob",
            () => dbTransferJob.Run(CancellationToken.None), 
            Cron.Minutely(),
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });*/

        BackgroundJob.Enqueue(() => dbTransferJob.Run(CancellationToken.None));

    }
}