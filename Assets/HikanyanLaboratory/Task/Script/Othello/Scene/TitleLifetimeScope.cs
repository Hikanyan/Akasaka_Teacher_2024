using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    public class TitleLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // TitleSceneの依存関係を登録
            builder.Register<StateMachine>(Lifetime.Singleton);
            builder.Register<TitleState>(Lifetime.Singleton);
        }
    }
}