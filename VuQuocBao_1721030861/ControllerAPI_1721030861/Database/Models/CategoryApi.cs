namespace ControllerAPI_1721030861.Database.Models;

public partial class CategoryApi
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<NewsApi> NewsApis { get; set; } = new List<NewsApi>();
}

public partial class CategoryApiDTO
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;
}
