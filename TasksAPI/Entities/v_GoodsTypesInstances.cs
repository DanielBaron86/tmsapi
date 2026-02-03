using Microsoft.EntityFrameworkCore;

namespace TasksAPI.Entities;

[Keyless]
public class v_GoodsTypesInstances : BaseEntity
{
public int Id;
public int GoodModelId;
public int GoodBaseId;
public Decimal Price;
public int LocationId;
public string LocationName;
public string SerialNumber;
public string Name;
public string Type;
public string Manufacturer;
public int Status;
public int Quantity;
}
