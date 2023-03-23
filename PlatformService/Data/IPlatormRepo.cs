using PlatformService.Models;

namespace PlatformService.Data;

public interface IPlatormRepo
{
    bool SaveChanges();
    IEnumerable<Platform> GetAllPlatforms();
    Platform GetPlatformById(int id);
    void CreatePlatform(Platform platform);

}
