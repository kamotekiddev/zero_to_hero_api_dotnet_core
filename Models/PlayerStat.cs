using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroToHeroAPI.Models;

public class PlayerStat
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public string Id { get; set; }

    [Column("current_level")] public int CurrentLevel { get; set; } = 1;

    [Column("max_level")] public int MaxLevel { get; set; } = 100;

    [Column("current_exp")] public int CurrentExp { get; set; } = 0;

    [Column("next_level_exp")] public int NextLevelExp { get; set; } = 100;

    [Required] [Column("user_id")] public string UserId { get; set; }

    public User User { get; set; }
}