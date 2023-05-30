using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Quizzes.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Quizzes.Commands.QuizCommands.UpdateQuiz
{
    public record UpdateQuizCommand : ICommand<bool>
    {
        public long QuizID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public long UserID { get; set; }
        public ICollection<QuestionUpdateCommand> Questions { get; set; }
    }

    public record QuestionUpdateCommand
    {
        public long ID { get; set; }
        public string Content { get; set; }
        public byte Points { get; set; }
        public ICollection<AnswerUpdateCommand> Answers { get; set; }
    }

    public record AnswerUpdateCommand
    {
        public long ID { get; set; }
        public string Content { get; set; }
        public bool Correct { get; set; }
    }

    public class UpdateQuizCommandHandler : ICommandHandler<UpdateQuizCommand, bool>
    {
        private readonly IQuizzesDbContext qContext;

        public UpdateQuizCommandHandler(IQuizzesDbContext qContext)
        {
            this.qContext = qContext;
        }

        public async Task<bool> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz quizToUpdate = await qContext.Quizzes
                .Include(q => q.User)
                .Include(q => q.Questions)
                .ThenInclude(qu => qu.Answers)
                .FirstOrDefaultAsync(q => q.ID == request.QuizID && q.User.ID == request.UserID);

            try
            {
                UpdateQuiz(request, quizToUpdate);
                quizToUpdate.MaxPoints = (short)quizToUpdate.Questions.Sum(q => q.Points);
                await qContext.SaveChangesAsync(cancellationToken);
                return true;
            } catch(ResourceNotFoundException)
            {
                return false;
            } catch(Exception)
            {
                throw;
            }
        }

        private void UpdateQuiz(UpdateQuizCommand quizCommand, Quiz quiz)
        {
            if(quiz == null)
            {
                throw new ResourceNotFoundException("Quiz", quizCommand.QuizID);
            }
            quiz.Description = !string.IsNullOrEmpty(quizCommand.Description) 
                ? quizCommand.Description : quiz.Description;
            quiz.Name = !string.IsNullOrEmpty(quizCommand.Name) 
                ? quizCommand.Name : quiz.Name;
            foreach(QuestionUpdateCommand question in quizCommand.Questions)
            {
                Question questionToUpdate = quiz.Questions
                    .FirstOrDefault(q => q.ID == question.ID);
                UpdateQuestion(question, questionToUpdate);
            }
        }

        private void UpdateQuestion(QuestionUpdateCommand questionCommand, Question question)
        {
            if (question == null)
            {
                throw new ResourceNotFoundException("Question", questionCommand.ID);
            }
            question.Content = !string.IsNullOrEmpty(questionCommand.Content)
                ? questionCommand.Content : question.Content;
            question.Points = questionCommand.Points > 0 
                ? questionCommand.Points : question.Points;
            foreach(AnswerUpdateCommand answer in questionCommand.Answers)
            {
                Answer answerToUpdate = question.Answers
                    .FirstOrDefault(a => a.ID == answer.ID);
                UpdateAnswer(answer, answerToUpdate);
            }
        }

        private void UpdateAnswer(AnswerUpdateCommand answerCommand, Answer answer)
        {
            if (answer == null)
            {
                throw new ResourceNotFoundException("Answer", answerCommand.ID);
            }
            answer.Content = !string.IsNullOrEmpty(answerCommand.Content)
                ? answerCommand.Content : answer.Content;
            answer.Correct = answerCommand.Correct;
        }
    }
}
