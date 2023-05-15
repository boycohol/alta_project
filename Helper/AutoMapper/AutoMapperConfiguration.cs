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
                .ForMember(dst => dst.FullName, opt => opt.MapFrom(src => src.InternalUser.User.Name))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.InternalUser.User.Email))
                .ForMember(dst => dst.RoleId, opt => opt.MapFrom(src => src.InternalUser.RoleId))
                .ForMember(dst => dst.AreaId, opt => opt.MapFrom(src => src.AreaId))
                .ReverseMap();
            /*CreateMap<VisitPlan, VisitPlanModel>()
                .ForMember(dst => dst.TimeId, opt => opt.MapFrom(src => src.TimeId))
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Date.ToShortDateString()))
                .ForMember(dst => dst.DistributorId, opt => opt.MapFrom(src => src.DistributorId))
                .ForMember(dst => dst.GuestIds, opt => opt.MapFrom(src => src.GuestGroup.Guests));*/
            CreateMap<VisitTask, TaskModel>()
               .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status))
               .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))
               .ForMember(dst => dst.Rating, opt => opt.MapFrom(src => src.Rating))
               .ForMember(dst => dst.AssigneeStaffId, opt => opt.MapFrom(src => src.AssigneeStaffId))
               .ForMember(dst => dst.CreatorUserId, opt => opt.MapFrom(src => src.CreatorUserId))
               .ReverseMap();
        }
    }
}
