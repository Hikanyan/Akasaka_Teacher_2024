using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    /// <summary>
    /// Root LifetimeScope for Manager Scene.
    /// 全体のSceneに反映されるLifetimeScope
    /// 親コンテナ
    /// </summary>
    public class ManagerLifetimeScope : LifetimeScope
    {
        [SerializeField] private bool _isDebugMode;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<ManagerSceneController>(Lifetime.Singleton);

            builder.Register<StateMachine>(Lifetime.Singleton);
            builder.Register<TitleState>(Lifetime.Scoped);
            builder.Register<InGameState>(Lifetime.Scoped);
            builder.Register<ResultState>(Lifetime.Scoped);

            builder.RegisterEntryPoint<ManagerPresenter>().WithParameter("isDebugMode", _isDebugMode);
        }
    }
}