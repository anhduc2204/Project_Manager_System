namespace BTL_LTWeb.Models.dto;

public class UserProjectViewDto
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public bool IsPM { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}