namespace HikanyanLaboratory.Task.Script.Othello
{
    public class ResultState : State
    {
        public OthelloPresenter Presenter { get; set; }

        public override void Enter()
        {
            // リザルト画面の初期化処理
            Presenter.ShowResultScreen();
        }

        public override void Execute()
        {
            // リザルト画面での実行処理（例: 入力待ち）
        }

        public override void Exit()
        {
            // リザルト画面終了時の処理
            Presenter.HideResultScreen();
        }
    }
}