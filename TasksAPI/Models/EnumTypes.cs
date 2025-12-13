namespace TasksAPI.Models
{
    public enum EnumTypes
    {
        NONE,
        NOTUSABLE,
        CLIENT,
        CLERK,
        SUPERVISOR,
    }

    public enum LocationTypesList
    {
        NONE,
        WAREHOUES,
        STORE,
        CLIENT,
        SUPPLIER
    }

    public enum GoodsStatus
    {
        NONE,
        PENDING,
        IN_TRANSIT,
        AVAILABLE,
        DEFECTED,
        IN_REPAIR,
        SOLD,
        RETURNED,
        LOST,
        DELETED,
        RESERVED
    }

    public enum TaskTypes
    {
        NONE,
        PROCUREMENT,
        TRANSFER,

    }
    public enum TaskTypesStatus
    {
        NONE,
        PENDING,
        OPEN,
        CLOSED,
        COMPLETE
    }

    public enum DBEntityStatus
    {
        NONE,
        ACTIVE,
        DISABLED,
        MARK_FOR_DELETE
    }
}
