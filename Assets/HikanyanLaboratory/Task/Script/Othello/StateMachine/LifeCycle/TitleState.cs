using HikanyanLaboratory.Task.Script.Othello.Scene;
using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello
{
    public class TitleState : State
    {
        public OthelloPresenter Presenter { get; set; }

        public override void Enter()
        {
            // タイトル画面の初期化処理
            Presenter.ShowTitleScreen();
        }

        public override void Execute()
        {
            // タイトル画面での実行処理
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Presenter.StartGame();
            }
        }

        public override void Exit()
        {
            // タイトル画面終了時の処理
            Presenter.HideTitleScreen();
        }
    }
}
