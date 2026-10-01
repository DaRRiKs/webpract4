using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webpract4.Common;
using webpract4.DTOs;
using webpract4.Models;
using webpract4.Repositories;

namespace webpract4.Controllers;

[ApiController]
[Route("api/team")]
public class TeamController(ITeamRepository repository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ReturnResult<IEnumerable<TeamDto>>>> GetAll()
    {
        var teams = await repository.GetAllAsync();
        var result = mapper.Map<IEnumerable<TeamDto>>(teams);
        return Ok(new ReturnResult<IEnumerable<TeamDto>> { IsSuccess = true, Result = result });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReturnResult<TeamDto>>> Get(int id)
    {
        var team = await repository.GetAsync(id);
        if (team is null)
        {
            return NotFound(new ReturnResult<TeamDto> { IsSuccess = false, ErrorMessage = "Team not found." });
        }

        return Ok(new ReturnResult<TeamDto> { IsSuccess = true, Result = mapper.Map<TeamDto>(team) });
    }

    [HttpPost]
    public async Task<ActionResult<ReturnResult<TeamDto>>> Create(TeamDto? dto)
    {
        if (dto is null || string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.City))
        {
            return BadRequest(new ReturnResult<TeamDto> { IsSuccess = false, ErrorMessage = "Name and City are required." });
        }

        var team = mapper.Map<Team>(dto);
        team.Id = 0;
        await repository.CreateAsync(team);
        var result = new ReturnResult<TeamDto> { IsSuccess = true, Result = mapper.Map<TeamDto>(team) };
        return CreatedAtAction(nameof(Get), new { id = team.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ReturnResult<TeamDto>>> Update(int id, TeamDto? dto)
    {
        if (dto is null || dto.Id != id || string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.City))
        {
            return BadRequest(new ReturnResult<TeamDto> { IsSuccess = false, ErrorMessage = "Invalid team data." });
        }

        var team = await repository.GetAsync(id);
        if (team is null)
        {
            return NotFound(new ReturnResult<TeamDto> { IsSuccess = false, ErrorMessage = "Team not found." });
        }

        mapper.Map(dto, team);
        await repository.UpdateAsync(team);
        return Ok(new ReturnResult<TeamDto> { IsSuccess = true, Result = mapper.Map<TeamDto>(team) });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ReturnResult<TeamDto>>> Delete(int id)
    {
        var team = await repository.GetAsync(id);
        if (team is null)
        {
            return NotFound(new ReturnResult<TeamDto> { IsSuccess = false, ErrorMessage = "Team not found." });
        }

        await repository.DeleteAsync(id);
        return Ok(new ReturnResult<TeamDto> { IsSuccess = true, Result = mapper.Map<TeamDto>(team) });
    }

    [HttpGet("city/{city}")]
    public async Task<ActionResult<ReturnResult<IEnumerable<TeamDto>>>> GetByCity(string city)
    {
        var teams = await repository.GetByCityAsync(city);
        var result = mapper.Map<IEnumerable<TeamDto>>(teams);
        return Ok(new ReturnResult<IEnumerable<TeamDto>> { IsSuccess = true, Result = result });
    }
}
