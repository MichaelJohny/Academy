using System.Collections.Generic;
using Academy.Core.DropLists;

namespace Academy.Core.Dtos
{
    public class DdlData
    {
        public DdlData()
        {
             Collages = new List<Collage>();
            Nationalities =new List<Nationality>();
            Qualifiations = new List<Qualifiation>();
            Cities = new List<City>();
            Areas = new List<Area>();
            Specializations = new List<Specialization>();
        }
        public List<Collage> Collages { get; set; }
        public List<Nationality> Nationalities { get; set; }
        public List<Qualifiation> Qualifiations { get; set; }
        public List<City> Cities { get; set; }
        public List<Area> Areas { get; set; }
        public List<Specialization> Specializations { get; set; }
        
    }
}
