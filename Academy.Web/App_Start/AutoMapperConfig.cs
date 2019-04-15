using AutoMapper;

namespace Academy.Web.App_Start
{
    public class AutoMapperConfig
    {
        private static MapperConfiguration _mapperConfiguration;
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if (_mapper != null) return _mapper;
                if (_mapperConfiguration == null)
                    Configure();

                if (_mapperConfiguration != null) _mapper = _mapperConfiguration.CreateMapper();
                return _mapper;
            }
            set { _mapper = value; }
        }

        public static void Configure()
        {
            _mapperConfiguration = new MapperConfiguration(configure =>
            {                
                

            });
        }
    }
}