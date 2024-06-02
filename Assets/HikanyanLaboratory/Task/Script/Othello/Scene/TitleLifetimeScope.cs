using HikanyanLaboratory.Task.Script.Othello.View;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    public class TitleLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // 親コンテナからManagerSceneControllerを解決
            var parentScope = FindObjectOfType<ManagerLifetimeScope>();
            if (parentScope != null)
            {
                var parentContainer = parentScope.Container;
                var sceneController = parentContainer.Resolve<ManagerSceneController>();
                builder.RegisterInstance(sceneController);
            }

            // TitleSceneの依存関係を登録
            builder.RegisterComponentInHierarchy<TitleView>();
        }
    }
}