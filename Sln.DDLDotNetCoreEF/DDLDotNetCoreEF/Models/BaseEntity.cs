using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DDLDotNetCoreEF.Models
{
    public class BaseEntity
    {
        public long ID { get; set; }
        public byte Status { get; set; }
        public string CreationUser { get; set; }
        [DisplayName("Creation Time")]
        public DateTime CreationDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<DateTime> LastUpdateDateTime { get; set; }
    }
}
