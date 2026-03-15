using Microsoft.EntityFrameworkCore;

namespace TasksAPI.Entities;

[Keyless]
public class v_GoodsTypesInstances : BaseEntity
{
    public int Id { get; set; }
    public int GoodModelId { get; set; }
    public int GoodBaseId { get; set; }
    public decimal Price { get; set; }
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    public string SerialNumber { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Manufacturer { get; set; }
    public int Status { get; set; }
    public int Quantity { get; set; }
}
