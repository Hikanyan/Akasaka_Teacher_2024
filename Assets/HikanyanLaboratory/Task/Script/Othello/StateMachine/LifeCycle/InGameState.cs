namespace HikanyanLaboratory.Task.Script.Othello
{
    public class InGameState : State
    {
        public OthelloPresenter Presenter { get; set; }

        public override void Enter()
        {
            // ゲーム開始の初期化処理
            Presenter.StartGame();
        }

        public override void Execute()
        {
            // ゲーム中の実行処理（例: プレイヤーの操作待ち）
        }

        public override void Exit()
        {
            // ゲーム終了時の処理
        }
    }
}