using AutoMapper;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Users.Queries.GetUserProfile
{
    public record GetUserProfileQuery : IQuery<UserProfileDTO>
    {
        public int UserID { get; set; }
    }

    public class GetUserProfileQueryHandler: IQueryHandler<GetUserProfileQuery, UserProfileDTO>
    {
        private readonly IMapper mapper;
        private readonly IUsersDbContext context;

        public GetUserProfileQueryHandler(IMapper mapper, IUsersDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<UserProfileDTO> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.ID == request.UserID);

            if(entity == null)
            {
                throw new ResourceNotFoundException("User", request.UserID);
            }

            return mapper.Map<UserProfileDTO>(entity);
        }
    }
}
