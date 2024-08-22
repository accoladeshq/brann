using Accolades.Brann.WixGenerator.Dtos;
using AutoMapper;

namespace Accolades.Brann.WixGenerator.Profiles;

public class WixProfile : Profile
{
    public WixProfile()
    {
        CreateMap<WixComponent, WixComponentDto>();
        CreateMap<WixComponentRef, WixComponentRefDto>();
        CreateMap<WixComponentGroup, WixComponentGroupDto>();
        CreateMap<WixDir, WixDirDto>();
        CreateMap<Wix, WixDto>()
            .ForMember(dto => dto.Include, option => option.MapFrom((wix, _) => 
            {
                var publishDir = $"\"$(sys.CURRENTDIR){wix.Include}\"";
                var doc = new System.Xml.XmlDocument();
                return doc.CreateProcessingInstruction("include", publishDir);
            }));
        CreateMap<WixFile, WixFileDto>();
        CreateMap<WixFragment, WixFragmentDto>();
    }
}