using Blazor.Data;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Services;

public class WorkspaceService(IDbContextFactory<AppDbContext> contextFactory)
{
    public async Task<IEnumerable<Workspace>> GetAllWorkspacesAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Workspaces.ToListAsync();
    }
}