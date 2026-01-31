namespace TasksAPI.Models;

public class v_GoodsTypesModel : BaseModel
{
    public int Id { get; set; }
    public int GoodModelId{ get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Manufacturer { get; set; }
}