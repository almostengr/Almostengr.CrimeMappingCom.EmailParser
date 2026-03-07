using System.ComponentModel.DataAnnotations;

namespace Almostengr.CrimeMappingCom.EmailParser.Shared;

public class CrimeMappingSettings
{
    [Required]
    public string Hostname { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public int PortNumber { get; set; } = 993;

    [Required]
    public string OutputDirectory { get; set; }

    public string Separator { get; set; } = "—————————";
}