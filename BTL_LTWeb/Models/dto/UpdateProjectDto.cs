using System.ComponentModel;

namespace BTL_LTWeb.Models.dto;

public class UpdateProjectDto
{
    public string Name { get; set; }
    // public DateTime StartDate { get; set; }
    // public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public string Description { get; set; }
}