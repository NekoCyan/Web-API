namespace ControllerAPI_1721030861.Database.Models.Bai3;

public partial class Account
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public string UserName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public string? Phone { get; set; }

    public DateTime? Birthday { get; set; }

    public int? AddressId { get; set; }

    public int? ImageId { get; set; }

    public int Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public string? Notes { get; set; }

    public virtual AccountType? AccountNavigation { get; set; }
}
