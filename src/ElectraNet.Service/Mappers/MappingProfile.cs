using AutoMapper;
using ElectraNet.Service.DTOs.Assets;
using ElectraNet.Domain.Enitites.Commons;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.DTOs.UserPermissions;
using ElectraNet.Service.DTOs.Permissions;

namespace ElectraNet.Service.Mappers;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<AssetCreateModel, Asset>().ReverseMap();
        CreateMap<Asset, AssetViewModel>().ReverseMap();

        CreateMap<UserPermissionCreateModel, UserPermission>().ReverseMap();
        CreateMap<UserPermissionUpdateModel, UserPermission>().ReverseMap();
        CreateMap<UserPermission, UserPermissionViewModel>().ReverseMap();


        CreateMap<PermissionCreateModel, Permission>().ReverseMap();
        CreateMap<PermissionUpdateModel, Permission>().ReverseMap();
        CreateMap<Permission, PermissionViewModel>().ReverseMap();

    }
}