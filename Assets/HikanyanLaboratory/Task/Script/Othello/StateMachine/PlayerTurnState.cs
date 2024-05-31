using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello
{
    public class PlayerTurnState : State
    {
        private readonly OthelloPresenter _presenter;

        public PlayerTurnState(OthelloPresenter presenter)
        {
            _presenter = presenter;
        }

        public override void Enter()
        {
            Debug.Log("Player Turn: Enter");
            // プレイヤーターン開始時の処理を実装
        }

        public override void Execute()
        {
            Debug.Log("Player Turn: Execute");
            // プレイヤーターンの実行中の処理を実装
        }

        public override void Exit()
        {
            Debug.Log("Player Turn: Exit");
            // プレイヤーターン終了時の処理を実装
        }
    }
}