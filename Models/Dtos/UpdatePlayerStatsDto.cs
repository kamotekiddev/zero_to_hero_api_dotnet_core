using System.ComponentModel.DataAnnotations;

namespace ZeroToHeroAPI.Models.Dtos;

public class UpdatePlayerStatsDto
{
    [Required] public int ExpGained { get; set; }
}