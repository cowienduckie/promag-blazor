﻿namespace ProMag.Server.Core.DataTransferObjects.CreateDtos;

public class ProjectCreateDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int StatusId { get; set; }
}