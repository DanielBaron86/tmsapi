using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TasksAPI.Entities;
using TasksAPI.Models;

namespace TasksAPI.Interfaces
{
    public interface IReportsServices
    {
        Task<ReportsEntitiesModel> CreateInventoryReport(CreateReportsEntitiesModel InventoryReportsModel);
        Task<ReportsEntitiesModel> CreateSalesReport(CreateSalesReportsEntitiesModel salesReportsMode);

        Task<int> RunInventoryReport(int reportTaskId, ReportsEntities TaskToRun);
        Task<int> RunSalesReport(int reportTaskId, ReportsEntities TaskToRun);

        Task<ReportsEntitiesResults> RetrieveReportResults(int reportResultsTaskId);

        Task<int> RunReport(int reportTaskId);
        Task<int> DeleteReport(int reportId);
        Task<int> DeleteReportResults(int resultsId);

    }
}
