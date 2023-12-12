using AutoMapper;
using WebAPI_V1.Models;
using WebAPI_V1.Models.RequestDto;

namespace WebAPI_V1
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WarehouseRequestDto, WarehouseDetail>();
 
            CreateMap<WarehouseDetail, WarehouseRequestDto>();

            CreateMap<ExportItem, ExportItemRequestDto>();

            CreateMap<ExportItemRequestDto, ExportItem>();
        }
    }
}
