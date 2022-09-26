using ProMag.Server.Core.Domain.Entities;
using ProMag.Shared.DataTypes;
using ProMag.Shared.Models;

namespace ProMag.Server.Core.Domain.Supervisor;

public interface ISupervisor
{
    PagedList<TEntity> GetPagedList<TEntity>(IList<TEntity> items, int pageIndex, int pageSize);

    Task<IEnumerable<TReadDto>> GetAllAsync<TEntity, TReadDto>() where TEntity : BaseEntity;

    Task<TReadDto?> GetByIdAsync<TEntity, TReadDto>(int id) where TEntity : BaseEntity;

    Task<TReadDto> CreateAsync<TEntity, TCreateDto, TReadDto>(TCreateDto createDto) where TEntity : BaseEntity;

    Task<bool> UpdateAsync<TEntity, TUpdateDto>(int id, TUpdateDto updateDto) where TEntity : BaseEntity;

    Task<bool> DeleteAsync<TEntity>(int id) where TEntity : BaseEntity;

    #region Projects
    Task<IEnumerable<SectionModel>> GetSectionsAsync();
    #endregion
}