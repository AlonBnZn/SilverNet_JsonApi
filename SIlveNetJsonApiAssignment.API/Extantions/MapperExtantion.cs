using AutoMapper;
using Humanizer.Localisation;
using SilveNetJsonApiAssignment.Service.Migrations;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilveNetJsonApiAssignment.Service.Extantions
{
    public static class MapperExtensions
    {
        public static TenantResource ToResource(this Tenant tenant)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tenant, TenantResource>();
            });

            var mapper = mapperConfiguration.CreateMapper();

            return mapper.Map<TenantResource>(tenant);
        }

        public static UserResource ToResource(this User user)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserResource>();
            });

            var mapper = mapperConfiguration.CreateMapper();

            return mapper.Map<UserResource>(user);
        }
    }
}
