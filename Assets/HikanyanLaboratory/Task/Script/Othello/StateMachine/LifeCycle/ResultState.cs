using HikanyanLaboratory.Task.Script.Othello.Scene;
using UnityEngine;

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
            if (Input.GetKeyDown(KeyCode.Space)) // スペースキーでタイトル画面に戻る
            {
                Presenter.SwitchToTitleState();
            }
        }

        public override void Exit()
        {
            // リザルト画面終了時の処理
            Presenter.HideResultScreen();
        }
    }
}