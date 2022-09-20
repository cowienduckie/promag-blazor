using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ProMag.Client.Blazor.App.Shared;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using System.ComponentModel.DataAnnotations;

namespace ProMag.Client.Blazor.App.Pages.Projects;

public partial class Index
{
    [Parameter]
    public int ProjectId { get; set; }

    [Inject] private IProjectService ProjectService { get; set; } = null!;

    private ProjectReadDto Project { get; set; } = new ProjectReadDto();

    private KanbanBoard? _kanbanBoard { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var projects = await ProjectService.GetByIdAsync(ProjectId);

        Project = projects;
    }
}