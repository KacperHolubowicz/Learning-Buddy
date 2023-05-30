using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Quizzes.Entities;
using LearningBuddy.Domain.Subjects.Entities;
using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Quizzes.Commands.AttemptCommands.CreateAttempt
{
    public record CreateAttemptCommand : ICommand<AttemptCreateResponseDTO>
    {
        public long QuizID { get; set; }
        public long? UserID { get; set; }
        public IList<AttemptAnswerCommand> Answers { get; set; }
    }

    public record AttemptAnswerCommand
    {
        public long QuestionID { get; set; }
        public long AnswerID { get; set; }
    }

    public class CreateAttemptCommandHandler : 
        ICommandHandler<CreateAttemptCommand, AttemptCreateResponseDTO>
    {
        private readonly IQuizzesDbContext qContext;
        private readonly IUsersDbContext uContext;

        public CreateAttemptCommandHandler(IQuizzesDbContext qContext, IUsersDbContext uContext)
        {
            this.qContext = qContext;
            this.uContext = uContext;
        }

        public async Task<AttemptCreateResponseDTO> Handle(CreateAttemptCommand request, CancellationToken cancellationToken)
        {
            Quiz solvedQuiz = await qContext.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(qu => qu.Answers)
                .FirstOrDefaultAsync(q => q.ID == request.QuizID);

            if (!await CheckUsersAccessToSubject(request.UserID, request.QuizID) || solvedQuiz == null)
            {
                throw new ResourceNotFoundException("Quiz", request.QuizID);
            }

            AttemptCreateResponseDTO attemptResp = CreateAttemptResponse(request, solvedQuiz);

            if(request.UserID != null && request.UserID != 0)
            {
                Attempt newAttempt = await AttemptResponseToEntity(request, solvedQuiz, attemptResp);
                await qContext.Attempts
                    .AddAsync(newAttempt);
                await qContext.SaveChangesAsync(cancellationToken);
                attemptResp.ID = newAttempt.ID;
            }
            return attemptResp;
        }

        private AttemptCreateResponseDTO CreateAttemptResponse(CreateAttemptCommand attempt, Quiz quiz)
        {
            return new AttemptCreateResponseDTO()
            {
                AttemptedAt = DateTimeOffset.UtcNow,
                MaxPoints = quiz.MaxPoints,
                Points = GetQuizResult(attempt, quiz)
            };
        }

        private async Task<Attempt> AttemptResponseToEntity(CreateAttemptCommand attempt, 
            Quiz quiz, AttemptCreateResponseDTO resp)
        {
            User user = await uContext.Users.FindAsync(attempt.UserID);
            if(user == null)
            {
                throw new ResourceNotFoundException("User", attempt.UserID.Value);
            }

            return new Attempt()
            {
                MaxPoints = resp.MaxPoints,
                Points = resp.Points,
                Quiz = quiz,
                AttemptedAt = resp.AttemptedAt,
                User = user,
                Answers = attempt.Answers.Select(a => new AttemptAnswer()
                {
                    Question = quiz.Questions
                        .First(q => q.ID == a.QuestionID),
                    Answer = quiz.Questions
                        .First(q => q.ID == a.QuestionID)
                        .Answers
                        .First(an => an.ID == a.AnswerID)
                }).ToArray()
            };
        }

        private short GetQuizResult(CreateAttemptCommand attempt, Quiz quiz) 
        {
            short points = 0;
            foreach(AttemptAnswerCommand answer in attempt.Answers)
            {
                Question question = quiz.Questions.FirstOrDefault(q => q.ID == answer.QuestionID);
                if (question == null)
                {
                    throw new ResourceNotFoundException("Question", answer.QuestionID);
                }
                Answer ans = question.Answers.FirstOrDefault(a => a.ID == answer.AnswerID);
                if(ans == null)
                {
                    throw new ResourceNotFoundException("Answer", answer.AnswerID);
                }
                if(ans.Correct)
                {
                    points += question.Points;
                }
            }
            return points;
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
