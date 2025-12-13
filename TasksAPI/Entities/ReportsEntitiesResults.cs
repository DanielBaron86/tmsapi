using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Entities
{
    public class ReportsEntitiesResults
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("ReportID")]
        public  ReportsEntities? ReportsEntities { get; set; }
        public int ReportID { get; set; }
        public DateTime RunDate { get; set; }
        public string ReportResults { get; set; } = string.Empty;
    }
}
