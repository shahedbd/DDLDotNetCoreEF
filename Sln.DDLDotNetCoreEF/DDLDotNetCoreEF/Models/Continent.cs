using System.ComponentModel;

namespace DDLDotNetCoreEF.Models
{
    public class Continent
    {
        [DisplayName("Continent ID")]
        public byte ContinentID { get; set; }
        [DisplayName("Continent Name")]
        public string ContinentName { get; set; }
    }
}
