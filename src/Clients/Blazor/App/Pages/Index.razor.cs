using Microsoft.AspNetCore.Components;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.DataTransferObjects.ReadDtos;

namespace ProMag.Client.Blazor.App.Pages;
public partial class Index
{
    [Inject]
    private IProjectService ProjectService { get; set; } = null!;

    private List<ProjectReadDto> Projects { get; set; } = new List<ProjectReadDto>();

    protected override async Task OnInitializedAsync()
    {
        var projects = await ProjectService.GetAllAsync();

        Projects = (List<ProjectReadDto>)projects;
    }
}