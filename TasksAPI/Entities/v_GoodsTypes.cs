using Microsoft.EntityFrameworkCore;

namespace TasksAPI.Entities;
[Keyless]
public class v_GoodsTypes : BaseEntity
{
    public int Id;
    public int GoodBaseId;
    public string Name;
    public string Description;
    public string Type;
    public string Manufacturer;
}
