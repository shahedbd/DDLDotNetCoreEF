using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DDLDotNetCoreEF.Models.ViewModel
{
    public class GlobalCitizenVM
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        [DisplayName("Country Name")]
        public string CountryName { get; set; }
        [DisplayName("Continent Name")]
        public string ContinentName { get; set; }
        public string Gender { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
