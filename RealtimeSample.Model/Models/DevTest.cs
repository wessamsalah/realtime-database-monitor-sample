using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeSample.Model
{
    public class DevTest 
    {
        public DevTest()
        {
            Isdeleted = false;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string CampaignName { get; set; }
        public DateTime Date { get; set; }
        public int Clicks { get; set; }
        public int Conversions { get; set; }
        public int Impressions { get; set; }
        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string AffiliateName { get; set; }
        public bool Isdeleted { get; set; }
    }
}
