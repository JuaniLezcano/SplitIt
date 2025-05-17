namespace SplitIt.Application.Models;

public class Debt
{
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public float Amount { get; set; }

}
