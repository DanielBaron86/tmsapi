using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TasksAPI.Entities
{
    public class StoreCartsEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int clerktId { get; set; }
        public int storeLocation { get; set; }
        public int clientId { get; set; }
        public int SessionID { get; set; }
        public int Status { get; set; }  // 1 - Open, 2 - Paid
        public Decimal Total { get; set; }
        public Decimal Paid { get; set; }
        public Decimal Remaining { get; set; }
        [JsonIgnore]
        public ICollection<StoreCartsEntityDetails>? StoreCartsEntityDetails {  get; set; }
    }
}
