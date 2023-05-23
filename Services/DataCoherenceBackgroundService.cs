using KanS.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanS.Services;

public class DataCoherenceBackgroundService : BackgroundService {
    private readonly IServiceProvider _serviceProvider;

    public DataCoherenceBackgroundService(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        while(!stoppingToken.IsCancellationRequested) {
            using(var scope = _serviceProvider.CreateScope()) {
                var dbContext = scope.ServiceProvider.GetRequiredService<KansDbContext>();

                await UpdateSectionsAndTasksDeletion(dbContext);
            }

            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
        }
    }

    private async Task UpdateSectionsAndTasksDeletion(KansDbContext dbContext) {
        var deletedBoards = await dbContext.UserBoards
            .Where(ub => ub.Deleted && ub.UserId == ub.Board.OwnerId)
            .Select(ub => ub.Board.Id)
            .ToListAsync();

        var sectionsToDelete = await dbContext.Sections
            .Where(s => deletedBoards.Contains(s.BoardId) && !s.Deleted)
            .ToListAsync();

        foreach(var section in sectionsToDelete) {
            section.Deleted = true;
        }

        var tasksToDelete = await dbContext.Tasks
            .Where(t => deletedBoards.Contains(t.BoardId) && !t.Deleted)
            .ToListAsync();

        foreach(var task in tasksToDelete) {
            task.Deleted = true;
        }

        await dbContext.SaveChangesAsync();
    }
}
