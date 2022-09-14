using Newtonsoft.Json.Linq;

namespace ProMag.Shared.DataTypes;

public interface IPagedList<T> : IList<T>
{
    int PageIndex { get; }
    int PageSize { get; }
    int TotalCount { get; }
    int TotalPage { get; }
    bool HasPreviousPage { get; }
    bool HasNextPage { get; }

    JObject ToJObject();
}

[Serializable]
public class PagedList<T> : List<T>, IPagedList<T>
{
    public int PageIndex { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    public int TotalPage { get; }

    public bool HasPreviousPage => PageIndex > 0;

    public bool HasNextPage => PageIndex < TotalPage;

    public PagedList()
    {
        PageIndex = 0;
        PageIndex = 0;
        TotalCount = 0;
        TotalPage = 0;
    }

    public PagedList(IQueryable<T> source, int pageIndex, int pageSize, int totalCount)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPage = TotalCount / PageSize;

        if (TotalCount % PageSize > 0) ++TotalPage;

        AddRange(source);
    }

    public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var totalCount = source.Count();

        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPage = TotalCount / PageSize;

        if (TotalCount % PageSize > 0) ++TotalPage;

        AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize));
    }

    public PagedList(IList<T> source, int pageIndex, int pageSize, int totalCount)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPage = TotalCount / PageSize;

        if (TotalCount % PageSize > 0) ++TotalPage;

        AddRange(source);
    }

    public PagedList(IList<T> source, int pageIndex, int pageSize)
    {
        var totalCount = source.Count;

        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPage = TotalCount / PageSize;

        if (TotalCount % PageSize > 0) ++TotalPage;

        AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize));
    }

    public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPage = TotalCount / PageSize;

        if (TotalCount % PageSize > 0) ++TotalPage;

        AddRange(source);
    }

    public JObject ToJObject()
    {
        return new JObject(this);
    }
}