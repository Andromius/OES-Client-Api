
namespace OESAppApi.Api.Services;

public class AnswerSimilarityCheckingService : BackgroundService
{
    private readonly BackgroundWorkerQueue _queue;

    public AnswerSimilarityCheckingService(BackgroundWorkerQueue queue)
    {
        _queue = queue;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await _queue.DequeueAsync();

            await workItem();
        }
    }
}
