namespace ProMag.Client.Blazor.Infrastructure.Routes;

public static class ProjectEndpoints
{
    public static string Projects = "projects";

    public static string RequestById(int id)
    {
        return $"projects/{id}";
    }
}