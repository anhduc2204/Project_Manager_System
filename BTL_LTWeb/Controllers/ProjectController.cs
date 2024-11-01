using BTL_LTWeb.Models.dto;
using BTL_LTWeb.Models.Service.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BTL_LTWeb.Controllers;

[Authorize]
[Route("projects")]
public class ProjectController : Controller
{
    private readonly IProjectRepository _projectRepository;


    public ProjectController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Project([FromBody]CreateProjectDto dto)
    {
        var response = await _projectRepository.CreateProject(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet()]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _projectRepository.GetAllProject();

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        var project = await _projectRepository.GetProjectById(id);

        return Ok(project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject([FromRoute]Guid id,[FromBody]UpdateProjectDto dto)
    {
        var response = await _projectRepository.UpdateProject(id, dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var response = await _projectRepository.DeleteProject(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}