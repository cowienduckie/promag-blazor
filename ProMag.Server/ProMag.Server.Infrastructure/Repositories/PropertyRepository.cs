using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;

namespace ProMag.Server.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly DataContext _context;

    public PropertyRepository(DataContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IEnumerable<Property> GetAll()
    {
        return _context.Properties
            .Where(p => !p.IsDelete)
            .AsNoTracking()
            .ToList();
    }

    public Property GetById(int id)
    {
        return _context.Properties
            .AsNoTracking()
            .FirstOrDefault(p => !p.IsDelete && p.Id == id) ?? throw new InvalidOperationException();
    }

    public Property Create(Property entity)
    {
        _context.Properties.Add(entity);
        _context.SaveChanges();

        return entity;
    }

    public bool Update(Property entity)
    {
        if (!IsExist(entity.Id)) return false;

        entity.LastModified = DateTime.Now;

        _context.Properties.Update(entity);
        _context.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        if (!IsExist(id)) return false;

        var task = _context.Properties.First(p => !p.IsDelete && p.Id == id);

        task.IsDelete = true;
        task.LastModified = DateTime.Now;

        _context.SaveChanges();

        return true;
    }

    public bool IsExist(int id)
    {
        return _context.Properties.Any(p => !p.IsDelete && p.Id == id);
    }
}