namespace ControllerAPI_1721030861.Database.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string PubName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? Status { get; set; }

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}

public partial class PublisherDTO
{
    public int Id { get; set; }

    public string PubName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? Status { get; set; }
}
