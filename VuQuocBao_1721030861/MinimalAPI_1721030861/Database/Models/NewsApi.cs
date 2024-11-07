using System;
using System.Collections.Generic;

namespace Minimal_API.Database.Models;

public partial class NewsApi
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Detail { get; set; } = null!;

    public string ImageFile { get; set; } = null!;

    public DateOnly PublishDate { get; set; }

    public int UserId { get; set; }

    public virtual CategoryApi Category { get; set; } = null!;

    public virtual UserApi User { get; set; } = null!;
}
