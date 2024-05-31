using HikanyanLaboratory.Task.Script.Othello.Scene;
using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello
{
    public class TitleState : State
    {
        public ManagerPresenter Presenter { get; set; }

        public override void Enter()
        {
            // タイトル画面の初期化処理
        }

        public override void Execute()
        {
            // タイトル画面での実行処理
            if(Input.GetKeyDown(KeyCode.Space))
            {
                
            }
        }

        public override void Exit()
        {
            // タイトル画面終了時の処理
            
        }
    }
}
