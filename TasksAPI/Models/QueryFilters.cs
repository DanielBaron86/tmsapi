using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace TasksAPI.Models;

public class QueryFilters
{
    public QueryFilters()
    {
        pageSize = 100;
        pageNumber = 1;
    }
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
    public IEnumerable<QueryFields>? queryFields { get; set; }
}

public class QueryFields
{
    public string keyField { get; set; }
    public string keyValue { get; set; }
    public string method { get; set; }
}