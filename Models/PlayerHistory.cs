using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZeroToHeroAPI.Enums;

namespace ZeroToHeroAPI.Models;

public class PlayerHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public int OldValue { get; set; }
    public int NewValue { get; set; }
    public PlayerActionEnum Action { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string PlayerId { get; set; }
    public Player Player { get; set; }
}