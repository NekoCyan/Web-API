namespace ControllerAPI_1721030861.Database.Models.Bai2;

public partial class BankType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime Timer { get; set; }

    public virtual ICollection<Bank> Banks { get; set; } = new List<Bank>();
}

public partial class BankTypeDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime Timer { get; set; }
}
