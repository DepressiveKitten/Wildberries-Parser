using AutoMapper;

namespace Wildberries_Parser
{
    /// <summary>
    /// Project Mapping profile.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            this.CreateMap<Wildberries.Parsing.SiteItem, Wildberries.Parsing.JsonSearchPage.Deserialization.Product>();
            this.CreateMap<Wildberries.Parsing.JsonSearchPage.Deserialization.Product, Wildberries.Parsing.SiteItem>();
        }
    }
}
