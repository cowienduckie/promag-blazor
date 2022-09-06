using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;

namespace ProMag.Server.Infrastructure.Repositories;

public class SubTaskRepository : ISubTaskRepository
{
    private readonly DataContext _context;

    public SubTaskRepository(DataContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IEnumerable<SubTask> GetAll()
    {
        return _context.SubTasks
            .Where(st => !st.IsDelete)
            .AsNoTracking()
            .ToList();
    }

    public SubTask GetById(int id)
    {
        return _context.SubTasks
            .AsNoTracking()
            .FirstOrDefault(st => !st.IsDelete && st.Id == id) ?? throw new InvalidOperationException();
    }

    public SubTask Create(SubTask entity)
    {
        _context.SubTasks.Add(entity);
        _context.SaveChanges();

        return entity;
    }

    public bool Update(SubTask entity)
    {
        if (!IsExist(entity.Id)) return false;

        entity.LastModified = DateTime.Now;

        _context.SubTasks.Update(entity);
        _context.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        if (!IsExist(id)) return false;

        var task = _context.SubTasks.First(st => !st.IsDelete && st.Id == id);

        task.IsDelete = true;
        task.LastModified = DateTime.Now;

        _context.SaveChanges();

        return true;
    }

    public bool IsExist(int id)
    {
        return _context.SubTasks.Any(st => !st.IsDelete && st.Id == id);
    }
}