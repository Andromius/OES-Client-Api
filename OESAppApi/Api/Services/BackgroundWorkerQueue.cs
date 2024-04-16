using System.Collections.Concurrent;

namespace OESAppApi.Api.Services;

public class BackgroundWorkerQueue
{
    private readonly ConcurrentQueue<Func<Task>> _workItems = new();
    private readonly SemaphoreSlim _signal = new(0, 1);

    public async Task<Func<Task>> DequeueAsync()
    {
        await _signal.WaitAsync();
        _workItems.TryDequeue(out var workItem);

        return workItem;
    }

    public void QueueBackgroundWorkItem(Func<Task> workItem)
    {
        ArgumentNullException.ThrowIfNull(workItem);

        _workItems.Enqueue(workItem);
        _signal.Release();
    }
}
