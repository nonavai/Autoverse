namespace DataTransfer.BackgroundJobs.Interfaces;

public interface IDbDataTransferJob
{
    public Task Run(CancellationToken cancellationToken);

}