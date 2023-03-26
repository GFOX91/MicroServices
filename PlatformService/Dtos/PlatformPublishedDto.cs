namespace PlatformService.Dtos;

/// <summary>
/// Used to send created/updated Platform data to message bus
/// </summary>
public class PlatformPublishedDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Event { get; set; }
}
