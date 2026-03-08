using System.ComponentModel.DataAnnotations;

namespace Almostengr.CrimeMappingCom.EmailParser.Shared;

public sealed class CrimeMappingSettings
{
    [Required]
    public string Hostname { get; init; }

    [Required]
    public string Username { get; init; }

    [Required]
    public string Password { get; init; }

    [Required]
    public int PortNumber { get; init; } = 993;

    [Required]
    public string OutputDirectory { get; init; }

    public string Separator { get; init; } = "—————————";
}