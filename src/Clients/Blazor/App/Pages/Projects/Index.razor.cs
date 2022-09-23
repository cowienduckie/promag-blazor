using Microsoft.AspNetCore.Components;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.DataTransferObjects.ReadDtos;

namespace ProMag.Client.Blazor.App.Pages.Projects;

public partial class Index
{
    [Parameter] public int ProjectId { get; set; }

    [Inject] private IProjectService ProjectService { get; set; } = null!;

    private ProjectReadDto Project { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        var project = await ProjectService.GetByIdAsync(ProjectId);

        Project = project;
    }
}