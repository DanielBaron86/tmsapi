using System.Text.Json.Serialization;

namespace TasksAPI.Models
{
    public struct ParamsModel
    {
        public ParamsModel()
        {
        }

        public int[] Locations { get; set; } = default!;
        public int[] GoodTypes { get; set; } = default!;
        public int[] GoodStatus { get; set; } = default!;
    }


    public struct SalesParamsModel
    {
        public SalesParamsModel()
        {
        }

        public int[] Locations { get; set; } = default!;
        public int[] Clerks { get; set; } = default!;
        public int[] OperationTypes { get; set; } = default!;
    }


    public class ReportsEntitiesModel : BaseModel
    {

        public int Id { get; set; }

        public string Description { get; set; } = String.Empty;
        public int ReportType { get; set; }
        public int ReportMode { get; set; }
        public string? Params { get; set; }

    }

    public struct GetReportsResults
    {
        public int ReportId { get; set; }
        public DateTime Date { get; set; }

    }

    public class CreateReportsEntitiesModel
    {
        [JsonRequired]
        public string Description { get; set; } = String.Empty;
        [JsonRequired]
        public int ReportMode { get; set; }
        public ParamsModel? Params { get; set; }
    }

    public class CreateSalesReportsEntitiesModel
    {
        [JsonRequired]
        public string Description { get; set; } = String.Empty;
        [JsonRequired]
        public int ReportMode { get; set; }
        public SalesParamsModel? Params { get; set; }
    }
}
