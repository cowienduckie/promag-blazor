using ProMag.Server.Core.Domain.Entities;
using ProMag.Shared.Models;
using TaskStatus = ProMag.Server.Core.Domain.Entities.TaskStatus;

namespace ProMag.Server.Core.Domain.Supervisor;

public partial class Supervisor : ISupervisor
{
    public async Task<IEnumerable<SectionModel>> GetSectionsAsync()
    {
         try
        {
            const string SECTIONS_CACHE_KEY = "SECTIONS_CACHE";

            if (!GetCache(SECTIONS_CACHE_KEY, out IEnumerable<TaskStatus> sections))
            {
                sections = await _projectRepository.GetSectionsAsync();
            }
            SetCache(sections, SECTIONS_CACHE_KEY);

            return _mapper.Map<IEnumerable<SectionModel>>(sections);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}