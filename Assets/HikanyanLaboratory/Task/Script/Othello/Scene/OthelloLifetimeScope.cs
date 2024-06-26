using HikanyanLaboratory.Task.Script.Othello.Animation;
using HikanyanLaboratory.Task.Script.Othello.Factory;
using HikanyanLaboratory.Task.Script.Othello.Infrastructure;
using HikanyanLaboratory.Task.Script.Othello.Model;
using HikanyanLaboratory.Task.Script.Othello.Services;
using HikanyanLaboratory.Task.Script.Othello.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    /// <summary>
    /// Othelloのライフタイムスコープ
    /// </summary>
    public class OthelloLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameObject _blackStonePrefab;
        [SerializeField] private GameObject _whiteStonePrefab;
        [SerializeField] private GameObject _boardPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            // 親コンテナからManagerSceneControllerを取得
            var parentLifetimeScope = Parent;
            if (parentLifetimeScope != null)
            {
                var parentContainer = parentLifetimeScope.Container;
                var sceneController = parentContainer.Resolve<ManagerSceneController>();
                builder.RegisterInstance(sceneController);
            }

            // Model
            builder.Register<IOthelloModel, OthelloModel>(Lifetime.Singleton);
            builder.Register<OthelloGameState>(Lifetime.Singleton);
            builder.Register<Player>(Lifetime.Singleton);

            // View
            builder.RegisterComponentInHierarchy<OthelloBoardGenerator>()
                .WithParameter("boardPrefab", _boardPrefab);
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
            builder.Register<IGameObjectFactory, GameObjectFactory>(Lifetime.Singleton)
                .WithParameter("blackStonePrefab", _blackStonePrefab)
                .WithParameter("whiteStonePrefab", _whiteStonePrefab)
                .WithParameter("boardPrefab", _boardPrefab);

            // StateMachine
            builder.Register<StateMachine>(Lifetime.Singleton);
            builder.Register<PlayerTurnState>(Lifetime.Singleton).AsSelf();
            builder.Register<AITurnState>(Lifetime.Singleton).AsSelf();
            builder.Register<InGameState>(Lifetime.Singleton).AsSelf();


            // Presenter
            builder.Register<OthelloPresenter>(Lifetime.Singleton).AsSelf();
        }
    }
}