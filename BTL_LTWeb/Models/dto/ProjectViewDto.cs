namespace BTL_LTWeb.Models.dto;

public class ProjectViewDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<UserProjectViewDto> UserProject { get; set; }
    public int Status { get; set; }
}