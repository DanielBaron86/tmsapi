using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Entities
{
    public class AccountsGoodsEntity : BaseEntity
    {
        public int Id { get; set; }
        [ForeignKey("AccountId")]
        public Accounts? Accounts {  get; set; }
        public int AccountId { get; set; }
        public int GoodId { get; set; }
        public double price { get; set; }
        public int Status { get; set; }

    }
}
