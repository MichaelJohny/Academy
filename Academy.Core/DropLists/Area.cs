using Academy.Core.ComplexTypes;

namespace Academy.Core.DropLists
{
    public class Area:BaseComplex
    {
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
