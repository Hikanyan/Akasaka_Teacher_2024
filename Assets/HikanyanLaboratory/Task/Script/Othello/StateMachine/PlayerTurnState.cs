using HikanyanLaboratory.Task.Script.Othello.Model;
using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello
{
    public class PlayerTurnState : State
    {
        private readonly IOthelloModel _model;
        public OthelloPresenter Presenter { get; set; }

        public PlayerTurnState(IOthelloModel model)
        {
            _model = model;
        }

        public override void Enter()
        {
            // Enter時の処理
            Debug.Log("PlayerTurnState");
        }

        public override void Execute()
        {
            // 実行時の処理
            Debug.Log("PlayerTurnState Execute");
        }

        public override void Exit()
        {
            // Exit時の処理
            Debug.Log("PlayerTurnState Exit");
        }
    }
}