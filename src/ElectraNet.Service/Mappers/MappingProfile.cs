using AutoMapper;
using ElectraNet.Service.DTOs.Users;
using ElectraNet.Service.DTOs.Assets;
using ElectraNet.Domain.Enitites.Users;
using ElectraNet.Service.DTOs.UserRoles;
using ElectraNet.Service.DTOs.Employees;
using ElectraNet.Service.DTOs.Positions;
using ElectraNet.Domain.Enitites.Commons;
using ElectraNet.Service.DTOs.Permissions;
using ElectraNet.Domain.Enitites.Positions;
using ElectraNet.Domain.Enitites.Employees;
using ElectraNet.Service.DTOs.Organizations;
using ElectraNet.Service.DTOs.UserPermissions;
using ElectraNet.Domain.Enitites.Organizations;
using ElectraNet.Domain.Enitites.Transformers;
using ElectraNet.Service.DTOs.Transformers;
using ElectraNet.Domain.Enitites.TransformerPoints;
using ElectraNet.Service.DTOs.TransformerPoints;
using ElectraNet.Domain.Enitites.Cables;
using ElectraNet.Service.DTOs.Cables;
using ElectraNet.Domain.Enitites.Laboratories;
using ElectraNet.Service.DTOs.Laboratories;
using ElectraNet.Domain.Enitites.ServiceRecords;
using ElectraNet.Service.DTOs.ServiceRecords;

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

        CreateMap<Employee, EmployeeCreateModel>().ReverseMap();
        CreateMap<Employee, EmployeeUpdateModel>().ReverseMap();
        CreateMap<EmployeeViewModel, Employee>().ReverseMap();  

        CreateMap<Transformer, TransformerCreateModel>().ReverseMap();
        CreateMap<Transformer, TransformerUpdateModel>().ReverseMap();
        CreateMap<TransformerViewModel, Transformer>().ReverseMap();

        CreateMap<TransformerPoint, TransformerPointCreateModel>().ReverseMap();
        CreateMap<TransformerPoint, TransformerPointUpdateModel>().ReverseMap();
        CreateMap<TransformerPointViewModel, TransformerPoint>().ReverseMap();  

        CreateMap<Cable, CableCreateModel>().ReverseMap();
        CreateMap<Cable, CableUpdateModel>().ReverseMap();
        CreateMap<CableViewModel,  Cable>().ReverseMap();

        CreateMap<Laboratory, LaboratoryCreateModel>().ReverseMap();
        CreateMap<Laboratory, LaboratoryUpdateModel>().ReverseMap();
        CreateMap<LaboratoryViewModel, Laboratory>().ReverseMap();

        CreateMap<ServiceRecord, ServiceRecordCreateModel>().ReverseMap();
        CreateMap<ServiceRecord, ServiceRecordUpdateModel>().ReverseMap();
        CreateMap<ServiceRecordViewModel, ServiceRecord>().ReverseMap();
    }
}