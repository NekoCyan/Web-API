using System;
using System.Collections.Generic;

namespace ControllerAPI_1721030861.Database.Models.Bai3;

public partial class RefreshToken
{
    public Guid Id { get; set; }

    public int AccountId { get; set; }

    public string Token { get; set; } = null!;

    public string JwtId { get; set; } = null!;

    public bool IsUsed { get; set; }

    public bool IsRevoked { get; set; }

    public DateTime IssuedAt { get; set; }

    public DateTime ExpiredAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public string? Notes { get; set; }
}
