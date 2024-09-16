using System;
using System.Collections.Generic;

namespace ControllerAPI_1721030861.Database.Models.Bai3;

public partial class Contact
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? Respponse { get; set; }

    /// <summary>
    /// The member you want to contact has an account
    /// </summary>
    public int? AccountId { get; set; }

    /// <summary>
    /// Account is adviser
    /// </summary>
    public int? AdviseId { get; set; }

    public int? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Notes { get; set; }
}
