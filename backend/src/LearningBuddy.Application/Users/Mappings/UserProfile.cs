using AutoMapper;
using LearningBuddy.Application.Users.Commands.RegisterUser;
using LearningBuddy.Application.Users.Queries.GetUserProfile;
using LearningBuddy.Domain.Users.Entities;

namespace LearningBuddy.Application.Users.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserProfileDTO>();
            CreateMap<RegisterUserCommand, User>()
                .ForMember(u => u.Password, opt => opt.Ignore());
        }
    }
}
