using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TasksAPI.Entities
{
    public class BaseEntity
    {

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
    }
}
