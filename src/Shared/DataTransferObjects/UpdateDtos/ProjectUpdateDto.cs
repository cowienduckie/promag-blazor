﻿namespace ProMag.Shared.DataTransferObjects.UpdateDtos;

public class ProjectUpdateDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int StatusId { get; set; }
}