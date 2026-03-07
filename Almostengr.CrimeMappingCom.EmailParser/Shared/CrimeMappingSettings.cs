namespace Almostengr.CrimeMappingCom.EmailParser.Shared;

public class CrimeMappingSettings
{
    public string Hostname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int PortNumber { get; set; }
    public string OutputDirectory { get; set; }
    public string Separator { get; set; } = "—————————";
}