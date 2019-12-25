namespace whris_v2.Mapping
{
    public class MappingProfile<TSource, TDestination>
    {
        public AutoMapper.MapperConfiguration config { get; set; }
        public AutoMapper.Mapper mapper { get; set; }

        public MappingProfile()
        {
            config = new AutoMapper.MapperConfiguration(x => x.CreateMap<TSource, TDestination>());
            mapper = new AutoMapper.Mapper(config);
        }
    }
}