using HikanyanLaboratory.Task.Othello;
using HikanyanLaboratory.Task.Script.Othello.Animation;
using HikanyanLaboratory.Task.Script.Othello.Factory;
using HikanyanLaboratory.Task.Script.Othello.Infrastructure;
using HikanyanLaboratory.Task.Script.Othello.Model;
using HikanyanLaboratory.Task.Script.Othello.Services;
using HikanyanLaboratory.Task.Script.Othello.View;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Script.Othello
{
    public class OthelloLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Model
            builder.Register<IOthelloModel, IOthelloModel>(Lifetime.Singleton);
            builder.Register<OthelloGameState>(Lifetime.Singleton);
            builder.Register<Player>(Lifetime.Singleton);

            // View
            builder.RegisterComponentInHierarchy<OthelloBoardView>();
            builder.RegisterComponentInHierarchy<OthelloPieceView>();

            // Services
            builder.Register<IGameService, GameService>(Lifetime.Singleton);
            builder.Register<IAnimationService, AnimationService>(Lifetime.Singleton);
            builder.Register<IPersistenceService, PersistenceService>(Lifetime.Singleton);

            // Infrastructure
            builder.Register<IOthelloRepository, OthelloRepository>(Lifetime.Singleton);

            // Animation
            builder.Register<TweenMovement>(Lifetime.Singleton);

            // Factory
            builder.Register<GameObjectFactory>(Lifetime.Singleton);

            // Presenter
            builder.Register<OthelloPresenter>(Lifetime.Singleton);
        }
    }
}