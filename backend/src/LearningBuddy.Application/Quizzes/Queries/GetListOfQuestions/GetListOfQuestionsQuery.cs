using AutoMapper;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Quizzes.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Quizzes.Queries.GetListOfQuestions
{
    public record GetListOfQuestionsQuery : IQuery<IList<QuestionItemDTO>>
    {
        public long? UserID { get; set; }
        public long QuizID { get; set; }
    }

    public class GetListOfQuestionsQueryHandler : IQueryHandler
        <GetListOfQuestionsQuery, IList<QuestionItemDTO>>
    {
        private readonly IMapper mapper;
        private readonly IQuizzesDbContext qContext;

        public GetListOfQuestionsQueryHandler(IMapper mapper, IQuizzesDbContext qContext)
        {
            this.mapper = mapper;
            this.qContext = qContext;
        }
        public async Task<IList<QuestionItemDTO>> Handle(GetListOfQuestionsQuery request, CancellationToken cancellationToken)
        {
            await CheckUsersAccessToSubject(request.UserID, request.QuizID);
            return await qContext.Questions
                .Include(q => q.Quiz)
                .Include(q => q.Answers)
                .Where(q => q.Quiz.ID == request.QuizID)
                .Select(q => mapper.Map<QuestionItemDTO>(q))
                .ToListAsync(cancellationToken);
        }

        private async Task CheckUsersAccessToSubject(long? userID, long quizID)
        {
            Quiz quiz = await qContext.Quizzes
                .Include(q => q.Subject)
                .ThenInclude(s => s.Creator)
                .FirstOrDefaultAsync(q => q.ID == quizID);

            if(quiz == null)
            {
                throw new ResourceNotFoundException("Quiz", quizID);
            }
            else if(!quiz.Subject.Public && quiz.Subject.Creator.ID != userID)
            {
                throw new UnauthorizedResourceAccessException("Quiz", quizID);
            }
        }
    }
}
