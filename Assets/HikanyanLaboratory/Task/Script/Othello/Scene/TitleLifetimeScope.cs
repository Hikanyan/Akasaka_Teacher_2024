using HikanyanLaboratory.Task.Script.Othello.View;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    /// <summary>
    /// LifetimeScope for Title Scene.
    /// </summary>
    public class TitleLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // 子コンテナ
            base.Configure(builder);
            // 親コンテナからManagerSceneControllerを取得
            var parentLifetimeScope = Parent;
            if (parentLifetimeScope != null)
            {
                var parentContainer = parentLifetimeScope.Container;
                var sceneController = parentContainer.Resolve<ManagerSceneController>();
                builder.RegisterInstance(sceneController);
            }

            // TitleSceneの依存関係を登録
            builder.RegisterComponentInHierarchy<TitleView>();
        }
    }
}