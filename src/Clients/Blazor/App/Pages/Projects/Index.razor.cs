using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
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

    protected override async Task OnInitializedAsync()
    {
        var projects = await ProjectService.GetByIdAsync(ProjectId);

        Project = projects;
    }

    #region Kanban Board
    private MudDropContainer<MainTaskReadDto>? _dropContainer;

    #endregion
}