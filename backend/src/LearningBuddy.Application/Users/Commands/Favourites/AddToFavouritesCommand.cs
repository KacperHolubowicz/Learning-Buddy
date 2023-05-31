using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Common.Interfaces.Messaging;
using LearningBuddy.Application.Common.Interfaces.Persistence;
using LearningBuddy.Domain.Quizzes.Entities;
using LearningBuddy.Domain.Subjects.Entities;
using LearningBuddy.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningBuddy.Application.Users.Commands.Favourites
{
    public record AddToFavouritesCommand : ICommand<bool>
    {
        public long UserID { get; set; }
        public long ObjectID { get; set; }
        public AddToFavouritesTypes ObjectType { get; set; }
    }

    public class AddToFavouritesCommandHandler 
        : ICommandHandler<AddToFavouritesCommand, bool>
    {
        private readonly IUsersDbContext uContext;
        private readonly ISubjectsDbContext sContext;
        private readonly IQuizzesDbContext qContext;

        public AddToFavouritesCommandHandler(IUsersDbContext uContext, ISubjectsDbContext sContext,
            IQuizzesDbContext qContext)
        {
            this.uContext = uContext;
            this.sContext = sContext;
            this.qContext = qContext;
        }

        public async Task<bool> Handle
            (AddToFavouritesCommand request, CancellationToken cancellationToken)
        {
            if(!await UserExists(request.UserID))
            {
                throw new ResourceNotFoundException("User", request.UserID);
            }
            if(!await HasAccess(request))
            {
                throw new ResourceNotFoundException(request.ObjectType.ToString(), request.ObjectID);
            }
            if(await AlreadyFavourite(request))
            {
                return false;
            }
            await AddObjectToFavourite(request, cancellationToken);
            return true;
        }

        private async Task<bool> UserExists(long userId)
        {
            return await uContext.Users.AnyAsync(u => u.ID == userId);
        }

        private async Task<bool> HasAccess(AddToFavouritesCommand req)
        {
            switch (req.ObjectType)
            {
                case AddToFavouritesTypes.Subject:
                    return await sContext.Subjects
                        .Include(s => s.Creator)
                        .AnyAsync(s => s.ID == req.ObjectID &&
                            s.Public || s.Creator.ID == req.UserID);
                case AddToFavouritesTypes.Quiz:
                    return await qContext.Quizzes
                        .Include(q => q.Subject)
                        .ThenInclude(s => s.Creator)
                        .AnyAsync(q => q.ID == req.ObjectID &&
                            q.Subject.Public || q.Subject.Creator.ID == req.UserID);
                case AddToFavouritesTypes.LearningSource:
                    return await sContext.Sources
                        .Include(s => s.Subject)
                        .ThenInclude(su => su.Creator)
                        .AnyAsync(s => s.ID == req.ObjectID &&
                        s.Subject.Public || s.Subject.Creator.ID == req.UserID);
                default:
                    throw new ResourceNotFoundException($"There is no object type marked as {req.ObjectType}");
            }
        }

        private async Task<bool> AlreadyFavourite(AddToFavouritesCommand req)
        {
            switch(req.ObjectType)
            {
                case AddToFavouritesTypes.Subject:
                    return await uContext.FavouriteSubjects
                        .Include(fs => fs.Subject)
                        .Include(fs => fs.User)
                        .AnyAsync(fs => fs.Subject.ID == req.ObjectID && fs.User.ID == req.UserID);
                case AddToFavouritesTypes.Quiz:
                    return await uContext.FavouriteQuizzes
                        .Include(fq => fq.Quiz)
                        .Include(fq => fq.User)
                        .AnyAsync(fq => fq.Quiz.ID == req.ObjectID && fq.User.ID == req.UserID);
                case AddToFavouritesTypes.LearningSource:
                    return await uContext.FavouriteLearningSources
                        .Include(fl => fl.LearningSource)
                        .Include(fl => fl.User)
                        .AnyAsync(fl => fl.LearningSource.ID == req.ObjectID && fl.User.ID == req.UserID);
                default:
                    throw new ResourceNotFoundException($"There is no object type marked as {req.ObjectType}");
            }
        }

        private async Task AddObjectToFavourite(AddToFavouritesCommand req, CancellationToken ct)
        {
            User user = await uContext.Users.FindAsync(req.UserID);
            switch (req.ObjectType)
            {
                case AddToFavouritesTypes.Subject:
                    Subject s = await sContext.Subjects.FindAsync(req.ObjectID);
                    await uContext.FavouriteSubjects
                        .AddAsync(new FavouriteSubject() { Subject = s, User = user });
                    break;
                case AddToFavouritesTypes.Quiz:
                    Quiz q = await qContext.Quizzes.FindAsync(req.ObjectID);
                    await uContext.FavouriteQuizzes
                        .AddAsync(new FavouriteQuiz() { Quiz = q, User = user });
                    break;
                case AddToFavouritesTypes.LearningSource:
                    LearningSource l = await sContext.Sources.FindAsync(req.ObjectID);
                    await uContext.FavouriteLearningSources
                        .AddAsync(new FavouriteLearningSource() { LearningSource = l, User = user });
                    break;
            }
            await uContext.SaveChangesAsync(ct);
        }
    }
}
