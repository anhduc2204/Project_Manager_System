using BTL_LTWeb.Models.dto;

namespace BTL_LTWeb.Models.Service.IRepository;

public interface IProjectRepository
{
    public Task<Responsedto> CreateProject(CreateProjectDto dto);
    public Task<ICollection<ProjectDto>> GetAllProject();
    public Task<ProjectViewDto> GetProjectById(Guid id);
    public Task<Responsedto> UpdateProject(Guid id,UpdateProjectDto dto);
    public Task<Responsedto> DeleteProject(Guid id);
}