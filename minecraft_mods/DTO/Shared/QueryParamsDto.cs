﻿using System.Text.Json.Serialization;

namespace DTO.Shared;

public class QueryParamsDto<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    [JsonIgnore]
    public string Search { get; set; } = "";
    [JsonIgnore]
    public string SortBy { get; set; } = "CreatedAt";
    [JsonIgnore]
    public string OrderBy { get; set; } = "desc";
    [JsonIgnore]
    public List<Guid> VersionIds { get; set; } = new();
    [JsonIgnore]
    public List<Guid> ModLoaderIds { get; set; } = new();
    [JsonIgnore]
    public List<Guid> DifficultyIds { get; set; } = new();
    [JsonIgnore]
    public List<Guid> TagIds { get; set; } = new();
    [JsonIgnore]
    public List<Guid> FocusIds { get; set; } = new();
    
}