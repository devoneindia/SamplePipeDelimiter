using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SamplePipeDelimiter.Models
{
    [Table(name: "pubacc_co", Schema = "main")]

    public class PubAccCO
    {
        [Key]
        [StringLength(2)]
        [Column(name: "record_type", Order = 1)]
        public string? RecordType { get; set; }

        [Precision(10, 0)] //Note this is 9 in legacy.
        [Column(name: "unique_system_identifier", Order = 2)]
        public required decimal UniqueSystemIdentifier { get; set; }

        [StringLength(14)]
        [Column(name: "uls_file_num", Order = 3)]
        public string? UlsFileNum { get; set; }

        [StringLength(10)]
        [Column(name: "callsign", Order = 4)]
        public string? CallSign { get; set; }

        [StringLength(10)]
        [Column(name: "comment_date", Order = 5)]
        public string? CommentDate { get; set; }

        [StringLength(255)]
        [Column(name: "description", Order = 6)]
        public string? Description { get; set; }

        [StringLength(1)]
        [Column(name: "status_code", Order = 7)]
        public string? StatusCode { get; set; }

        [Column(name: "status_date", Order = 8)]
        public DateTime? StatusDate { get; set; }
    }
}

