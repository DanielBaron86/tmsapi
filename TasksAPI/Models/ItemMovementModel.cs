namespace TasksAPI.Models
{
    public class ItemMovementModel : BaseModel
    {
        public int goodId { get; set; }

        public int FromLocation { get; set; }
        public int ToLocation { get; set; }

        public int FromStatus { get; set; }
        public int ToStatus { get; set; }

        public int UserId { get; set; }
    }


    public class CreateItemMovementModel
    {
        public int goodId { get; set; }

        public int FromLocation { get; set; }
        public int ToLocation { get; set; }

        public int FromStatus { get; set; }
        public int ToStatus { get; set; }

        public int UserId { get; set; }
    }


}
