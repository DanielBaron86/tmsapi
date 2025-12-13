using AutoMapper;
using TasksAPI.Entities;
using TasksAPI.Models;

namespace TasksAPI.Profiles
{
    public class TasksProfiles : Profile
    {

        public TasksProfiles()
        {
            CreateMap<TasksEntities, TasksModel>();
            CreateMap<TasksEntities, TasksModelWithProcurements>();
            CreateMap<TasksEntities, TasksModelWithTransfer>();

            CreateMap<TasksEntitiesProcurements, ProcurementsSubtaskModel>();
            CreateMap<ProcurementsSubtaskModelForUpdate, TasksEntitiesProcurements>();

            CreateMap<TasksEntitiesTransfer, TasksEntities_TransferModel>();
            CreateMap<RejectedGoodsTransfer, TasksModelWithTransfer>();
            CreateMap<GoodsOrder, TasksEntitiesTransfer>();

            CreateMap<UpdateTasksModel, TasksEntities>();
            CreateMap<TransferSubtaskModelForUpdate, TasksEntitiesTransfer>();


        }
    }
}
