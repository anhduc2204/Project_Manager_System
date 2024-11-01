namespace BTL_LTWeb.Models.dto;

public class CreateProjectDto
{
    public string ProjectName { get; set; }
    // public DateTime StartDate { get; set; }
    // public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public string Description { get; set; }
}

public enum ProjectStatus
{
    Cancelled = -1,
    NotStart =0,
    OnProgress =1,
    Completed = 2
}