using LearningBuddy.Application.Common;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Extensions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Quizzes.Queries.GetListOfQuizzes
{
    public record GetListOfQuizzesQuery : IQuery<PaginatedList<QuizItemDTO>>
    {
        public long? UserID { get; set; }
        public long SubjectID { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetListOfQuizzesQueryHandler : IQueryHandler
        <GetListOfQuizzesQuery, PaginatedList<QuizItemDTO>>
    {
        private readonly ISubjectsDbContext subContext;
        private readonly IQuizzesDbContext qContext;

        public GetListOfQuizzesQueryHandler(ISubjectsDbContext subContext, IQuizzesDbContext qContext)
        {
            this.subContext = subContext;
            this.qContext = qContext;
        }
        public async Task<PaginatedList<QuizItemDTO>> Handle(GetListOfQuizzesQuery request, CancellationToken cancellationToken)
        {
            await CheckUsersAccessToSubject(request.UserID, request.SubjectID);
            return await qContext.Quizzes
                .Include(q => q.User)
                .Include(q => q.Subject)
                .Include(q => q.Questions)
                .AsNoTracking()
                .Where(q => q.Subject.ID == request.SubjectID)
                .Select(q => new QuizItemDTO()
                {
                    ID = q.ID,
                    IsOwner = q.User.ID == request.UserID,
                    ModifiedAt = q.ModifiedAt,
                    Name = q.Name,
                    QuestionCount = (short)q.Questions.Count,
                    SubjectName = q.Subject.Name,
                    UserUsername = q.User.Username
                })
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        private async Task CheckUsersAccessToSubject(long? userID, long subjectID)
        {
            Subject sub = await subContext.Subjects
                .Include(s => s.Creator)
                .FirstOrDefaultAsync(s => s.ID == subjectID);

            if(sub == null)
            {
                throw new ResourceNotFoundException("Subject", subjectID);
            }
            else if(!sub.Public && sub.Creator.ID != userID)
            {
                throw new UnauthorizedResourceAccessException("Subject", subjectID);
            }
        }
    }
}
