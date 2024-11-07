using System;
using System.Collections.Generic;

namespace Minimal_API.Database.Models;

public partial class CategoryApi
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<NewsApi> NewsApis { get; set; } = new List<NewsApi>();
}
