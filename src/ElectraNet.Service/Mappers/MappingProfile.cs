using AutoMapper;
using ElectraNet.Domain.Enitites.Commons;
using ElectraNet.Domain.Enitites.Organizations;
using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.DTOs.Assets;
using ElectraNet.Service.DTOs.Organizations;
using ElectraNet.Service.DTOs.Permissions;
using ElectraNet.Service.DTOs.Positions;
using ElectraNet.Service.DTOs.UserPermissions;
using ElectraNet.Service.DTOs.UserRoles;
using ElectraNet.Service.DTOs.Users;

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

        CreateMap<UserRole, UserRoleCreateModel>().ReverseMap();
        CreateMap<UserRole, UserRoleUpdateModel>().ReverseMap();
        CreateMap<UserRoleViewModel, UserRole>().ReverseMap();

        CreateMap<User, UserCreateModel>().ReverseMap();
        CreateMap<User, UserUpdateModel>().ReverseMap();
        CreateMap<UserViewModel, User>().ReverseMap();
        CreateMap<UserCreateModel, UserUpdateModel>().ReverseMap();

        CreateMap<Position, PositionCreateModel>().ReverseMap();
        CreateMap<Position, PositionUpdateModel>().ReverseMap();
        CreateMap<PositionViewModel, Position>().ReverseMap();

        CreateMap<Organization, OrganizationCreateModel>().ReverseMap();
        CreateMap<Organization, OrganizationUpdateModel>().ReverseMap();
        CreateMap<OrganizationViewModel, Organization>().ReverseMap();
    }
}