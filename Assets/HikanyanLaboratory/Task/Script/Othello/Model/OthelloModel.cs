using System.Collections.Generic;
using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello.Model
{
    public class OthelloModel : IOthelloModel
    {
        private OthelloGameState _gameState;

        public OthelloModel()
        {
            // 初期ゲーム状態を設定
            _gameState = new OthelloGameState
            {
                Pieces = new List<GameObject>(),
                CurrentPlayer = new Player { Color = 0 } // 0が先手（黒）、1が後手（白）とする
            };
        }

        public OthelloGameState GetGameState()
        {
            return _gameState;
        }

        public void SetGameState(OthelloGameState gameState)
        {
            _gameState = gameState;
        }
    }
}