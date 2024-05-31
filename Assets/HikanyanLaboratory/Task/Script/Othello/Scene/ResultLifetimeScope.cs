using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    public class ResultLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // ResultSceneの依存関係を登録
            builder.Register<ResultState>(Lifetime.Singleton);
        }
    }
}