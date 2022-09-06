using Microsoft.EntityFrameworkCore;
using ProMag.Server.Core.Domain.Entities;
using ProMag.Server.Core.Domain.Repositories;

namespace ProMag.Server.Infrastructure.Repositories;

public class MainTaskRepository : IMainTaskRepository
{
    private readonly DataContext _context;

    public MainTaskRepository(DataContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IEnumerable<MainTask> GetAll()
    {
        return _context.MainTasks
            .Where(mt => !mt.IsDelete)
            .AsNoTracking()
            .ToList();
    }

    public MainTask GetById(int id)
    {
        return _context.MainTasks
            .AsNoTracking()
            .FirstOrDefault(mt => !mt.IsDelete && mt.Id == id) ?? throw new InvalidOperationException();
    }

    public MainTask Create(MainTask entity)
    {
        _context.MainTasks.Add(entity);
        _context.SaveChanges();

        return entity;
    }

    public bool Update(MainTask entity)
    {
        if (!IsExist(entity.Id)) return false;

        entity.LastModified = DateTime.Now;

        _context.MainTasks.Update(entity);
        _context.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        if (!IsExist(id)) return false;

        var task = _context.MainTasks.First(mt => !mt.IsDelete && mt.Id == id);

        task.IsDelete = true;
        task.LastModified = DateTime.Now;

        _context.SaveChanges();

        return true;
    }

    public bool IsExist(int id)
    {
        return _context.MainTasks.Any(mt => !mt.IsDelete && mt.Id == id);
    }
}