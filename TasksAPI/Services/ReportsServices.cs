using AutoMapper;
using Castle.Core.Resource;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Services
{
    public class ReportsServices: IReportsServices
    {
        private readonly DatabaseConnectContext _DBContext;
        private readonly IMapper _mapper;

        public ReportsServices(DatabaseConnectContext context, IMapper mapper) {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _DBContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ReportsEntitiesModel> CreateInventoryReport(CreateReportsEntitiesModel InventoryReportsModel)
        {
           

            var newReportTask = _mapper.Map<ReportsEntities>(new ReportsEntitiesModel { ReportType = 1,ReportMode= InventoryReportsModel.ReportMode,Descrption = InventoryReportsModel.Descrption, Params = JsonConvert.SerializeObject(InventoryReportsModel.Params)  });
            _DBContext.Add(newReportTask);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<ReportsEntitiesModel>(newReportTask);
        }

        public async Task<ReportsEntitiesModel> CreateSalesReport(CreateSalesReportsEntitiesModel salesReportsMode)
        {

            var newReportTask = _mapper.Map<ReportsEntities>(new ReportsEntitiesModel { ReportType = 2, ReportMode= salesReportsMode.ReportMode,Descrption = salesReportsMode.Descrption, Params = JsonConvert.SerializeObject(salesReportsMode.Params) });
            _DBContext.Add(newReportTask);
            await _DBContext.SaveChangesAsync(CancellationToken.None);

            return _mapper.Map<ReportsEntitiesModel>(newReportTask);
        }

        public async Task<ReportsEntitiesResults> RetrieveReportResults(int reportResultsTaskId)
        {
            var result = await _DBContext.ReportsEntitiesResults
                .FirstOrDefaultAsync( t => t.Id == reportResultsTaskId) ??throw new ArgumentException("Task not found"); ;
            return result;
        }

        public async Task<int> RunReport(int reportTaskId)
        {
            var TaskToRun = _DBContext.ReportsEntities.FirstOrDefault(t => t.Id == reportTaskId) ??throw new ArgumentException("Task not found");
            if(TaskToRun.ReportType == 1)
            {
                return await RunInventoryReport(reportTaskId, TaskToRun);
            }
            return await RunSalesReport(reportTaskId, TaskToRun);
        }

        public async Task<int> RunInventoryReport(int reportTaskId, ReportsEntities TaskToRun)
        {
            
            var TaskParams = JsonConvert.DeserializeObject<ParamsModel>(TaskToRun.Params);
            
            if(TaskToRun.ReportMode ==1)
            {
                return await RunInventoryListReportint(reportTaskId, TaskParams);
            }

            if (TaskToRun.ReportMode == 2)
            {
                return await RunInventorySummaryReportint(reportTaskId, TaskParams);
            }

            return 0;
            
        }

        public async Task<int> RunSalesReport(int reportTaskId, ReportsEntities TaskToRun)
        {

            var TaskParams = JsonConvert.DeserializeObject<SalesParamsModel>(TaskToRun.Params);

            if (TaskToRun.ReportMode == 1)
            {
                return await RunSalesListReportint(reportTaskId, TaskParams);
            }

            if (TaskToRun.ReportMode == 2)
            {
                return await RunSalesSummaryReportint(reportTaskId, TaskParams);
            }

            return 0;

        }

        private async Task<int> RunInventoryListReportint (int reportTaskId,ParamsModel ParamsModel)
        {
            List<int> locations = ParamsModel.Locations.ToList();
            List<int> goodTypes = ParamsModel.GoodTypes.ToList();
            List<int> goodStatus = ParamsModel.GoodStatus.ToList();

   

            var reportResult = await _DBContext.GoodsTypesInstances
              .Where(t => !locations.Any() || locations.Contains(t.LocationId))
              .Where(t => !goodTypes.Any() || goodTypes.Contains(t.GoodModelId))
              .Where(t => !goodStatus.Any() || goodStatus.Contains(t.Status))
              .Include(e => e.GoodsTypes)
              .Select(e => new { e.Id, e.GoodsTypes.Name, e.Price, e.LocationId, e.serialNumber, e.Status })
          .ToListAsync();

            StringBuilder sb = new StringBuilder();
            sb.Append(new string("Id,Name,Price,LocationId,serialNumber,Status"));
            sb.Append("\r\n");

            foreach (var report in reportResult)
            {
                sb.Append($"{report.Id},{report.Name},{report.Price},{report.LocationId},{report.serialNumber},{report.Status}");
                sb.Append("\r\n");
            }

            var saveit = new ReportsEntitiesResults { ReportID = reportTaskId, RunDate = DateTime.UtcNow, ReportResults = sb.ToString() };
            _DBContext.Add(saveit);

            return await _DBContext.SaveChangesAsync(CancellationToken.None);
        }


        private async Task<int> RunInventorySummaryReportint(int reportTaskId, ParamsModel ParamsModel) {

            List<int> locations = ParamsModel.Locations.ToList();
            List<int> goodTypes = ParamsModel.GoodTypes.ToList();
            List<int> goodStatus = ParamsModel.GoodStatus.ToList();

            var reportResult = await _DBContext.GoodsTypesInstances
              .Where(t => !locations.Any() || locations.Contains(t.LocationId))
              .Where(t => !goodTypes.Any() || goodTypes.Contains(t.GoodModelId))
              .Where(t => !goodStatus.Any() || goodStatus.Contains(t.Status))
              .Include(e => e.GoodsTypes)  
              .GroupBy(e => new {  e.GoodsTypes.Name, e.GoodModelId, e.LocationId, e.Status })
              .Select( e => new { 
                  e.Key.GoodModelId,
                  e.Key.Name,
                  e.Key.LocationId,
                  e.Key.Status, 
                  Count = e.Count()
                  
              })
              .ToListAsync();

            StringBuilder sb = new StringBuilder();
            sb.Append(new string("Name,GoodModelId,LocationId,Status,Count"));
            sb.Append("\r\n");

            foreach (var report in reportResult)
            {
                sb.Append($"{report.Name},{report.GoodModelId},{report.LocationId},{report.Status},{report.Count}");
                sb.Append("\r\n");
            }

            var saveit = new ReportsEntitiesResults { ReportID = reportTaskId, RunDate = DateTime.UtcNow, ReportResults = sb.ToString() };
            _DBContext.Add(saveit);

            return await _DBContext.SaveChangesAsync(CancellationToken.None);
        }


        private async Task<int> RunSalesListReportint(int reportTaskId, SalesParamsModel ParamsModel) {
            List<int> locations = ParamsModel.Locations.ToList();
            List<int> clerks = ParamsModel.Clerks.ToList();
            List<int> operationTypes = ParamsModel.OperationTypes.ToList();

            var salesReport = await _DBContext.StoreCartsEntityDetails
                .Where(t => !locations.Any() || locations.Contains(t.StoreCartsEntity.storeLocation))
                .Where(t => !clerks.Any() || locations.Contains(t.StoreCartsEntity.clerktId))
                .Where(t => !operationTypes.Any() || locations.Contains(t.OperationType))
                .Include(t => t.StoreCartsEntity)
                .Where(t => t.StoreCartsEntity.Status == 2)
                .Where(t => t.StoreCartsEntity.CreatedDate.Date == DateTime.Now.Date)

                .Select(t => new { 
                    t.CartId,
                    t.StoreCartsEntity.clerktId,
                    t.OperationType,
                    t.StoreCartsEntity.Total,
                    t.StoreCartsEntity.Paid,
                    t.StoreCartsEntity.Remaining
                }
                )
                .Distinct()
                .ToListAsync();

            StringBuilder sb = new StringBuilder();
            sb.Append(new string("CartId,clerktId,OperationType,Total,Paid,Remaining"));
            sb.Append("\r\n");

            foreach (var report in salesReport)
            {
                sb.Append($"{report.CartId},{report.clerktId},{report.OperationType},{report.Total},{report.Paid},{report.Remaining}");
                sb.Append("\r\n");
            }
            var saveit = new ReportsEntitiesResults { ReportID = reportTaskId, RunDate = DateTime.UtcNow, ReportResults = sb.ToString() };
            _DBContext.Add(saveit);

            return await _DBContext.SaveChangesAsync(CancellationToken.None);
        }
        private async Task<int> RunSalesSummaryReportint(int reportTaskId, SalesParamsModel ParamsModel) {

            List<int> locations = ParamsModel.Locations.ToList();
            List<int> clerks = ParamsModel.Clerks.ToList();
            List<int> operationTypes = ParamsModel.OperationTypes.ToList();

//SELECT
//clerk, slocation, operation,
//SUM(total) total_stocks
//FROM
//(select distinct c.clerktId as clerk, c.storeLocation as slocation, p.OperationType as operation, c.Total as total from[StoreCartsEntity] c
//inner join StoreCartsEntityDetails p on c.Id = p.CartId) qop
//GROUP BY
//clerk, slocation, operation

            var joinTable = await _DBContext.StoreCartsEntityDetails
                            .Include( t => t.StoreCartsEntity)
                            .Where(t => !locations.Any() || locations.Contains(t.StoreCartsEntity.storeLocation))
                            .Where(t => !clerks.Any() || locations.Contains(t.StoreCartsEntity.clerktId))
                            .Where(t => !operationTypes.Any() || locations.Contains(t.OperationType))
                            .Where(t => t.StoreCartsEntity.Status == 2)
                            .Where(t => t.StoreCartsEntity.CreatedDate.Date == DateTime.Now.Date)
                            .Select(t => new { t.StoreCartsEntity.clerktId, t.StoreCartsEntity.storeLocation, t.OperationType,t.StoreCartsEntity.Total })
                            .Distinct()
                            .ToListAsync();

            var salesReport = joinTable
                .GroupBy( l => new { l.clerktId ,l.storeLocation,l.OperationType})
                .Select(s => new { s.First().clerktId , s.First().storeLocation, s.First().OperationType,Sum = s.Sum( x => x.Total) })
                .ToList();


  

            StringBuilder sb = new StringBuilder();
            sb.Append(new string("clerktId,storeLocation,OperationType,Total"));
            sb.Append("\r\n");

            foreach (var report in salesReport)
            {
                sb.Append($"{report.clerktId},{report.storeLocation},{report.OperationType},{report.Sum}");
                sb.Append("\r\n");
            }
            var saveit = new ReportsEntitiesResults { ReportID = reportTaskId, RunDate = DateTime.UtcNow, ReportResults = sb.ToString() };
            _DBContext.Add(saveit);

            return await _DBContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<int> DeleteReport(int reportId)
        {
            var report = _DBContext.ReportsEntities.FirstOrDefault(t => t.Id == reportId) ?? throw new ArgumentException($"Report {reportId} not found");
            _DBContext.Remove(report);
            return await _DBContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<int> DeleteReportResults(int resultsId)
        {
            var reportResults = _DBContext.ReportsEntitiesResults.FirstOrDefault(t => t.Id == resultsId) ?? throw new ArgumentException($"Report results {resultsId} not found");
            _DBContext.Remove(reportResults);
            return await _DBContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}
