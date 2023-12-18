using AutoMapper;
using WebAPI_V1.Models;
using WebAPI_V1.Models.RequestDto;
using WebAPI_V1.Models.ResponseDto.ImportItemResponseDto;

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

            CreateMap<ExportItemDetail, ExportItemDetailRequestDto>();

            CreateMap<ExportItemDetailRequestDto, ExportItemDetail>();

            CreateMap<PoDetail, PORequestDto>();

            CreateMap<PORequestDto, PoDetail>();

            CreateMap<ImportItem, ImportItemResponseDto>()
                .ForMember(des => des.Bill_No, mem => mem.MapFrom(s => s.BillNo))
                .ForMember(des => des.Sum_Quantity, mem => mem.MapFrom(s => s.SumQuantity))
                .ForMember(des => des.Sum_Price, mem => mem.MapFrom(s => s.SumPrice))
                .ForMember(des => des.Sum_Payment, mem => mem.MapFrom(s => s.SumPayment));

            CreateMap<ImportItemResponseDto, ImportItem>()
                .ForMember(des => des.BillNo, mem => mem.MapFrom(s => s.Bill_No))
                .ForMember(des => des.SumQuantity, mem => mem.MapFrom(s => s.Sum_Quantity))
                .ForMember(des => des.SumPrice, mem => mem.MapFrom(s => s.Sum_Price))
                .ForMember(des => des.SumPayment, mem => mem.MapFrom(s => s.Sum_Payment));
        }
    }
}

