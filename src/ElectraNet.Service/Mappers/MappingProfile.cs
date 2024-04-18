using AutoMapper;
using ElectraNet.Service.DTOs.Assets;
using ElectraNet.Domain.Enitites.Commons;

namespace ElectraNet.Service.Mappers;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<AssetCreateModel, Asset>().ReverseMap();
        CreateMap<Asset, AssetViewModel>().ReverseMap();
    }
}
