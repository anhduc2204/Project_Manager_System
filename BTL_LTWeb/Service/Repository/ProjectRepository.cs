using System.Security.Claims;
using BTL_LTWeb.Models.dto;
using BTL_LTWeb.Models.Service.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWeb.Models.Service.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProjectRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<Responsedto> CreateProject(CreateProjectDto dto)
    {
       var userIdText = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdText)) return new Responsedto() { IsSuccess = false, ErrorMessage = "403" };
        var userId = Guid.Parse(userIdText);
        var isAlreadyName =
            await _context.Projects
                .Include(u=>u.UserProjects)
                .AnyAsync(
                u => u.Name.ToLower().Trim() == dto.ProjectName.ToLower().Trim() && u.UserProjects.FirstOrDefault()!.UserId==userId);
       if (isAlreadyName) return new Responsedto() { IsSuccess = false, ErrorMessage = "Tên dự án đã tồn tại" };
        var project = new Project()
        {
            Id = Guid.NewGuid(),
            Name = dto.ProjectName,
            Description = dto.Description,
            CreatedBy = Guid.Parse(userIdText),
            CreatedAt = DateTime.UtcNow,
            Status = (int)dto.Status,
        };
        var userProject = new UserProject()
        {
            UserId = userId,
            ProjectId = project.Id,
            IsPM = true
        };
        await _context.Projects.AddAsync(project);
        await _context.UserProjects.AddAsync(userProject);
        var result = await _context.SaveChangesAsync();
        if (result>0) return new Responsedto() { IsSuccess = true, ErrorMessage = "Success" };

        return new Responsedto() { IsSuccess = false, ErrorMessage = "Đã xảy ra lỗi khi tạo dự án" };

    }

    public async Task<ICollection<ProjectDto>> GetAllProject()
    {
        var userIdText = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse(userIdText!);
        var projects = await _context.UserProjects
            .Include(u => u.Project)
            .Where(u => u.UserId == userId)
            .Select(u=>new ProjectDto()
            {
                Id = u.Project.Id,
                ProjectName = u.Project.Name
            }).ToListAsync();
        return projects;
    }

    public async Task<ProjectViewDto> GetProjectById(Guid id)
    {
        var project = await _context.Projects
            .Include(u => u.UserProjects)
            .ThenInclude(u => u.User)
            .FirstOrDefaultAsync(u => u.Id == id);
        return new ProjectViewDto()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            UserProject = project.UserProjects.Select(u => new UserProjectViewDto()
            {
                ProjectId = u.ProjectId,
                UserId = u.UserId,
                IsPM = u.IsPM,
                Name = u.User.FullName,
                Email = u.User.Email
            }).ToList()
        };
    }

    public async Task<Responsedto> UpdateProject(Guid id, UpdateProjectDto dto)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(u => u.Id == id);
        if (project == null) return new Responsedto() { IsSuccess = false, ErrorMessage = "404" };
        var userIdText = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdText)) return new Responsedto() { IsSuccess = false, ErrorMessage = "403" };
        var userId = Guid.Parse(userIdText);
        var isAlreadyName =
            await _context.UserProjects
                .Include(u=>u.Project)
                .AnyAsync(
                    u => u.Project.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && u.UserId==userId &&
                        u.ProjectId!=id);
        if (isAlreadyName) return new Responsedto() { IsSuccess = false, ErrorMessage = "Tên dự án đã tồn tại" };

        project.Name = dto.Name;
        project.Description = dto.Description;
        project.Status = (int)dto.Status;

         _context.Projects.Update(project);
         var result = await _context.SaveChangesAsync();
         if (result>0)
         {
             return new Responsedto() { IsSuccess = true, ErrorMessage = "Update Successfully" };
         }


         return new Responsedto() { IsSuccess = false, ErrorMessage = "Error Update Project" };

    }

    public async Task<Responsedto> DeleteProject(Guid id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(u => u.Id == id);
        if (project == null) return new Responsedto() { IsSuccess = false, ErrorMessage = "404" };

        _context.Projects.Remove(project);
        var result = await _context.SaveChangesAsync();
        if (result>0)
        {
            return new Responsedto() { IsSuccess = true, ErrorMessage = "Delete Successfully" };
        }

        return new Responsedto() { IsSuccess = false, ErrorMessage = "Error deleting project" };
    }
}