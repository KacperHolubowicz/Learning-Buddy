using AutoMapper;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Application.Quizzes.Queries.GetListOfQuizzes;
using LearningBuddy.Application.Subjects.Queries.GetListOfLearningSources;
using LearningBuddy.Application.Subjects.Queries.GetListOfSubjects;
using LearningBuddy.Application.Users.Queries.GetFavourite;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Users.Queries.GetFavouriteSubjects
{
    public record GetFavouriteQuery : IQuery<GetFavouriteResponse>
    {
        public long UserID { get; set; }
    }

    public class GetFavouriteQueryHandler 
        : IQueryHandler<GetFavouriteQuery, GetFavouriteResponse>
    {
        private readonly IMapper mapper;
        private readonly IUsersDbContext uContext;

        public GetFavouriteQueryHandler(IMapper mapper, IUsersDbContext uContext)
        {
            this.mapper = mapper;
            this.uContext = uContext;
        }

        public async Task<GetFavouriteResponse> Handle(GetFavouriteQuery request,
            CancellationToken cancellationToken)
        {
            var subjects = await uContext.FavouriteSubjects
                .Include(fs => fs.Subject)
                .Where(fs => fs.User.ID == request.UserID)
                .Select(fs => mapper.Map<SubjectItemDTO>(fs.Subject))
                .ToListAsync();
            var quizzes = await uContext.FavouriteQuizzes
                .Include(fq => fq.Quiz)
                .Where(fq => fq.User.ID == request.UserID)
                .Select(fq => mapper.Map<QuizItemDTO>(fq.Quiz))
                .ToListAsync();
            var sources = await uContext.FavouriteLearningSources
                .Include(fl => fl.LearningSource)
                .Where(fl => fl.User.ID == request.UserID)
                .Select(fl => mapper.Map<LearningSourceDTO>(fl.LearningSource))
                .ToListAsync();
            return new GetFavouriteResponse()
            {
                Subjects = subjects,
                Quizzes = quizzes,
                LearningSources = sources
            };
        }
    }
}
