using Microsoft.EntityFrameworkCore;
using webpract4.Data;
using webpract4.Models;

namespace webpract4.Repositories;

public class TeamRepository(AppDbContext context) : ITeamRepository
{
    public async Task<IEnumerable<Team>> GetAllAsync()
    {
        return await context.Teams.ToListAsync();
    }

    public async Task<Team?> GetAsync(int id)
    {
        return await context.Teams.FindAsync(id);
    }

    public async Task CreateAsync(Team team)
    {
        context.Teams.Add(team);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Team team)
    {
        context.Teams.Update(team);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var team = await context.Teams.FindAsync(id);
        if (team is not null)
        {
            context.Teams.Remove(team);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Team>> GetByCityAsync(string city)
    {
        return await context.Teams.Where(team => team.City == city).ToListAsync();
    }
}
