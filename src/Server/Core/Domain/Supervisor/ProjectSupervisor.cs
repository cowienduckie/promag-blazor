using ProMag.Shared.Models;
using TaskStatus = ProMag.Server.Core.Domain.Entities.TaskStatus;

namespace ProMag.Server.Core.Domain.Supervisor;

public partial class Supervisor
{
    public async Task<IEnumerable<SectionModel>> GetSectionsAsync()
    {
         try
         {
             const string sectionsCacheKey = "SECTIONS_CACHE";

             if (!GetCache(sectionsCacheKey, out IEnumerable<TaskStatus> sections))
             {
                 sections = await _projectRepository.GetSectionsAsync();
             }
             SetCache(sections, sectionsCacheKey);

             return _mapper.Map<IEnumerable<SectionModel>>(sections);
         }
         catch (Exception e)
         {
             Console.WriteLine(e);
             throw;
         }
    }
}