using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TasksAPI.Entities;
using System.Text;
using System.Collections;
using Newtonsoft.Json.Linq;

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


    public class ReportsEntitiesModel :BaseModel
    {
 
        public int Id { get; set; }
        
        public string Descrption { get; set; } = String.Empty;
        public int ReportType { get; set; }
        public int ReportMode { get; set; }
        public string? Params { get;set; }
        
    }

    public struct GetReportsResults
    {
        public int reportId { get; set; }
        public DateTime date { get; set; }

    }

    public class CreateReportsEntitiesModel
    {
        public string Descrption { get; set; } = String.Empty;
        public int ReportMode { get; set; }
        public ParamsModel Params { get; set; }
    }

    public class CreateSalesReportsEntitiesModel
    {
        public string Descrption { get; set; } = String.Empty;
        public int ReportMode { get; set; }
        public SalesParamsModel Params { get; set; }
    }
}
