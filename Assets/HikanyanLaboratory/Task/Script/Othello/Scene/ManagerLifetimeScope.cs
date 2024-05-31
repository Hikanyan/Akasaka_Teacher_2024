using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    public class ManagerLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<ManagerSceneController>(Lifetime.Singleton);

            builder.Register<StateMachine>(Lifetime.Singleton);
            builder.Register<TitleState>(Lifetime.Scoped);
            builder.Register<InGameState>(Lifetime.Scoped);
            builder.Register<ResultState>(Lifetime.Scoped);

            builder.RegisterEntryPoint<ManagerPresenter>();
            
            builder.Register<OthelloPresenter>(Lifetime.Singleton).AsSelf();
        }
    }
}