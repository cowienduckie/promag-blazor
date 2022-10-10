using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProMag.Client.Blazor.Infrastructure.Services.Interfaces;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;

namespace ProMag.Client.Blazor.App.Pages.Projects;

public partial class ProjectDetail
{
    #region Params

    [Parameter] public int ProjectId { get; set; }

    #endregion

    #region Board Events

    private void TaskUpdated(MudItemDropInfo<MainTaskReadDto> dropItem)
    {
        dropItem.Item.Status.Name = dropItem.DropzoneIdentifier;
    }

    #endregion

    #region Kanban Classes

    public class Section
    {
        public Section(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Section()
        {
            Id = NO_STATUS_ID;
            Name = NO_STATUS;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool NewTaskOpen { get; set; }
        public string NewTaskName { get; set; } = string.Empty;
    }

    #endregion

    #region Props

    [Inject] private IProjectService ProjectService { get; set; } = null!;

    [Inject] private IMainTaskService MainTaskService { get; set; } = null!;

    private const int NO_STATUS_ID = 0;
    private const string NO_STATUS = "No Status";
    private static List<Section> _baseSections = new();

    private ProjectReadDto Project { get; set; } = new();
    private List<MainTaskReadDto> Tasks { get; set; } = new();
    private List<Section> Sections { get; set; } = new();
    private MudDropContainer<MainTaskReadDto>? _dropContainer = new();

    #endregion

    #region Component Render

    protected override async Task OnInitializedAsync()
    {
        var sections = await ProjectService.GetSectionsAsync();

        _baseSections = sections
            .Select(s => new Section(s.Id, s.Name))
            .ToList();

        Sections = _baseSections;
    }

    protected override async Task OnParametersSetAsync()
    {
        var mainTasks = await MainTaskService.GetByProjectId(ProjectId);

        Project = await ProjectService.GetByIdAsync(ProjectId);

        Tasks = mainTasks?.ToList() ?? new List<MainTaskReadDto>();

        RefreshContainer();
    }

    #endregion

    #region Board Functions

    private async Task AddTask(Section section)
    {
        MainTaskCreateDto newTask = new()
        {
            Name = section.NewTaskName,
            ProjectId = Project.Id,
            StatusId = section.Id
        };

        try
        {
            var createdTask = await MainTaskService.CreateAsync(newTask);

            Tasks.Add(createdTask);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            // TODO: Pop-up error msg
        }
        finally
        {
            section.NewTaskName = string.Empty;
            section.NewTaskOpen = false;

            RefreshContainer();
        }
    }

    private async Task DeleteTask()
    {
        try
        {
            await MainTaskService.DeleteAsync(_currentTask.Id);

            Tasks.Remove(_currentTask);

            _currentTask = new MainTaskReadDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            // TODO: Pop-up error msg
        }
        finally
        {
            RefreshContainer();
        }
    }

    private void RefreshContainer()
    {
        StateHasChanged();
        _dropContainer?.Refresh();
    }

    #endregion

    #region Task Detailed Drawer

    private MainTaskReadDto _currentTask = new();
    private bool _drawerOpen;

    private void OpenDrawer()
    {
        _drawerOpen = true;
    }

    private void CloseDrawer()
    {
        _drawerOpen = false;
    }

    private void ToggleDrawer()
    {
        _drawerOpen = !_drawerOpen;
    }

    #endregion
}