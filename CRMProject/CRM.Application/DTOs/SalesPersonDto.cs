namespace CRM.Application.DTOs;

public class SalesPersonDto
{
    public string Name { get; set; } = null!;
    public string Region { get; set; } = null!;
    public DateTime JoinDate { get; set; }
}