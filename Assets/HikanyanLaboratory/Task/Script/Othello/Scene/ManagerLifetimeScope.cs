using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    public class ManagerLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<StateMachine>(Lifetime.Singleton);
            builder.Register<ManagerPresenter>(Lifetime.Singleton);

            // builder.Register<TitleLifetimeScope>(Lifetime.Singleton);
            // builder.Register<InGameLifetimeScope>(Lifetime.Singleton);
            // builder.Register<ResultLifetimeScope>(Lifetime.Singleton);
        }
    }
}