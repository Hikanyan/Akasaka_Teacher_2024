using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HikanyanLaboratory.Task.Othello
{
    public class OthelloLifetimeScope : LifetimeScope
    {
        [SerializeField] GameObject _boardObject;
        [SerializeField] GameObject _blackStone;
        [SerializeField] GameObject _whiteStone;

        protected override void Configure(IContainerBuilder builder)
        {
            // OthelloManager の依存性を登録
            builder.Register<OthelloManager>(Lifetime.Singleton).AsSelf();
            builder.Register<OthelloManager>(Lifetime.Singleton).AsSelf();
            
            
            

            
                // .WithParameter("boardObject", _boardObject)
                // .WithParameter("blackStone", _blackStone)
                // .WithParameter("whiteStone", _whiteStone)
                // .WithParameter("boardSize", 8);
            
            builder.Register<OthelloModel>(Lifetime.Scoped).AsSelf();
            builder.Register<OthelloPresenter>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<OthelloView>();

            builder.RegisterEntryPoint<OthelloManager>();
        }
    }
}