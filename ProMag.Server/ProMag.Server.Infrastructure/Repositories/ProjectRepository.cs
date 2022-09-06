using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;

namespace ProMag.Server.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DataContext _context;

    public ProjectRepository(DataContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IEnumerable<Project> GetAll()
    {
        return _context.Projects
            .Where(p => !p.IsDelete)
            .AsNoTracking()
            .ToList();
    }

    public Project GetById(int id)
    {
        return _context.Projects
            .AsNoTracking()
            .FirstOrDefault(p => !p.IsDelete && p.Id == id) ?? throw new InvalidOperationException();
    }

    public Project Create(Project entity)
    {
        _context.Projects.Add(entity);
        _context.SaveChanges();

        return entity;
    }

    public bool Update(Project entity)
    {
        if (!IsExist(entity.Id)) return false;

        entity.LastModified = DateTime.Now;

        _context.Projects.Update(entity);
        _context.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        if (!IsExist(id)) return false;

        var project = _context.Projects.First(p => !p.IsDelete && p.Id == id);

        project.IsDelete = true;
        project.LastModified = DateTime.Now;

        _context.SaveChanges();

        return true;
    }

    public bool IsExist(int id)
    {
        return _context.Projects.Any(p => !p.IsDelete && p.Id == id);
    }
}