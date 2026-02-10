using System.Text.Json.Serialization;

namespace TasksAPI.Models;

public class QueryFilters
{
    public QueryFilters()
    {
        pageSize = 10;
        pageNumber = 1;
    }
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
    [JsonRequired]
    public IEnumerable<QueryFields> queryFields { get; set; }
}

public class QueryFields
{
    [JsonRequired]
    public string keyField { get; set; }
    [JsonRequired]
    public string keyValue { get; set; }
}