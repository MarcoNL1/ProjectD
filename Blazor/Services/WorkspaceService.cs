using Blazor.Data;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Services;

public class WorkspaceService(IDbContextFactory<AppDbContext> contextFactory)
{
    public async Task<IEnumerable<Workspace>> GetAllWorkspacesAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Workspaces
            .Include(w => w.Room)
            .ToListAsync();
    }
    
    public async Task<Workspace?> GetWorkspaceByIdAsync(Guid id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Workspaces.FirstOrDefaultAsync(w => w.Id == id);
    }
    
    public async Task<Workspace> CreateWorkspaceAsync(Workspace workspace)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        await context.Workspaces.AddAsync(workspace);
        await context.SaveChangesAsync();
        return workspace;
    }
    
    public async Task<Workspace?> UpdateWorkspaceAsync(Workspace workspace)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var existingWorkspace = await context.Workspaces.FirstOrDefaultAsync(w => w.Id == workspace.Id);
        if (existingWorkspace == null) return null;
        // idk of hier wat moet voor workspaces
        await context.SaveChangesAsync();
        return existingWorkspace;
    }
    
    public async Task DeleteWorkspaceAsync(Guid id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var workspace = await context.Workspaces.FirstOrDefaultAsync(r => r.Id == id);
        if (workspace is not null)
        {
            context.Workspaces.Remove(workspace);
            await context.SaveChangesAsync();
        }
    }
}