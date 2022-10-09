using ProMag.Shared.DataTransferObjects.ReadDtos;

namespace ProMag.Server.Core.Domain.Supervisor;

public partial class Supervisor
{
    public async Task<IEnumerable<MainTaskReadDto>> GetMainTasksByProjectId(int projectId)
    {
        try
        {
            var entities = await _mainTaskRepository.GetByProjectId(projectId);

            SetCache(entities);

            return _mapper.Map<IEnumerable<MainTaskReadDto>>(entities);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}