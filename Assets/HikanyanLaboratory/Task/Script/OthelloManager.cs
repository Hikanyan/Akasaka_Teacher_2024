using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Othello
{
    public class OthelloManager : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Presenter, View, Model, その他の依存関係の登録
            builder.RegisterComponentInHierarchy<OthelloView>();
            builder.Register<OthelloModel>(Lifetime.Scoped).AsSelf();
            builder.Register<OthelloPresenter>(Lifetime.Singleton).AsSelf();
        }
    }
}