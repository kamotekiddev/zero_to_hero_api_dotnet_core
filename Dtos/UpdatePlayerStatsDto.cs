using System.ComponentModel.DataAnnotations;

namespace ZeroToHeroAPI.Dtos;

public class UpdatePlayerStatsDto
{
    [Required] public int ExpGained { get; set; }
}