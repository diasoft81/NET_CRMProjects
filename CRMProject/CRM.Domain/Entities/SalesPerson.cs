namespace CRM.Domain.Entities;

public class SalesPerson
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Region { get; set; } = null!;
    public DateTime JoinDate { get; set; }

    public ICollection<Lead> Leads { get; set; } = new List<Lead>();
    public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
    public ICollection<SalesTarget> SalesTargets { get; set; } = new List<SalesTarget>();
}