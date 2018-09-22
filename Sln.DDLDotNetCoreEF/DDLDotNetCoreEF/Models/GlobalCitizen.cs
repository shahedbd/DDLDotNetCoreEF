using System.ComponentModel;

namespace DDLDotNetCoreEF.Models
{
    public class GlobalCitizen: BaseEntity
    {
        public string Name { get; set; }
        [DisplayName("Country Name")]
        public string CountryName { get; set; }
        public byte ContinentCode { get; set; }
        public byte Gender { get; set; }
    }
}
