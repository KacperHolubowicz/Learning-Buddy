using AutoMapper;
using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Application.Quizzes.Queries.GetListOfQuestions;
using LearningBuddy.Domain.Subjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Quizzes.Queries.GetListOfQuestionsWithAnswers
{
    public class GetListOfQuestionsWithAnswersQuery : IQuery<IList<QuestionWithAnswerItemDTO>>
    {
        public long? UserID { get; set; }
        public long QuizID { get; set; }
    }

    public class GetListOfQuestionsWithAnswerQueryHandler : IQueryHandler
        <GetListOfQuestionsWithAnswersQuery, IList<QuestionWithAnswerItemDTO>>
    {
        private readonly IMapper mapper;
        private readonly IQuizzesDbContext qContext;

        public GetListOfQuestionsWithAnswerQueryHandler(IMapper mapper, IQuizzesDbContext qContext)
        {
            this.mapper = mapper;
            this.qContext = qContext;
        }
        public async Task<IList<QuestionWithAnswerItemDTO>> Handle
            (GetListOfQuestionsWithAnswersQuery request, CancellationToken cancellationToken)
        {
            if (await CheckUsersAccessToSubject(request.UserID, request.QuizID))
            {
                return await qContext.Questions
                    .Include(q => q.Quiz)
                    .Include(q => q.Answers)
                    .Where(q => q.Quiz.ID == request.QuizID)
                    .Select(q => mapper.Map<QuestionWithAnswerItemDTO>(q))
                    .ToListAsync(cancellationToken);
            }
            throw new ResourceNotFoundException("Quiz", request.QuizID);
        }

        private async Task<bool> CheckUsersAccessToSubject(long? userID, long quizID)
        {
            Subject sub = (await qContext.Quizzes
                .Include(q => q.Subject)
                .ThenInclude(s => s.Creator)
                .FirstOrDefaultAsync(q => q.ID == quizID))
                .Subject;

            if (sub != null && (sub.Public || userID.HasValue && sub.Creator.ID == userID))
            {
                return true;
            }
            return false;
        }
    }
}
