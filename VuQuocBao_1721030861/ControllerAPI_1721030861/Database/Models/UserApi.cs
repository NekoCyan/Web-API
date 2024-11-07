namespace ControllerAPI_1721030861.Database.Models;

public partial class UserApi
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    /// <summary>
    /// 1: Admin; 2: Manager; 3: User
    /// </summary>
    public int RoleId { get; set; }

    public virtual ICollection<NewsApi> NewsApis { get; set; } = new List<NewsApi>();
}

public partial class UserApiDTO
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    //public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    /// <summary>
    /// 1: Admin; 2: Manager; 3: User
    /// </summary>
    public int RoleId { get; set; }
}
