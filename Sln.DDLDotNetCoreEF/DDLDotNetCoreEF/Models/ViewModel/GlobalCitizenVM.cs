using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DDLDotNetCoreEF.Models.ViewModel
{
    public class GlobalCitizenVM: BaseEntity
    {
        public string Name { get; set; }
        [DisplayName("Country Name")]
        public string CountryName { get; set; }
        public string ContinentName { get; set; }
    }
}
