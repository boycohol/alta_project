using AltaProject.Entity;
using AltaProject.Model.EntityModel;
using AutoMapper;

namespace AltaProject.Helper.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Staff, StaffModel>()
                .ForMember(dst=>dst.FullName,opt=>opt.MapFrom(src=>src.User.Name))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dst => dst.RoleId, opt => opt.MapFrom(src => src.User.RoleId))
                .ForMember(dst => dst.AreaId, opt => opt.MapFrom(src => src.AreaId))
                .ReverseMap();
        }
    }
}
