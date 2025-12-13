
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TasksAPI.Models;

namespace TasksAPI.Entities
{
    public class ReportsEntities: BaseEntity
    {

        public ReportsEntities() {
            ReportStatus = (int)DBEntityStatus.ACTIVE;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Descrption { get; set; } =String.Empty;
        [Required]
        public int ReportType { get; set; }
        [Required]
        public int ReportMode { get; set; }
        [Required]

        public int ReportStatus { get; set; }
        public string Params { get; set; } = String.Empty;

        public ICollection<ReportsEntitiesResults>? ReportsEntitiesResults {  get; set; }
    }
}
